using ESourcing.Order.Consumers;
using ESourcing.Order.Extensions;
using EventBusRabbitMQ;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ordering.Application;
using Ordering.Application.Handlers;
using Ordering.Infrastructure;
using RabbitMQ.Client;
using System.Reflection;

namespace ESourcing.Order
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
            services.AddControllers();

            #region Add Infrastructure

            services.AddInfrastructure(Configuration);

            #endregion

            #region Add Application

            services.AddApplication();

            #endregion

            #region SqlServer Dependencies

            //services.AddDbContext<OrderContext>(c =>
            //    c.UseSqlServer(Configuration.GetConnectionString("OrderConnection")), ServiceLifetime.Singleton); // we made singleton this in order to resolve in mediatR when consuming Rabbit

            #endregion

            #region Project Dependencies

            //// Add Infrastructure Layer
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            //services.AddTransient<IOrderRepository, OrderRepository>(); // we made transient this in order to resolve in mediatR when consuming Rabbit

            //// Add AutoMapper
            //services.AddAutoMapper(typeof(Startup));

            //// Add MediatR
            //services.AddMediatR(typeof(OrderCreateHandler).GetTypeInfo().Assembly);
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            #endregion

            #region RabbitMQ Dependencies

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };

                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                {
                    factory.Password = Configuration["EventBus:Password"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddSingleton<EventBusOrderCreateConsumer>();

            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRabbitListener();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
            });
        }
    }
}
