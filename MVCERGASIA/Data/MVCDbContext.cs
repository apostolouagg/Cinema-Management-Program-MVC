using Microsoft.EntityFrameworkCore;
using MVCERGASIA.Models;

namespace MVCERGASIA.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {

        }

        //epeidh theloume sundiasthka kai ta 2 bazoume auto
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provoles>()
                .HasOne(p => p.Movies)
                .WithMany()
                .HasForeignKey(p => p.ProvolesMovieId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Provoles>()
                .HasOne(p => p.cinema)
                .WithMany()
                .HasForeignKey(p => p.CinemaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Provoles>()
                .HasOne(p => p.content_admin)
                .WithMany()
                .HasForeignKey(p => p.ContentAdminId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservations>()
                .HasOne(r => r.movies)
                .WithMany()
                .HasForeignKey(r => r.ReservationsMovieId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Reservations>().HasKey(key => new { key.ReservationsCinemaId, key.CustomersId });
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Cinemas> Cinemas { get; set; }
        public DbSet<Content_Admin> ContentAdmins { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Provoles> Provoles { get; set; }
        public DbSet<Reservations> Reservations { get; set; }

    }
}
