namespace LocalChatServerWeb.Models
{
    public class ChatViewModel // The usename of the session owner
    {
        public Session Session { get; set; }
        public string UserName_SessionOwner { get; set; }
        public string UserId_SessionOwner { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string CurrentMessage { get; set; }
        public List<Message> Messages { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

    }
}
