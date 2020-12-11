using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DatabaseLayer;
using MediatR;
using DatabaseLayer.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FlightService
{
    public class Startup
    {
        readonly string AllowedOrigins = "_allowedOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.
            SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins, builder =>
                {
                    //builder.WithOrigins("http://localhost:3000").WithHeaders(HeaderNames.ContentType, "Access-Control-Allow-Origin").AllowAnyMethod();
                    //builder.WithOrigins("http://172.25.48.1:3000").AllowAnyHeader().AllowAnyMethod();
                    builder.WithOrigins("http://172.25.48.1:3000").AllowAnyHeader().AllowAnyMethod();
                });
            });

            
            services.AddDbContext<DataContext>(options =>
            {

                options.UseSqlServer(Configuration.GetSection("MyConnectionString").Value);
            });

           
            services.AddTransient<IFlightBusiness, LuggageLocationBusiness>(function => new LuggageLocationBusiness(new LuggageAndDestinationDatabase()));
            services.AddMediatR(typeof(Startup));

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(AllowedOrigins);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
