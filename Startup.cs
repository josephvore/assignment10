using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment10.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace assignment10
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
            services.AddControllersWithViews();

            //db context
            services.AddDbContext<BowlingLeagueContext>(options =>
                options.UseSqlite(Configuration["ConnectionStrings:BowlingLeagueDbConnection"]));
        }

        // HTTP Request Pipeline
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("teampagenum",
                "Team/{id}/{team}/{pageNum}",
                    new { Controller = "Home", Action = "Index" }
                    );

                endpoints.MapControllerRoute("id",
                    "Team/{id}/{team}",
                    new { Controller = "Home", action = "Index" }
                    );

                endpoints.MapControllerRoute("pageNum",
                    "Page/{pageNum}",
                    new { Controller = "Home", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
