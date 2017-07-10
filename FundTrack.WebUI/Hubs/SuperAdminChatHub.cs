using FundTrack.Infrastructure.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundTrack.WebUI.Hubs
{
    /// <summary>
    /// Hub for super admin chat
    /// </summary>
    public class SuperAdminChatHub : Hub
    {
        // connected users
        private static readonly List<SuperAdminChatUserModel> _users = new List<SuperAdminChatUserModel>();

        /// <summary>
        /// Checks if the user is connected
        /// </summary>
        /// <param name="user">User to check</param>
        /// <returns>User connecting status</returns>
        bool IsUserConnected(SuperAdminChatUserModel user)
        {
            var curId = Context.ConnectionId;

            var userName = _users.FirstOrDefault(x => x.ConnectionId == curId).UserName;

            user.UserName = userName;
            user.ConnectionId = curId;
            return (!String.IsNullOrWhiteSpace(userName));
        }

        /// <summary>
        /// Sends message to user
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="message">Message to send</param>
        /// <returns>Message</returns>
        public async Task SendMessage(string userName, string message)
        {
            var user = new SuperAdminChatUserModel();
            if (IsUserConnected(user))
            {
                return;
            }

            await Clients.All.addMessage(user.UserName, message, user.ConnectionId);
        }

        /// <summary>
        /// Connects new user
        /// </summary>
        /// <param name="userName">Name of User</param>
        /// <returns>Connected User</returns>
        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!_users.Any(x => x.ConnectionId == id))
            {
                _users.Add(new SuperAdminChatUserModel { ConnectionId = id, UserName = userName });
                
                await Clients.Caller.onConnected(id, userName,_users);
               
                await Clients.AllExcept(id).onNewUserConnected(id, userName);               
            }
        }

        /// <summary>
        /// Disconects user
        /// </summary>
        /// <param name="stopCalled">Indicator for caller stopping</param>
        /// <returns>Disconected user</returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            var item = _users.FirstOrDefault(x => x.ConnectionId == id);
            if (item != null)
            {
                _users.Remove(item);

                Clients.All.onUserDisconnected(id, item.UserName);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
