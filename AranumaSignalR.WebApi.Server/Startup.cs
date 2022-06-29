using AranumaSignalR.WebApi.Server.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using IdentityServer4.AccessTokenValidation;
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
            

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
                {                
                    options.Authority = "http://localhost:5000/";
                    options.RequireHttpsMetadata = false;   
                    options.Audience = "Aranuma.SignalR.Api";
                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateIssuerSigningKey = true,
                    //    ValidateIssuer = true,
                    //    ValidateAudience = true,
                    //    ValidateLifetime = true,
                    //    IssuerSigningKey = new SymmetricSecurityKey(new HMACSHA256(Encoding.UTF8.GetBytes("signalRclientsAuth")).Key),
                    //    ValidIssuer = "https://localhost:5001/",
                    //    ValidAudience = "AranumaCo"
                    //};

                });
            
            services.AddSingleton<ITokenService, TokenService>();

            services.AddSignalR(options =>
            {
                // Faster pings for testing
                options.KeepAliveInterval = TimeSpan.FromSeconds(5);

            });
           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat", conf =>
                {
                    conf.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;

                });
                //.RequireAuthorization(); //For authorize hub un comment this but include anymous methods comment .RequireAuthorization() and use [Authorize] in hub
            });


        }
    }
}
