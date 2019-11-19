using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContradoECommerce.DataAccess;
using ContradoECommerce.DataAccess.Interface;
using ContradoECommerce.Service;
using ContradoECommerce.Shared.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ContradoECommerce.Api
{
    public class Startup
    {
        private string _strConnectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("AllowAllHeaders",
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddControllers();

            services.AddScoped<IProductAttributeLookupService, ProductAttributeLookupService>();
            services.AddScoped<IProductService, ProductService>();

            IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
            if (appSettingsSection != null)
            {
                _strConnectionString = appSettingsSection.GetValue<string>("ContradoECommerceDatabaseConnectionString");
                services.AddSingleton<ISqlService>(sqlService => new SqlService(_strConnectionString));
            }
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
            app.UseCors("AllowAllHeaders");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
