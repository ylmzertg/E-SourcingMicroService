using ESourcing.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.Infrastructure.Data
{
    public class WebAppContextSeed
    {
        public static async Task SeedAsync(WebAppContext webAppContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // INFO: Run this if using a real database. Used to automaticly migrate docker image of sql server db.
                webAppContext.Database.Migrate();
                //orderContext.Database.EnsureCreated();

                if (!webAppContext.Employees.Any())
                {
                    webAppContext.Employees.AddRange(GetPreconfiguredOrders());
                    await webAppContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<WebAppContextSeed>();
                    log.LogError(exception.Message);
                    System.Threading.Thread.Sleep(2000);
                    await SeedAsync(webAppContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Employee> GetPreconfiguredOrders()
        {
            return new List<Employee>()
            {
               new Employee
               {
                   FirstName ="User1",
                   LastName="UserLastName1",
                   IsActive = true,
                   IsAdmin =false,
                   IsSeller = true
               }
            };
        }
    }
}
