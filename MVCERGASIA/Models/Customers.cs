using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class Customers
    {
        [Key]
        public int customerid { get; set; }

        [Required]
        [StringLength(45)]
        public string customername { get; set; }

        [Required]
        [StringLength(32)]
        public string CustomerUsername { get; set; }

        [ForeignKey("CustomerUsername")]
        public Users users { get; set; }
    }
}
