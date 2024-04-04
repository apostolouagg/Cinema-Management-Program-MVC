using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCERGASIA.Models
{
    public class Reservations
    {
        [Key]
        public int ReservationsId {  get; set; }

        public int ReservationsMovieId { get; set; }

        [ForeignKey("ReservationsMovieId")]

        public Movies movies { get; set; }


        public int ReservationsCinemaId { get; set; }

        [ForeignKey("ReservationsCinemaId")]
        
        public Cinemas cinemas { get; set; }


        public int CustomersId { get; set; }

        [ForeignKey("CustomersId")]
        
        public Customers customers { get; set; }



        [StringLength(45)]
        public string ReservationsMovieName { get; set; }

        [Required]
        public DateTime reservations_time { get; set; }

        [Required]
        public int number_of_seats { get; set; }
    }
}
