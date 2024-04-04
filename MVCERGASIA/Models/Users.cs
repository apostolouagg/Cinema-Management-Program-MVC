using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class Users
    {
        [Key]
        [StringLength(32)]
        public string Username { get; set; }

        [StringLength(32)]
        public string Email { get; set; }

        [StringLength(45)]
        public string Password { get; set; }

        [StringLength(45)]
        public string Role { get; set; }
    }
}
