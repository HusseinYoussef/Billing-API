using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BillingApi.Data;
using BillingApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BillingApi
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if(_env.IsDevelopment())
            {
                services.AddDbContext<BillAppDbContext>(options => 
                        options.UseSqlite(_config.GetConnectionString("BillAppConnectionString"))
                );
            }
            else
            {
                services.AddDbContext<BillAppDbContext>(options => 
                        options.UseNpgsql(_config.GetConnectionString("BillAppConnectionString"))
                );
            }
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllers();
            services.AddScoped<IItemRepository, MockItemRepository>()
                    .AddScoped<ICartService, CartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMvc(route => 
                route.MapRoute("default", "{controller=Home}/{action=Index}/{id?}")
            );

            app.Run(async context => 
                await context.Response.WriteAsync("Hello World")
            );
        }
    }
}
