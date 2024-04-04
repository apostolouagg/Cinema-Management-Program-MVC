using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCERGASIA.Models
{
    public class Movies
    {
        [Key]
        public int moviesid { get; set; }

        [Required]
        [StringLength(45)]
        public string moviename { get; set; }

        [Required]
        public string moviecontent { get; set; }

        public int movielength { get; set; }

        [Required]
        [StringLength(45)]
        public string movietype { get; set; }

        [Required]
        public string moviesummary { get; set; }

        [Required]
        [StringLength(45)]
        public string moviedirector { get; set; }

        [Required]
        public int movieseats { get; set; }

        public int ContentAdminId { get; set; }

        [ForeignKey("ContentAdminId")]
        public Content_Admin content_admin { get; set; }


    }
}
