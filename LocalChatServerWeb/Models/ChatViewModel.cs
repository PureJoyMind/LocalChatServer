namespace LocalChatServerWeb.Models
{
    public class ChatViewModel // The usename of the session owner
    {
        public Session Session { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<Message> Messages { get; set; }

    }
}
