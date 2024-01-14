using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace LocalChatServerWeb.Models
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {

    }
}
