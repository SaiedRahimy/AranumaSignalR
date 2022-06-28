using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AranumaSignalR.Ids
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateHostBuilder(args).Build().Run();

            //var host = new WebHostBuilder()
            //   .UseKestrel(options =>
            //   {

            //       options.ListenLocalhost(5000);

            //       // HTTPS 5001
            //       options.ListenLocalhost(5001, builder =>
            //       {
            //           builder.UseHttps();
            //       });
            //   })
            //   .UseContentRoot(Directory.GetCurrentDirectory())
            //   .UseIISIntegration()
            //   .UseStartup<Startup>()
            //   .Build();

            //host.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
