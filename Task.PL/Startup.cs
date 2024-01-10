using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
 
using TaskApp.BLL.Interfaces;
using TaskApp.BLL.Repositories;
using TaskApp.DAL.Contexts;
using TaskApp.PL.Helper;

namespace TaskApp.PL
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
            services.AddControllersWithViews();
            services.AddDbContext<MvcAppDbContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddAutoMapper(profile => profile.AddProfile(new DepartmentProfile()));
            //services.AddAutoMapper(typeof(DepartmentProfile));

            //services.AddAutoMapper(profile => profile.AddProfile(new EmployeeProfile()));
            //services.AddAutoMapper(typeof(EmployeeProfile));

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //    {
            //        options.LoginPath = new PathString("/Account/Login");
            //        options.AccessDeniedPath = new PathString("/Shared/Error");

            //    });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {


                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequiredLength = 5;
                //options.SignIn.RequireConfirmedAccount = false;

            })
                .AddEntityFrameworkStores<MvcAppDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);
        }
    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                    IdentityConfig.SeedBasicUser(userManager).Wait();

                }

                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger<MvcAppDbContext>();
                    logger.LogError(ex.Message, "An Error Occured While Seeding Data!!");
                }
            }




            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=SignIn}/{id?}");
            });
        }
    }
}
