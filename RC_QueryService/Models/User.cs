using System.ComponentModel.DataAnnotations;

namespace RCARS.Interface.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid UserGUID { get; set; }
    }
}