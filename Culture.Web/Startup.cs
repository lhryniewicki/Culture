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

			services.AddScoped<CultureDbContext, CultureDbContext>();


			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IEventService, EventService>();
			services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReactionService, ReactionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IFileService, FileService>();



            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserInEventRepository, UserInEventRepository>();
            services.AddScoped<IEventInCalendarRepository, EventInCalendarRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IEventReactionRepository, EventReactionRepository> ();
            services.AddScoped<INotificationRepository, NotificationRepository>();



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
						ValidateIssuerSigningKey = true
					};
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
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
			app.UseSpaStaticFiles();

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

			app.UseAuthentication();

		}
	}
}
