using System.Diagnostics;
using LocalChatServerWeb.Models;
using LocalChatServerWeb.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace LocalChatServerWeb.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MessageRepository messageRepository;
        public ChatHub(MessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }
        public async Task SendMessage(string sessionId, string user, string userId, string message)
        {
            var messageDb = new Message
            {
                SenderId = new Guid(userId),
                SenderName = user,
                SessionId = new Guid(sessionId), 
                Text = message
            };
            await messageRepository.CreateAsync(messageDb);
            await Clients.All.SendAsync("RecieveMessage", user, userId, message);
        }
    }
}
