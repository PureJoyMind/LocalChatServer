using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;

namespace LocalChatServerWeb.Models
{
    [CollectionName("Messages")]
    public class Message : Document
    {
        //public Guid Id { get; set; }
        public Session Session { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}
