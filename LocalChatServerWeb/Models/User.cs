using System.ComponentModel.DataAnnotations;

namespace LocalChatServerWeb.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
