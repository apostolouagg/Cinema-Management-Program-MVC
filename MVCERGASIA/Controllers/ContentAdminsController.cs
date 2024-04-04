using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using MVCERGASIA.Data;
using MVCERGASIA.Models;

namespace MVCERGASIA.Controllers
{
    public class ContentAdminsController : Controller
    {
        private readonly MVCDbContext _dbContext;

        public ContentAdminsController(MVCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Users");
        }

        public async Task<IActionResult> ContentAdminsIndex(Content_Admin contentadmin)
        {
            ViewBag.ContentAdmins = contentadmin.ContentAdminUsername;

            List<Movies> mov = new List<Movies>();
            mov = await _dbContext.Movies.ToListAsync();

            List<AddMovieViewModel> mov2 = new List<AddMovieViewModel>();

            foreach (Movies m in mov)
            {
                var model = new AddMovieViewModel()
                {
                    Moviesid = m.moviesid,
                    Moviename = m.moviename,
                    Moviecontent = m.moviecontent,
                    Movielength = m.movielength,
                    Movietype = m.movietype,
                    Moviesummary = m.moviecontent,
                    Moviedirector = m.moviedirector,
                    MovieContentAdminId = m.ContentAdminId,
                    MovieSeats = m.movieseats
                };

                mov2.Add(model);
            }

            ViewBag.Movies = mov2;

            return View();

        }

        public IActionResult AddPerson(LoginViewModel addperson)
        {
            return RedirectToAction("AddIndex", "Add", addperson);

        }

        [HttpGet]
        public async Task<IActionResult> ViewMovieIndex(AddMovieViewModel movie)
        {
            List<Provoles> provoles = new List<Provoles>();
            provoles = await _dbContext.Provoles.ToListAsync();
            ViewBag.provoles = provoles;

            List<Cinemas> cinema = new List<Cinemas>();
            cinema = await _dbContext.Cinemas.ToListAsync();
            ViewBag.cinema = cinema;

            var m = await _dbContext.Movies.FirstOrDefaultAsync(x => x.moviesid == movie.Moviesid);

            if (m != null)
            {
                var s = await _dbContext.Provoles.FirstOrDefaultAsync(y => y.MovieName == m.moviename);
                var s2 = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.cinemaid == s.CinemaId);

                var viewSelected = new AddMovieViewModel()
                {
                    Moviesid = m.moviesid,
                    Moviename = m.moviename,
                    Moviecontent = m.moviecontent,
                    Movielength = m.movielength,
                    Movietype = m.movietype,
                    Moviesummary = m.moviesummary,
                    Moviedirector = m.moviedirector,
                    MovieContentAdminId = m.ContentAdminId,
                    MovieTime = s.provoles_time,
                    MovieCinemaId = s2.cinemaid,
                    MovieSeats = m.movieseats
                };

                ViewBag.assignedPro = s.provoles_time;
                ViewBag.assignedCin = s.CinemaId;
                ViewBag.ContentAdmins = movie.MovieContentAdminId;
                ViewBag.assignedCinName = s2.cinemaname;

                return View(viewSelected);
            }

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> AssignMovieIndex(AddMovieViewModel addMovieViewModel)
        {
            var previousmovie = await _dbContext.Provoles.FirstOrDefaultAsync(x => x.ProvolesMovieId == addMovieViewModel.Moviesid);

            List<Reservations> oldres = new List<Reservations>();
            oldres = await _dbContext.Reservations.ToListAsync();

            List<Reservations> newres = new List<Reservations>();


            if (previousmovie != null)
            {
                var nextmovie = await _dbContext.Provoles.FirstOrDefaultAsync(
                    x => x.provoles_time == addMovieViewModel.MovieTime && x.CinemaId == addMovieViewModel.MovieCinemaId);

                var newcinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.cinemaid == addMovieViewModel.MovieCinemaId);

                var temp1 = previousmovie.provoles_time;
                previousmovie.provoles_time = nextmovie.provoles_time;
                nextmovie.provoles_time = temp1;

                if (nextmovie.CinemaId == newcinema.cinemaid)
                {
                    var temp2 = previousmovie.CinemaId;
                    previousmovie.CinemaId = nextmovie.CinemaId;
                    nextmovie.CinemaId = temp2;

                    //gia pinaka reservations
                    var previousreservation = await _dbContext.Reservations.FirstOrDefaultAsync(x => x.ReservationsMovieId == addMovieViewModel.Moviesid);
                    var nextreservation = await _dbContext.Reservations.FirstOrDefaultAsync(
                        x => x.reservations_time == addMovieViewModel.MovieTime && x.ReservationsCinemaId == addMovieViewModel.MovieCinemaId);

                    if (nextreservation != null)
                    {
                        foreach (Reservations oldr in oldres)
                        {
                            if (oldr.reservations_time == previousreservation.reservations_time &&
                                oldr.ReservationsCinemaId == previousreservation.ReservationsCinemaId)
                            {
                                var newr1 = new Reservations()
                                {
                                    ReservationsMovieId = oldr.ReservationsMovieId,
                                    ReservationsCinemaId = nextreservation.ReservationsCinemaId,
                                    ReservationsMovieName = oldr.ReservationsMovieName,
                                    CustomersId = oldr.CustomersId,
                                    reservations_time = nextreservation.reservations_time,
                                    number_of_seats = oldr.number_of_seats
                                };

                                var newr2 = new Reservations()
                                {
                                    ReservationsMovieId = nextreservation.ReservationsMovieId,
                                    ReservationsCinemaId = oldr.ReservationsCinemaId,
                                    ReservationsMovieName = nextreservation.ReservationsMovieName,
                                    CustomersId = nextreservation.CustomersId,
                                    reservations_time = oldr.reservations_time,
                                    number_of_seats = nextreservation.number_of_seats
                                };

                                _dbContext.Reservations.Remove(oldr);

                                _dbContext.Reservations.Add(newr1);
                                _dbContext.Reservations.Add(newr2);

                                await _dbContext.SaveChangesAsync();

                            }

                            if (oldr.reservations_time == nextreservation.reservations_time &&
                                oldr.ReservationsCinemaId == nextreservation.ReservationsCinemaId)
                            {
                                _dbContext.Reservations.Remove(oldr);
                            }
                        }
                    }
                }
                else
                {
                    previousmovie.CinemaId = newcinema.cinemaid;
                }

                await _dbContext.SaveChangesAsync();

                var c = await _dbContext.ContentAdmins.FirstOrDefaultAsync(x => x.contentadminid == addMovieViewModel.MovieContentAdminId);
                Content_Admin content_Admin = new Content_Admin();
                content_Admin.ContentAdminUsername = c.ContentAdminUsername;

                return RedirectToAction("ContentAdminsIndex", "ContentAdmins", content_Admin);
            }


            return View();
        }

    }
}
