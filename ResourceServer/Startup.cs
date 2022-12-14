using BusinessLayer;
using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResourceServer.Models;
using System;

namespace ResourceServer
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
            var connection = Configuration.GetConnectionString("DefaultConnection");
            var identityConnection = Configuration.GetConnectionString("IdentityDbConnection");
            services.AddDbContext<DSDbContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 15)), b => b.MigrationsAssembly(nameof(DataLayer))));
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(identityConnection));

            services.AddTransient<ICleaningSpace, EFCleaningSpaceRepository>();
            services.AddTransient<ILoginInformation, EFLoginInformationRepository>();
            services.AddTransient<IRole, EFRoleRepository>();
            services.AddTransient<IUser, EFUserRepository>();
            services.AddTransient<IRoom, EFRoomRepository>();

            services.AddScoped<DataManager>();

            services.AddIdentity<UserIdentity, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{_id?}");
                /*endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller}/{action}/{id?}");*/
            });
        }
    }
}
