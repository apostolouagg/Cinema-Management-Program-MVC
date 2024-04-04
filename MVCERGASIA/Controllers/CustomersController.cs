using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCERGASIA.Data;
using MVCERGASIA.Models;

namespace MVCERGASIA.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MVCDbContext _dbContext;

        public CustomersController(MVCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Users");
        }

        public async Task<IActionResult> CustomersIndex(Customers customers)
        {
            //Movies
            List<Movies> mov = new List<Movies>();
            mov = await _dbContext.Movies.ToListAsync();

            List<ViewCustomersMoviesViewModel> mov2 = new List<ViewCustomersMoviesViewModel>();

            foreach (Movies m in mov)
            {
                var p = await _dbContext.Provoles.FirstOrDefaultAsync(x => x.ProvolesMovieId == m.moviesid);
                var c = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.cinemaid == p.CinemaId);

                var model1 = new ViewCustomersMoviesViewModel()
                {
                    movieId = m.moviesid,
                    moviename = m.moviename,
                    movielength = m.movielength,
                    movieTime = p.provoles_time,
                    movieCinemaName = c.cinemaname,
                    tickets = m.movieseats,
                    customerUsername = customers.CustomerUsername
                };

                mov2.Add(model1);
            }

            ViewBag.Customer = customers.CustomerUsername;
            ViewBag.Movies = mov2;

            //Reservations
            List<Reservations> res = new List<Reservations>();
            res = await _dbContext.Reservations.ToListAsync();

            List<ViewReservationsViewModel> res2 = new List<ViewReservationsViewModel>();

            var cust = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerUsername == customers.CustomerUsername);

            foreach (Reservations r in res)
            {
                if (r.CustomersId == cust.customerid)
                {
                    var ci = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.cinemaid == r.ReservationsCinemaId);

                    var model2 = new ViewReservationsViewModel()
                    {
                        VreservationsMovieName = r.ReservationsMovieName,
                        VreservationsCinemaName = ci.cinemaname,
                        Vreservations_time = r.reservations_time,
                        Vnumber_of_seats = r.number_of_seats
                    };

                    res2.Add(model2);
                }
            }

            ViewBag.Reservations = res2;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewCustomersMovieIndex(ViewCustomersMoviesViewModel movie)
        {
            var m = await _dbContext.Movies.FirstOrDefaultAsync(x => x.moviesid == movie.movieId);

            if (m != null)
            {
                var s = await _dbContext.Provoles.FirstOrDefaultAsync(y => y.ProvolesMovieId == movie.movieId);
                var s2 = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.cinemaid == s.CinemaId);

                var viewSelected = new ViewCustomersMoviesViewModel()
                {
                    movieId = m.moviesid,
                    moviename = m.moviename,
                    movielength = m.movielength,
                    customerUsername = movie.customerUsername,
                    movieTime = s.provoles_time,
                    movieCinemaName = s2.cinemaname,
                    tickets = movie.tickets
                };

                ViewBag.Customer = movie.customerUsername;
                ViewBag.id = m.moviesid;

                return View(viewSelected);
            }

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(ViewCustomersMoviesViewModel viewCustomers)
        {
            try
            {
                ViewBag.flag = false;

                // auto to kanoume etsi wste otan ginei to return thn arxikh selida tou customer
                // na fainontai ksana ta stoixeia tou
                Customers cu = new Customers();
                cu.CustomerUsername = viewCustomers.customerUsername;

                ViewBag.Customer = viewCustomers.customerUsername;
                ViewBag.id = viewCustomers.movieId;

                var cine = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.cinemaname == viewCustomers.movieCinemaName);
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerUsername == viewCustomers.customerUsername);
                var provoles = await _dbContext.Provoles.FirstOrDefaultAsync(x => x.ProvolesMovieId == viewCustomers.movieId);
                var mov = await _dbContext.Movies.FirstOrDefaultAsync(x => x.moviesid == viewCustomers.movieId);

                if (viewCustomers.ticketstotake <= mov.movieseats)
                {
                    var reservation = new Reservations()
                    {
                        ReservationsCinemaId = cine.cinemaid,
                        CustomersId = customer.customerid,
                        ReservationsMovieId = viewCustomers.movieId,
                        ReservationsMovieName = viewCustomers.moviename,
                        reservations_time = provoles.provoles_time,
                        number_of_seats = viewCustomers.ticketstotake
                    };

                    _dbContext.Reservations.Add(reservation);

                    await _dbContext.SaveChangesAsync();


                    mov.movieseats = mov.movieseats - viewCustomers.ticketstotake;

                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("CustomersIndex", "Customers", cu);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                ViewBag.flag = true;
                ViewBag.Customer = viewCustomers.customerUsername;
                ViewBag.id = viewCustomers.movieId;
                ViewBag.AlertMessage = "We do not have that many available tickets. Please try again.";
                return View("~/Views/Customers/ViewCustomersMovieIndex.cshtml");
            }
        }
    }
}
