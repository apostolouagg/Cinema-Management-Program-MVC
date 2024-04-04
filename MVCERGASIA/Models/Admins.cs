using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class Admins
    {
        [Key]
        public int adminid { get; set; }

        [Required]
        [StringLength(45)]
        public string adminname { get; set; }

        [Required]
        [StringLength(32)]
        public string AdminsUsername { get; set; }

        [ForeignKey("AdminsUsername")]
        public Users users { get; set; }
    }
}
