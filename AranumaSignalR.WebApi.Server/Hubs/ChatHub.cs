using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AranumaSignalR.WebApi.Server.Hubs
{
    public class ChatHub : Hub
    {
        
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            
            Console.WriteLine(name + ": " + message);
            await Clients.All.SendAsync("alert", name, message);
        }

        public async Task SendPrivate(string name, string message)
        {
            // Call the broadcastMessage method to update clients.

            Console.WriteLine(name + ": " + message);
            await Clients.User(name).SendAsync("alert", name, message);
        }
    }
}
