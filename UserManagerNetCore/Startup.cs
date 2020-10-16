using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using UserManagerNetCore.Infrastructure.Extensions;

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
                       .AddSwaggerGen(s =>
                       {
                           s.SwaggerDoc("v1", new OpenApiInfo
                           {
                               Version = "v1",
                               Title = "User management",
                               Description = "My Api",
                               Contact = new OpenApiContact
                               {
                                   Name = "Lê Vĩnh Hảo",
                                   Email = "vinhhao2604@gmail.com",
                                   Url = new Uri("https://Fb.com/haole2604")
                               },
                               License = new OpenApiLicense
                               {
                                   Name = "MIT",
                                   Url = new Uri("https://github.com/ignaciojvig/ChatAPI/blob/master/LICENSE")
                               }

                           });

                           s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                           {
                               Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                               Name = "Authorization",
                               In = ParameterLocation.Header,
                               Type = SecuritySchemeType.ApiKey,
                               Scheme = "Bearer"
                           });

                           s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

                       })
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

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}