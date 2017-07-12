using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Hubs;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;

namespace FundTrack.WebUI.Hubs
{
    /// <summary>
    /// Hub for super admin chat
    /// </summary>
    [HubName("SuperAdminChatHub")]
    public class SuperAdminChatHub : Hub
    {     
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
            Clients.All.OnMessageSent(new ChatMessageViewModel { Message = userMessage.Message });
        }        
    }
}
