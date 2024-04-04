using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCERGASIA.Models
{
    public class Provoles
    {
        [Key]
        [StringLength(45)]
        public int provolesid { get; set; }

        public int ProvolesMovieId { get; set; }
        public string MovieName { get; set; }

        public Movies Movies { get; set; }

        public int CinemaId { get; set; }

        [ForeignKey("CinemaId")]
        public Cinemas cinema { get; set; }

        public DateTime provoles_time { get; set; }

        public int ContentAdminId { get; set; }

        [ForeignKey("ContentAdminId")]
        public Content_Admin content_admin { get; set; }
    }
}
