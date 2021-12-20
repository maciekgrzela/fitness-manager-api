using System.Linq;
using System.Threading.Tasks;
using FitnessManager.DataAccess.Context;
using FitnessManager.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FitnessManager.API
{
    public static class Seed
    {
        public static async Task SeedDataAsync(DataContext context, UserManager<UserEntity> manager, IConfiguration configuration)
        {
            if (!manager.Users.Any())
            {
                /*var usersId = Guid.NewGuid().ToString();
                
                var user = new User
                {
                    Id = usersId,
                    FirstName = "Maciek",
                    LastName = "Grzela",
                    UserName = usersId,
                    Email = "maciekgrzela45@gmail.com",
                    SelfDescription = "",
                    LookingForAJob = true,
                    Abilities = new List<Ability>(),
                    Achievements = new List<Achievement>(),
                    Projects = new List<Project>(),
                    WorkExperiences = new List<WorkExperience>()
                };

                await manager.CreateAsync(user, configuration.GetSection("DefaultUsersCredentials").Value);
                await context.SaveChangesAsync();*/
            }
        }
    }
}