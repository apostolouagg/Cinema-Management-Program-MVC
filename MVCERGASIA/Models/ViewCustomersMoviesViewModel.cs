using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class ViewCustomersMoviesViewModel
    {
        public int movieId { get; set; }

        public string moviename { get; set; }

        public int movielength { get; set; }

        public DateTime movieTime { get; set; }

        public string movieCinemaName { get; set; }

        public string customerUsername { get; set; }

        public int tickets { get; set; }

        public int ticketstotake { get; set; }
    }
}
