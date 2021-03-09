using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace ESourcing.Order.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>())
                {
                    try
                    {
                        orderContext.Database.Migrate();

                        OrderContextSeed.SeedAsync(orderContext)
                            .Wait();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
