using AspNetCore.CacheOutput.Extensions;
using AspNetCore.CacheOutput.InMemory.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using UserManagerNetCore.Infrastructure.Extensions;
using UserManagerNetCore.Infrastructure.Services;

namespace UserManagerNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) => services
                       .AddDatabase(this.Configuration)
                       .AddIdentity()
                       .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))
                       .AddApplicationServices()
                        .AddInMemoryCacheOutput()
                       .AddSwagger()
                       .AddAutoMapper(typeof(AutoMapperProfile).Assembly)
            .AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI()
                .UseRouting()
                .UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader())
                .UseAuthentication();

            app.UseHttpsRedirection();

           

            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                
            });

            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
                {
                    if (!context.HttpContext.Response.ContentLength.HasValue || context.HttpContext.Response.ContentLength == 0)
                    {
                        // You can change ContentType as json serialize
                       context.HttpContext.Response.ContentType = "application/json";
                        await context.HttpContext.Response.WriteAsync($"Status Code: {context.HttpContext.Response.StatusCode}");
                    }
                }
                else
                {
                    // You can ignore redirect
                    context.HttpContext.Response.Redirect($"/error?code={context.HttpContext.Response.StatusCode}");
                }
            });

            app.UseExceptionHandler("/api/errors/401");
            app.UseStatusCodePagesWithReExecute("/api/errors/{0}");

            app.UseRouting();
            app.UseAuthorization();
            app.UseCacheOutput();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}