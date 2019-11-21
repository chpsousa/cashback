using Cashback.Domain;
using Cashback.Domain.Commands;
using Cashback.Domain.Events;
using Cashback.Domain.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Cashback.API
{
    public class Startup
    {
        public static string Environment { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CashbackDbContext>(opt => opt.UseInMemoryDatabase());

            //services.AddDbContext<CashbackDbContext>(options => options
            //    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            //    .EnableSensitiveDataLogging()
            //    .ConfigureWarnings(warnings => warnings
            //        .Throw(CoreEventId.IncludeIgnoredWarning)
            //        .Throw(RelationalEventId.QueryClientEvaluationWarning)));

            services.AddScoped<CashbackCommandsHandler>();
            services.AddScoped<CashbackQueriesHandler>();
            services.AddScoped<EventsHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "Cashback API", Version = "v1" });
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Environment = env.EnvironmentName;
            CashbackStartup.Configure(Configuration.GetConnectionString("CashbackDb"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cashback API.v1");
                c.RoutePrefix = string.Empty;
            });

            // Enabling Cross-Origin Requests
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
