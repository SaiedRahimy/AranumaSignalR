using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AranumaSignalR.WebApi.Server.Hubs
{

    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize]
    public class ChatHub : Hub
    {


        public override Task OnConnectedAsync()
        {
            if (Context != null)
            {
                var message = "Client On Connected by ConnectionId  :" + Context.ConnectionId;
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Client Connected...");
            }

            return base.OnConnectedAsync();
        }
       

        /// <summary>
        /// Identification Client/Caller
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task Identification(string name)
        {
            // Send Response to Caller Client.


            var message = "Hello " + name + Environment.NewLine + "Your ConnectionId is :" + Context.ConnectionId;
            Console.WriteLine(message);

            await Clients.Caller.SendAsync("identificationResponse", message);


        }

        /// <summary>
        /// Logout SpecialClient
        /// </summary>
        public void Logout()
        {
            // Send Response to Caller Client.
            Context.Abort();
        }

        /// <summary>
        /// Logout Client/Caller
        /// </summary>
        public void LogoutSpecialClient()
        {
            // Send Response to Caller Client.
            Context.Abort();
            //Connections
            //Clients.Client("").Clo
        }

        /// <summary>
        /// Sent to Caller
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Send(string name, string message)
        {
            // Sent to Caller
            Console.WriteLine(name + ": " + message);
            await Clients.Clients(Context.ConnectionId).SendAsync("recive", name, message);

        }

        /// <summary>
        /// Sent to Special Client
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToSpecialClient(string connectionId, string message)
        {
            // Sent to Caller
            Console.WriteLine("Send message to ( " + connectionId + " ) :" + message);
            await Clients.Clients(connectionId).SendAsync("recive", "Server", message);

        }

        /// <summary>
        /// Send to All Clients
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToAll(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Console.WriteLine(name + ": " + message);
            await Clients.All.SendAsync("alert", name, message);
        }

        /// <summary>
        /// Send to All Client Except Some Connections
        /// </summary>
        /// <param name="excludedConnectionIds"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToAllExcept(IReadOnlyList<string> excludedConnectionIds, string message)
        {
            // Send Message to all clients except some Connection ids
            Console.WriteLine("Send To all Except " + String.Join(" , ", excludedConnectionIds.ToList()) + ": " + message);
            await Clients.AllExcept(excludedConnectionIds).SendAsync("groupsMessage", message);

        }

        /// <summary>
        /// Add Caller to Group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task AddToGroup(string groupName)
        {
            // Join Caller to Group
            await AddSpecialClientToGroup(this.Context.ConnectionId, groupName);

        }

        /// <summary>
        /// Add Special Client to Group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task AddSpecialClientToGroup(string connectionId, string groupName)
        {
            // Join Caller to Group
            Console.WriteLine(connectionId + " Request Join to Group ( " + groupName + " )");
            await Groups.AddToGroupAsync(connectionId, groupName);

        }


        /// <summary>
        /// Remove Caller from Group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task RemoveFromGroup(string groupName)
        {
            // Remove Caller From Group.            
            await RemoveSpecialClientFromGroup(this.Context.ConnectionId, groupName);

        }

        /// <summary>
        /// Remove Special Client from Group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task RemoveSpecialClientFromGroup(string connectionId, string groupName)
        {
            // Remove Special Client From Group.
            Console.WriteLine(connectionId + " Request Remove From Group ( " + groupName + " )");
            await Groups.RemoveFromGroupAsync(connectionId, groupName);

        }


        /// <summary>
        /// Send Message to Group
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToGroup(string groupName, string message)
        {
            // Send Message to Group
            Console.WriteLine("SendToGroup( " + groupName + " ): " + message);
            await Clients.Group(groupName).SendAsync("groupMessage", groupName, message);

        }

        /// <summary>
        /// Send Message to Group Except Some Connection
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="excludedConnectionIds"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToGroupExcept(string groupName, IReadOnlyList<string> excludedConnectionIds, string message)
        {
            // Send Message to Group excluding the specified connections
            Console.WriteLine("Send To Group Except ( " + groupName + " ): " + message);
            await Clients.GroupExcept(groupName, excludedConnectionIds).SendAsync("groupMessage", groupName, message);

        }


        /// <summary>
        /// Send Message To Groups
        /// </summary>
        /// <param name="groupNames"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToGroups(IEnumerable<string> groupNames, string message)
        {
            // Send Message to Groups
            Console.WriteLine("Send To Groups ( " + String.Join(" , ", groupNames.ToList()) + " ): " + message);
            await Clients.Groups(groupNames).SendAsync("groupsMessage", message);

        }

        /// <summary>
        /// When Connection is Disconnect this method calling
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context != null)
            {
                var message = "ConnectionId  :" + Context.ConnectionId + ", Disconnected";
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Client Disconnected.");
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendPrivate(string name, string message)
        {
            // Call the broadcastMessage method to update clients.

            Console.WriteLine(name + ": " + message);
            await Clients.User(name).SendAsync("alert", name, message);
        }
    }
}
