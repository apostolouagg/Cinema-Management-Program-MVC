using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Models
{
    public class AssignScreeningViewModel
    {
        public int assignid { get; set; }

        public int assignMovieId { get; set; }
        public string assignMovieName { get; set; }

        public int assignCinemaId { get; set; }

        public DateTime assignprovolestime { get; set; }

        public int assignContentAdminId { get; set; }
    }
}
