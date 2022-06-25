using AranumaSignalR.WebApi.Server.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.IO;

namespace AranumaSignalR.WebApi.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
        .UseKestrel(options =>
        {

            options.ListenLocalhost(5000);


            //// TCP 8007
            options.Listen(IPAddress.Any, 5060, builder =>
            {
                builder.UseHub<ChatHub>();
            });


            // HTTPS 5001
            //options.ListenLocalhost(5001, builder =>
            //{
            //    builder.UseHttps();
            //});
        })
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()

        .Build();

            host.Run();

            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseKestrel(options =>
                    //{
                    //    options.ListenLocalhost(5000);

                    //    // TCP 8007
                    //    options.Listen(IPAddress.Any, 5060, builder =>
                    //    {
                    //        builder.UseHub<ChatHub>();
                    //    });

                    //    // HTTP 5000
                    //    //options.ListenLocalhost(5005);

                    //    // HTTPS 5001
                    //    //options.ListenLocalhost(5001, builder =>
                    //    //{
                    //    //    builder.UseHttps();
                    //    //});
                    //}).UseStartup<Startup>();
                });
    }
}
