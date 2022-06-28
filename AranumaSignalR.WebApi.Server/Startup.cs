using AranumaSignalR.WebApi.Server.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using AranumaSignalR.WebApi.Server.Service;

namespace AranumaSignalR.WebApi.Server
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

            services.AddControllers();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:5002");
            }));



            //var guestPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
            //  .RequireClaim("scope", "myApi.read")
            //  .Build();

            //var tokenValidationParameters = new TokenValidationParameters()
            //{
            //    ValidIssuer = "http://localhost:5000/",
            //    ValidAudience = "AranumaCo",
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("signalRclientsAuth")),
            //    NameClaimType = "email",
            //    RoleClaimType = "role",

            //    //ValidIssuer = "http://localhost:5000/",
            //    //ValidAudience = "AranumaCo",
            //    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dataEventRecordsSecret")),
            //    //NameClaimType = "email",
            //    //RoleClaimType = "role",



            //};


            //var jwtSecurityTokenHandler = new JwtSecurityTokenHandler
            //{
            //    InboundClaimTypeMap = new Dictionary<string, string>()
            //};

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options =>
            //   {
            //       //options.Authority = "https://localhost:5001/";
            //       options.Authority = "http://localhost:5000/";
            //       options.Audience = "AranumaCo";                   
            //       options.IncludeErrorDetails = true;
            //       options.SaveToken = true;
            //       options.SecurityTokenValidators.Clear();
            //       options.SecurityTokenValidators.Add(jwtSecurityTokenHandler);
            //       options.TokenValidationParameters = tokenValidationParameters;
            //       options.Events = new JwtBearerEvents
            //       {
            //           OnMessageReceived = context =>
            //           {
            //               Console.WriteLine(JsonConvert.SerializeObject(context, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));


            //               if ((context.Request.Path.Value.StartsWith("/signalrhome")
            //                    || context.Request.Path.Value.StartsWith("/looney")
            //                    || context.Request.Path.Value.StartsWith("/usersdm")
            //                    || context.Request.Path.Value.StartsWith("/chat")
            //                   )
            //                    && context.Request.Headers.TryGetValue("Authorization", out StringValues token)
            //                //context.Request.Query.TryGetValue("token", out StringValues token)
            //                )
            //               {





            //                   context.Token = token;//.ToString().Replace("Bearer ", "");
            //               }

            //               return Task.CompletedTask;
            //           },
            //           OnAuthenticationFailed = context =>
            //           {
            //               var te = context.Exception;
            //               return Task.CompletedTask;
            //           }
            //       };
            //   });


            //services.AddAuthentication("Bearer")
            //  .AddIdentityServerAuthentication("Bearer", options =>
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
                {
                options.Audience = "https://localhost:5001/";
                    options.Authority = "https://localhost:5001/";
                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateIssuerSigningKey = true,
                    //    ValidateIssuer = true,
                    //    ValidateAudience = true,
                    //    ValidateLifetime = true,
                    //    IssuerSigningKey = "signalRclientsAuth",
                    //    ValidIssuer = "https://localhost:5001/",
                    //    ValidAudience = "https://localhost:5001/"
                    //};
                    options.Events = new JwtBearerEvents
                           {
                               OnMessageReceived = context =>
                               {
                                   //Console.WriteLine(JsonConvert.SerializeObject(context, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));


                                   if ((context.Request.Path.Value.StartsWith("/signalrhome")
                                        || context.Request.Path.Value.StartsWith("/looney")
                                        || context.Request.Path.Value.StartsWith("/usersdm")
                                        || context.Request.Path.Value.StartsWith("/chat")
                                       )
                                        && context.Request.Headers.TryGetValue("Authorization", out StringValues token)
                                    //context.Request.Query.TryGetValue("token", out StringValues token)
                                    )
                                   {





                                       context.Token = token.ToString().Replace("Bearer ", "");
                                   }

                                   return Task.CompletedTask;
                               },
                               OnAuthenticationFailed = context =>
                               {
                                   var te = context.Exception;
                                   return Task.CompletedTask;
                               }
                           };
                });

            services.AddSingleton<ITokenService, TokenService>();

            services.AddSignalR(options =>
            {
                // Faster pings for testing
                options.KeepAliveInterval = TimeSpan.FromSeconds(5);

            });
            //.AddMessagePackProtocol(); ;


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseSignalR(route => { route.MapHub<MessageHub>("/chat"); });


            app.UseAuthentication();
            app.UseAuthorization();

            //GlobalHost.HubPipeline.RequireAuthentication();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat", conf =>
                {
                    conf.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;

                });
            });


        }
    }
}
