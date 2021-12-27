using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.DataAccess.Context;
using FitnessManager.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FitnessManager.DataAccess
{
    public static class Seed
    {
        public static async Task SeedDataAsync(DataContext context, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<UserEntity>
                {
                    new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Maciek",
                        LastName = "Grzela",
                        UserName = "maciekgrzela45@gmail.com",
                        Email = "maciekgrzela45@gmail.com",
                    }
                };

                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("RegularUser"));
                }

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "zaq1@WSX");
                    await userManager.AddToRolesAsync(user, new[] {"Admin"});
                }
            }
        }
    }
}