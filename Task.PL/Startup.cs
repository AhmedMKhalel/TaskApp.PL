using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using DevExpress.AspNetCore;
using System;
using System.IO;

using TaskApp.BLL.Interfaces;
using TaskApp.BLL.Repositories;
using TaskApp.DAL.Contexts;
using TaskApp.PL.Helper;
using DevExpress.AspNetCore.Reporting;
using Microsoft.Extensions.FileProviders;
using TaskApp.PL.Models;
using TaskApp.DAL.Entities;
using TaskApp.PL.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace TaskApp.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MvcAppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<MvcAppDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider)
            .AddDefaultTokenProviders();

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddDevExpressControls();

            services.ConfigureReportingServices(configurator =>
            {
                configurator.DisableCheckForCustomControllers();
            });


            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Configure additional services if needed
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    IdentityConfig.SeedBasicUser(userManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<MvcAppDbContext>();
                    logger.LogError(ex.Message, "An Error Occurred While Seeding Data!!");
                }
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDevExpressControls();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=SignIn}/{id?}");
            });
        }
    }
}
