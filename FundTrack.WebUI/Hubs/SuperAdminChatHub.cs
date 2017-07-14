/*using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Hubs;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
using FundTrack.Infrastructure.Models;
using System.Collections.Generic;

namespace FundTrack.WebUI.Hubs
{
    /// <summary>
    /// Hub for super admin chat
    /// </summary>
    [HubName("SuperAdminChatHub")]
    public class SuperAdminChatHub : Hub
    {
        public static List<SuperAdminChatUserModel> Users { get; } = new List<SuperAdminChatUserModel>();

        private bool UserExists(string connectionId) => Users.Find(u => u.ConnectionId == connectionId) == null ? false : true;

        private string _getSuperAdminConnectionId() => Users.Find(u => u.UserName == "SuperAdmin").UserName;

        private string _getUserConnectionId(string userName) => Users.Find(u => u.UserName == userName).ConnectionId;

        /// <summary>
        /// Called when the user is connected
        /// </summary>
        /// <returns>Task for connected user</returns>
        public override Task OnConnected()
        {                    
            return base.OnConnected();
        }
        
        /// <summary>
        /// Send Message to all users in the chat
        /// </summary>
        /// <param name="message">Message to send</param>
        public void Chat(ChatMessageViewModel userMessage)
        {          
            if (!UserExists(userMessage.ConnectionId))
            {
                Users.Add(new SuperAdminChatUserModel { UserName = userMessage.Login, ConnectionId = userMessage.ConnectionId });
            }

            Clients.Client(_getSuperAdminConnectionId()).OnMessageSent(new ChatMessageViewModel { Message = userMessage.Message });
        }

        /// <summary>
        /// Send Message to User
        /// </summary>
        /// <param name="userMessage">Message from user</param>
        public void SuperAdminMessage(ChatMessageViewModel userMessage)
        {
            Clients.Client(_getUserConnectionId(userMessage.Login)).OnSuperAdminMessageSend(new ChatMessageViewModel { Message = userMessage.Message });
        }
    }
}*/
