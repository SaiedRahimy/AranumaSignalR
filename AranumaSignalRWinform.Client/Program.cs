using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AranumaSignalRWinform.Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var app = new CommandLineApplication();
            app.FullName = "SignalR Client Samples";
            app.Description = "Client Samples for SignalR";

            RawSample.Register(app);
            HubSample.Register(app);

            app.Command("help", cmd =>
            {
                cmd.Description = "Get help for the application, or a specific command";

                var commandArgument = cmd.Argument("<COMMAND>", "The command to get help for");
                cmd.OnExecute(() =>
                {
                    app.ShowHelp(commandArgument.Value);
                    return 0;
                });
            });

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });

            app.Execute(new string[] { "hub" });
            //app.Execute(args);


            Application.Run(new Form1());
        }
    }
}
