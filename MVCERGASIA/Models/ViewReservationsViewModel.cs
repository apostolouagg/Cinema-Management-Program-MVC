using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class ViewReservationsViewModel
    {
        public string VreservationsMovieName { get; set; }

        public DateTime Vreservations_time { get; set; }

        public int Vnumber_of_seats { get; set; }

        public string VreservationsCinemaName { get; set; }
    }
}
