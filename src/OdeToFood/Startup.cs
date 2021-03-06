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
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
using OdeToFood.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using OdeToFood.Middleware;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // identity
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options=>
            {
                _configuration.Bind("AzureAd", options);
            })     
            .AddCookie();

            services.AddSingleton<IGreeter, Greeter>();
            services.AddDbContext<OdeToFoodDbContext>(options=> options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));  //registering DbContext
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddMvc();
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


            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent()); //enabled ssl


            //app.UseFileServer(); // combintation of both UseDefaultFiles and UseStaticFiles

            // to server default content.. will be serving index.html
            //app.UseDefaultFiles();

            //static file middleware
            app.UseStaticFiles();

            //static file middleware
            app.UseNodeModules(env.ContentRootPath);



            app.UseAuthentication(); // asp.net identity middleware

            //mvc middleware with default route
            // app.UseMvcWithDefaultRoute();

            app.UseMvc(ConfigureRoutes);

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

            //app.Run(async (context) =>
            //{
            //    //throw new Exception("error!");

            //    var greeting = greeter.GetMessageOfTheDay();
            //    context.Response.ContentType = "text/plain"; //header
            //    await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            //conventional routing
            //match Home/Index/4 or Home/Index/

            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
