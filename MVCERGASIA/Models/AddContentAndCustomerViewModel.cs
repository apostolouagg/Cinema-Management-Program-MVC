using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class AddContentAndCustomerViewModel
    {
        public int Cid { get; set; }

        [Required]
        [StringLength(45)]
        public string Cname { get; set; }

        [StringLength(45)]
        public string Cpassword { get; set; }

        [StringLength(32)]
        public string Cemail { get; set; }

        [StringLength(32)]
        [Required]
        public string AddCusername { get; set; }

        [Required]
        [StringLength(32)]
        public string adUsername { get; set; }
    }
}
