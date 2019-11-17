using Culture.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess.Context
{
    public static class DBSeeder
    {
            public async static Task<IWebHost> SeedData(this IWebHost host)
            {
                using (var scope = host.Services.CreateScope())
                {

                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<CultureDbContext>();
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();


                if (context.Roles.Where(x=>x.Name=="Admin").FirstOrDefault() == null)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
                    await roleManager.CreateAsync(new IdentityRole<Guid>("User"));
                }
                if(context.Users.Where(x => x.UserName == "admin").FirstOrDefault() == null)
                {
                  
                    var user = new AppUser()
                    {
                        Email = "Lukasz12380@gmail.com",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        FirstName = "admin",
                        LastName = "admin",
                        UserName = "admin",
                      
                    };


                    await userManager.CreateAsync(user, "Kappa123$");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                context.SaveChanges();
            }
            return host;
            }
        }
    
}
