using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace RFERL.Modules.Framework.Data.EntityFramework.Abstractions
{
    /// <summary>Contains extension methods for <see cref="IWebHost"/>.</summary>
    public static class WebHostExtensions
    {
        /// <summary>Migrate pending changes on database context.</summary>
        /// <typeparam name="TContext">Type of database context.</typeparam>
        /// <param name="webHost">Instance which implements <see cref="IWebHost"/>.</param>
        /// <param name="seeder">Delegate which is used for seeding database.</param>
        /// <returns>IWebHost instance.</returns>
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetRequiredService<TContext>();

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                    var retry = Policy.Handle<Exception>()
                        .WaitAndRetry(new TimeSpan[]
                        {
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(8),
                        });

                    retry.Execute(() =>
                    {
                        if (context.Database.GetPendingMigrations().Any())
                        {
                            context.Database.Migrate();
                        }

                        seeder(context, services);
                    });

                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                }
            }

            return webHost;
        }
    }
}