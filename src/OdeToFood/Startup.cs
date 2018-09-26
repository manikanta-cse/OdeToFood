﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IGreeter greeter, ILogger<Startup> logger)
        {
            //order of middleware is important

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }


            //app.UseFileServer(); // combintation of both UseDefaultFiles and UseStaticFiles

            // to server default content.. will be serving index.html
            app.UseDefaultFiles();

            //static file middleware

            app.UseStaticFiles();

            ////custom middleware

            //app.Use(next =>
            //{

            //    return async context =>
            //    {
            //        logger.LogInformation("Request incoming");

            //        if (context.Request.Path.StartsWithSegments("/mym"))
            //        {
            //            await context.Response.WriteAsync("Hit!!");
            //            logger.LogInformation("Request handled");

            //        }else
            //        {
            //            await next(context);
            //            logger.LogInformation("Respone outgoing");
            //        }
            //    };

            //});

            ////middleware
            //app.UseWelcomePage(new WelcomePageOptions {

            //    Path="/wp"  //route

            //});

            app.Run(async (context) =>
            {
                //throw new Exception("error!");

                var greeting = greeter.GetMessageOfTheDay();
                await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
            });
        }
    }
}
