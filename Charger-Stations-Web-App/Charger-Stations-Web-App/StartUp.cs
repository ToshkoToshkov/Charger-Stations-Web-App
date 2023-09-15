//namespace Charger_Stations_Web_App
//{
//    using Charger_Stations_Web_App.Data;
//    using Charger_Stations_Web_App.Infrastructure;
//    using Microsoft.AspNetCore.Builder;
//    using Microsoft.AspNetCore.Hosting;
//    using Microsoft.AspNetCore.Identity;
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.Extensions.Configuration;
//    using Microsoft.Extensions.DependencyInjection;
//    using Microsoft.Extensions.Hosting;

//    public class StartUp
//    {
//        public StartUp(IConfiguration configuration)
//           => this.Configuration = configuration;

//        public IConfiguration Configuration { get; }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            services
//                .AddDbContext<ChargerStationsDbContext>(options => options
//                .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

//            services.AddDatabaseDeveloperPageExceptionFilter();

//            services
//                .AddDefaultIdentity<IdentityUser>(options =>
//                {
//                    options.Password.RequireDigit = false;
//                    options.Password.RequireLowercase = false;
//                    options.Password.RequireNonAlphanumeric = false;
//                    options.Password.RequireUppercase = false;
//                })
//                .AddEntityFrameworkStores<ChargerStationsDbContext>();

//            services.AddControllersWithViews();
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {

//            app.PrepareDatabase();

//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                app.UseMigrationsEndPoint();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection()
//               .UseStaticFiles()
//               .UseRouting()
//               .UseAuthentication()
//               .UseAuthorization()
//               .UseEndpoints(endpoints =>
//               {
//                   endpoints.MapDefaultControllerRoute();
//                   endpoints.MapRazorPages();
//               });
//        }
//    }
//}
