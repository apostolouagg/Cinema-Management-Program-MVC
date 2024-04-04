using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class DeleteContentAndCustomerViewModel
    {
        public int Did { get; set; }

        [Required]
        [StringLength(45)]
        public string Dname { get; set; }

        [StringLength(45)]
        public string Dpassword { get; set; }

        [StringLength(32)]
        public string Demail { get; set; }

        [StringLength(32)]
        [Required]
        public string AddDusername { get; set; }

        [StringLength(45)]
        [Required]
        public string Drole { get; set; }

        [Required]
        [StringLength(32)]
        public string adUsername { get; set; }
    }
}
