using MongoDbGenericRepository.Attributes;

namespace LocalChatServerWeb.Models
{
    [CollectionName("Messages")]
    public class Message
    {
        public Guid Id { get; set; }
        public Session Session { get; set; }
        public ApplicationUser Sender { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
