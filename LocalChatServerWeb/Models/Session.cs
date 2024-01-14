using MongoDbGenericRepository.Attributes;

namespace LocalChatServerWeb.Models
{
    [CollectionName("Sessions")]
    public class Session
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public ApplicationUser Creator { get; set; }
        public bool State { get; set; }
        public List<ApplicationUser> ActiveUsers { get; set; }
    }
}
