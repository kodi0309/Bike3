using System.Security.Claims;
using IdentityModel;
using Srv_Id.Data;
using Srv_Id.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Srv_Id;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
            using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userMgr.Users.Any()) return;

            var klaudiusz = userMgr.FindByNameAsync("klaudiusz").Result;
            if (klaudiusz == null)
            {
                klaudiusz = new ApplicationUser
                {
                    UserName = "klaudiusz",
                    Email = "klaudiuszkozlowski@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(klaudiusz, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(klaudiusz, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Klaudiusz Kozlowski"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("klaudiusz created");
            }
            else
            {
                Log.Debug("klaudiusz already exists");
            }

            var ksawery = userMgr.FindByNameAsync("ksawery").Result;
            if (ksawery == null)
            {
                ksawery = new ApplicationUser
                {
                    UserName = "ksawery",
                    Email = "ksawerykozlowski@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(ksawery, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(ksawery, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Ksawery Kozlowski"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("ksawery created");
            }
            else
            {
                Log.Debug("ksawery already exists");
            }
        
    }
}
