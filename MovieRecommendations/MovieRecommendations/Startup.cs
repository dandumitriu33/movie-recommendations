using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Contexts;
using MoviesDataAccessLibrary.Repositories;
using AutoMapper;
using System.Reflection;
using MovieRecommendations.Models;

namespace MovieRecommendations
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
            services.AddDbContext<MoviesContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    // section just for edu purposes, defaults are the same
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 4; //default 6
                    options.Password.RequiredUniqueChars = 0; // default 1
                    options.Password.RequireDigit = false; // default true
                    options.Password.RequireNonAlphanumeric = false; // default true
                    options.Password.RequireLowercase = false; // default true
                    options.Password.RequireUppercase = false; //default true
                })
                .AddEntityFrameworkStores<MoviesContext>();
            services.AddScoped<IRepository, SQLRepository>();
            services.AddScoped<IPersonalizedRecommendationsBuilder, PersonalizedRecommendationsBuilder>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
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
            app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
