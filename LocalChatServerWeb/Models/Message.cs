using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;

namespace LocalChatServerWeb.Models
{
    [CollectionName("Messages")]
    public class Message : Document
    {
        //public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
    }
}
