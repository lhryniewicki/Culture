using Culture.Contracts.IRepositories;
using Culture.Contracts.IServices;
using Culture.DataAccess.Context;
using Culture.DataAccess.Repositories;
using Culture.Models;
using Culture.Contracts;
using Culture.DataAccess;
using Culture.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Culture.Services.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Culture.Contracts.Facades;
using Culture.Implementation.Facades;
using Culture.Implementation.SignalR;

namespace Culture.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:50882");
            }));
            services.AddSignalR(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
                options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            });

            services.AddScoped<IUserService, UserService>();
			services.AddScoped<IEventService, EventService>();
			services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReactionService, ReactionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEventReactionService, EventReactionService>();
            services.AddScoped<IGeolocationService, GeolocationService>();
            services.AddScoped<IEmailService, EmailService>();


            
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserInEventRepository, UserInEventRepository>();
            services.AddScoped<IEventReactionRepository, EventReactionRepository>();
            services.AddScoped<IEventInCalendarRepository, EventInCalendarRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IEventReactionRepository, EventReactionRepository> ();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAccountFacade, AccountFacade>();
            services.AddScoped<IAttendanceFacade, AttendanceFacade>();
            services.AddScoped<ICommentsFacade, CommentsFacade>();
            services.AddScoped<IEventsFacade, EventsFacade>();
            services.AddScoped<INotificationsFacade, NotificationsFacade>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});


			services.AddDbContext<CultureDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("Firmowe")));


			services.AddIdentity<AppUser, IdentityRole<Guid>>()
				.AddEntityFrameworkStores<CultureDbContext>();


            services.Configure<IdentityOptions>(options =>
			{
				// Default Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;


				options.User.AllowedUserNameCharacters =
					"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

			});


			ConfigureAuthentication(services);

          


        }

		private void ConfigureAuthentication(IServiceCollection services)
		{
			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Values:IssuerToken"]));

			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(config =>
				{
					config.RequireHttpsMetadata = false;
					config.SaveToken = true;
					config.TokenValidationParameters = new TokenValidationParameters
					{
						IssuerSigningKey = signingKey,
						ValidateLifetime = true,
						ValidateAudience = false,
						ValidateIssuer = false,
						ValidateIssuerSigningKey = true,
                        RoleClaimType = "Role",
                    };

                    config.Events = new JwtBearerEvents
                    {
                        
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/notification"))||
                                path.StartsWithSegments("/event"))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notification");
                routes.MapHub<EventHub>("/event");

            });


            if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

            app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});


           

		}
	}
}
