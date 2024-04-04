using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class Content_Admin
    {
        [Key]
        public int contentadminid { get; set; }

        [Required]
        [StringLength(45)]
        public string contentadminname { get; set; }

        [Required]
        [StringLength(32)]
        public string ContentAdminUsername { get; set; }

        [ForeignKey("ContentAdminUsername")]
        public Users users { get; set; }
    }
}
