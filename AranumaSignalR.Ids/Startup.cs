
using AranumaSignalR.Ids.Infr;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;




namespace AranumaSignalR.Ids
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(InMemoryConfig.GetClients())
                .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
                .AddInMemoryApiResources(InMemoryConfig.ApiResources())
                .AddInMemoryApiScopes(InMemoryConfig.ApiScopes())
                .AddTestUsers(InMemoryConfig.GetUsers())
                .AddDeveloperSigningCredential(); //not something we want to use in a production environment;




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

            app.UseIdentityServer();



            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });


            });
        }
    }
}
