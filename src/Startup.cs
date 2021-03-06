// <auto-generated></auto-generated>
using Autofac;
using LiMeowApi.Filter;
using LiMeowApi.Middleware;
using LiMeowApi.Repository;
using LiMeowApi.Service.Implement;
using LiMeowApi.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace LiMeowApi
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
            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(ApiResultFilterAttribute));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddNewtonsoftJson(options =>
            {
                ////???Կ??ֶ?
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                ////????Ϊ???????ֶ?
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                ////????ĸСд
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                ////ö???????ַ???
                ////options.SerializerSettings.Converters.Add(new StringEnumConverter());
                ////????????
                options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
            });

            #region jwt
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.IncludeErrorDetails = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtSecret)),
                    ValidateIssuer = true,
                    ////??????
                    ValidIssuer = AppSettings.JwtIssuer,
                    ValidateAudience = false,
                    ////?Ƿ???֤??ʱ  ??????exp??nbfʱ??Ч ͬʱ????ClockSkew 
                    ValidateLifetime = true,
                    ////ע?????ǻ???????ʱ?䣬?ܵ???Чʱ??????????ʱ??????jwt?Ĺ???ʱ?䣬?????????ã?Ĭ????5????
                    ClockSkew = TimeSpan.FromMinutes(30),
                    RequireExpirationTime = true,
                };
                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // ???????ڣ?????<?Ƿ?????>???ӵ???????ͷ??Ϣ??
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                };
            });
            #endregion


            services.AddControllers();

            #region swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    Title = "???ٿ????ӿ?",
                    Contact = new OpenApiContact()
                    {
                        Name = "ydfk",
                        Email = "lyh6728326@gmail.com",
                    }
                });
                
                c.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme()
                    {
                        Description = "JWT??Ȩ(???ݽ???????ͷ?н??д???) ֱ?????¿???????Bearer {token}??ע??????֮????һ???ո???\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });

                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return  controllerAction.ControllerName+"-"+controllerAction.ActionName;
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
            #endregion swagger

            #region ????cors
            services.AddCors(c =>
            {
                ////һ?????????ַ???
                c.AddPolicy(
                    "LimitRequests",
                    policy =>
                    {
                        var corsOrigins = AppSettings.CorsOrigins;
                        ////????????
                        if (string.IsNullOrEmpty(corsOrigins) || corsOrigins == "*")
                        {
                            policy.SetIsOriginAllowed(_ => true)
                             .AllowAnyHeader()
                             .AllowAnyMethod().AllowCredentials();
                        }
                        else
                        {
                            var origins = new List<string>() { };

                            if (corsOrigins.Contains(","))
                            {
                                origins = corsOrigins.Split(",").ToList();
                            }
                            else
                            {
                                origins.Add(corsOrigins);
                            }

                            policy.WithOrigins(origins.ToArray())
                             .AllowAnyHeader()
                             .AllowAnyMethod().AllowCredentials();
                        }
                    });
            });

            #endregion

            #region redis
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(RedisUtil.GetRedisConfiguration());
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c => { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "???ٿ????ӿ?";
            });

            app.UseRouting();

            ////??֤
            app.UseAuthentication();
            ////??Ȩ
            app.UseAuthorization();

            ////????????
            app.UseCors("LimitRequests");

            ////???ش?????
            app.UseStatusCodePages();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        ///  IOC
        /// </summary>
        /// <param name="builder">ContainerBuilder</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(OtherService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .AsImplementedInterfaces();
        }
    }
}
