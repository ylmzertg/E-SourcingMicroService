using ESourcing.Core.Entities;
using ESourcing.Infrastructure.Data;
using ESourcing.UI.ApiExtension;
using ESourcing.UI.ApiExtension.Interfaces;
using ESourcing.UI.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESourcing.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<Employee, IdentityRole>
                (opt =>
                {
                    opt.Password.RequiredLength = 4;
                    opt.Password.RequireNonAlphanumeric = false;//* gibi karakterler gýrýlsýn ýstemýyrm
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireDigit = false;
                })
                .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<WebAppContext>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc();

            #region Project Dependencies

            // add for httpClient factory
            services.AddHttpClient();

            // add api dependecy
            //services.AddTransient<IAuctionApi, AuctionApi>();
            //services.AddTransient<IBidApi, BidApi>();
            //services.AddTransient<IOrderApi, OrderApi>();
            //services.AddTransient<IProductApi, ProductApi>();

            #endregion

            #region ClienDependencies

            services.AddHttpClient<AuctionClient>();
            services.AddHttpClient<ProductClient>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
