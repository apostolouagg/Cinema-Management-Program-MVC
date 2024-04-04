using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class AddMovieViewModel
    {

        public int Moviesid { get; set; }

        [Required]
        [StringLength(45)]
        public string Moviename { get; set; }

        [Required]
        [StringLength(45)]
        public string Moviecontent { get; set; }

        public int Movielength { get; set; }

        [Required]
        [StringLength(45)]
        public string Movietype { get; set; }

        [Required]
        [StringLength(45)]
        public string Moviesummary { get; set; }

        [Required]
        [StringLength(45)]
        public string Moviedirector { get; set; }

        public int MovieContentAdminId { get; set; }

        public DateTime MovieTime { get; set; }

        public int MovieCinemaId { get; set; }

        [Required]
        public int MovieSeats { get; set; }
    }
}
