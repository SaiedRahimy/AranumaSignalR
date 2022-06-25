using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AranumaSignalR.WebApi.Server.Hubs
{
    public class ChatHub : Hub
    {

        public async Task Identification(string name)
        {
            // Send Response to Caller Client.

            
            var message = "Hello " + name + Environment.NewLine + "Your ConnectionId is :" + Context.ConnectionId;
            Console.WriteLine(message);

            await Clients.Caller.SendAsync("identificationResponse", message);
            

        }

        public void Logout()
        {
            // Send Response to Caller Client.
            Context.Abort();
        }



        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.

            Console.WriteLine(name + ": " + message);
            //await Clients.All.SendAsync("alert", name, message);
            await Clients.Clients(Context.ConnectionId).SendAsync("recive", name, message);
            
        }



        public async Task SendPrivate(string name, string message)
        {
            // Call the broadcastMessage method to update clients.

            Console.WriteLine(name + ": " + message);
            await Clients.User(name).SendAsync("alert", name, message);
        }
    }
}
