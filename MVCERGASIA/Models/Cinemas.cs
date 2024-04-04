using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class Cinemas
    {
        [Key]
        public int cinemaid { get; set; }

        [Required]
        [StringLength(45)]
        public string cinemaname { get; set; }

        /*[Required]
        [StringLength(45)]
        public string cinemaseats { get; set; }*/

        [Required]
        [StringLength(45)]
        public string cinema_3d { get; set; }
    }
       
}
