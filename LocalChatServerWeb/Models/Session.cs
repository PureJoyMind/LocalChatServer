using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;

namespace LocalChatServerWeb.Models
{
    [CollectionName("Sessions")]
    public class Session : Document
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StopTime { get; set; }
        public Guid Creator { get; set; }
        public bool State { get; set; }
        public List<Guid> ActiveUsers { get; set; }
    }
}
