using System;
using FitnessManager.DataAccess.Context;
using FitnessManager.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FitnessManager.API.Extensions
{
    public static class SeedManager
    {
        public static IHost SeedData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            using var manager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            
            try
            {
                Seed.SeedDataAsync(context, manager, config).Wait();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred during data seed");
            }

            return host;
        }
    }
}