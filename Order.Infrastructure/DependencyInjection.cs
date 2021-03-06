using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Repositories;
using Ordering.Domain.Repositories.Base;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repository;
using Ordering.Infrastructure.Repository.Base;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("OrderConnection"),
                        b => b.MigrationsAssembly(typeof(OrderContext).Assembly.FullName)), ServiceLifetime.Singleton);

            //services.AddSingleton(provider => provider.GetService<OrderContext>());

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddTransient<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
