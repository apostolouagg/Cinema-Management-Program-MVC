using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCERGASIA.Data;
using MVCERGASIA.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCERGASIA.Controllers
{

    public class AddController : Controller
    {
        private readonly MVCDbContext _dbContext;

        public AddController(MVCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> AddIndex(LoginViewModel users)
        {
            if (users.Role.Equals("Customer"))
            {
                ViewBag.adUsername = users.Username;
                return View("~/Views/Add/AddCustomerIndex.cshtml");
            }
            else if (users.Role.Equals("Content Admin"))
            {
                ViewBag.adUsername = users.Username;
                return View("~/Views/Add/AddContentAdminIndex.cshtml");
            }
            else if (users.Role.Equals("Movie"))
            {
                List<Cinemas> cinema = new List<Cinemas>();
                cinema = await _dbContext.Cinemas.ToListAsync();
                ViewBag.cinema = cinema;

                var u = await _dbContext.ContentAdmins.FirstOrDefaultAsync(x => x.ContentAdminUsername == users.Username);
                ViewBag.ContentAdmins = u.contentadminid;

                return View("~/Views/Add/AddMovieIndex.cshtml");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddContentAdmin(AddContentAndCustomerViewModel addContentAndCustomerViewModel)
        {
            try
            {
                ViewBag.flag = false;

                bool name = addContentAndCustomerViewModel.Cname.All(Char.IsLetter);
                bool username = addContentAndCustomerViewModel.AddCusername.All(Char.IsLetterOrDigit);

                if (name && username &&
                    (addContentAndCustomerViewModel.Cemail.EndsWith("@yahoo.gr") ||
                    addContentAndCustomerViewModel.Cemail.EndsWith("@yahoo.com") ||
                    addContentAndCustomerViewModel.Cemail.EndsWith("@gmail.com")))
                {
                    // auto to kanoume etsi wste otan ginei to return thn arxikh selida tou admin
                    // na fainontai ksana ta stoixeia tou
                    Admins admins = new Admins();
                    admins.AdminsUsername = addContentAndCustomerViewModel.adUsername;

                    var contentadmin = new Content_Admin()
                    {
                        contentadminname = addContentAndCustomerViewModel.Cname,
                        ContentAdminUsername = addContentAndCustomerViewModel.AddCusername,
                    };

                    var users = new Users()
                    {
                        Username = addContentAndCustomerViewModel.AddCusername,
                        Password = addContentAndCustomerViewModel.Cpassword,
                        Email = addContentAndCustomerViewModel.Cemail,
                        Role = "Content Admin"
                    };

                    _dbContext.ContentAdmins.Add(contentadmin);
                    _dbContext.Users.Add(users);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("AdminsIndex", "Admins", admins);
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                ViewBag.adUsername = addContentAndCustomerViewModel.adUsername;
                ViewBag.flag = true;
                ViewBag.AlertMessage = "Something is incorrect or Username already exist! Please check your inputs and try again.";
                return View("~/Views/Add/AddContentAdminIndex.cshtml");
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddContentAndCustomerViewModel addContentAndCustomerViewModel)
        {
            try
            {
                ViewBag.flag = false;

                bool name = addContentAndCustomerViewModel.Cname.All(Char.IsLetter);
                bool username = addContentAndCustomerViewModel.AddCusername.All(Char.IsLetterOrDigit);

                if (name && username &&
                    (addContentAndCustomerViewModel.Cemail.EndsWith("@yahoo.gr") ||
                    addContentAndCustomerViewModel.Cemail.EndsWith("@yahoo.com") ||
                    addContentAndCustomerViewModel.Cemail.EndsWith("@gmail.com")))
                {
                    // auto to kanoume etsi wste otan ginei to return thn arxikh selida tou admin
                    // na fainontai ksana ta stoixeia tou
                    Admins admins = new Admins();
                    admins.AdminsUsername = addContentAndCustomerViewModel.adUsername;

                    var customer = new Customers()
                    {
                        customername = addContentAndCustomerViewModel.Cname,
                        CustomerUsername = addContentAndCustomerViewModel.AddCusername,
                    };

                    var users = new Users()
                    {
                        Username = addContentAndCustomerViewModel.AddCusername,
                        Password = addContentAndCustomerViewModel.Cpassword,
                        Email = addContentAndCustomerViewModel.Cemail,
                        Role = "Customer"
                    };

                    _dbContext.Customers.Add(customer);
                    _dbContext.Users.Add(users);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("AdminsIndex", "Admins", admins);
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                ViewBag.adUsername = addContentAndCustomerViewModel.adUsername;
                ViewBag.flag = true;
                ViewBag.AlertMessage = "Something is incorrect or Username already exist! Please check your inputs and try again.";
                return View("~/Views/Add/AddCustomerIndex.cshtml");
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddMovie(AddMovieViewModel addMovieViewModel)
        {
            try
            {
                // auto to kanoume etsi wste otan ginei to return thn arxikh selida tou admin
                // na fainontai ksana ta stoixeia tou
                var c = await _dbContext.ContentAdmins.FirstOrDefaultAsync(x => x.contentadminid == addMovieViewModel.MovieContentAdminId);
                Content_Admin co_admin = new Content_Admin();
                co_admin.ContentAdminUsername = c.ContentAdminUsername;

                ViewBag.flag = false;

                bool length = addMovieViewModel.Movielength.ToString().All(Char.IsDigit);

                if (length)
                {
                    var provoles = await _dbContext.Provoles.ToListAsync();
                    ViewBag.provoles = provoles;

                    foreach (Provoles p in provoles)
                    {
                        if (p.CinemaId == addMovieViewModel.MovieCinemaId && p.provoles_time == addMovieViewModel.MovieTime)
                        {
                            throw new Exception();
                        }
                    }

                    var movie = new Movies()
                    {
                        moviename = addMovieViewModel.Moviename,
                        moviecontent = addMovieViewModel.Moviecontent,
                        movielength = addMovieViewModel.Movielength,
                        movietype = addMovieViewModel.Movietype,
                        moviesummary = addMovieViewModel.Moviesummary,
                        moviedirector = addMovieViewModel.Moviedirector,
                        ContentAdminId = addMovieViewModel.MovieContentAdminId,
                        movieseats = 100
                    };

                    _dbContext.Movies.Add(movie);
                    
                    await _dbContext.SaveChangesAsync();


                    List<Movies> m = new List<Movies>();
                    m = await _dbContext.Movies.ToListAsync();
                    var id = m.Last();
                    
                    var provolh = new Provoles()
                    {
                        ProvolesMovieId = id.moviesid,
                        MovieName = addMovieViewModel.Moviename,
                        CinemaId = addMovieViewModel.MovieCinemaId,
                        provoles_time = addMovieViewModel.MovieTime,
                        ContentAdminId = addMovieViewModel.MovieContentAdminId
                    };
                    
                    _dbContext.Provoles.Add(provolh);
                    
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("ContentAdminsIndex", "ContentAdmins", co_admin);
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                List<Cinemas> cinema = new List<Cinemas>();
                cinema = await _dbContext.Cinemas.ToListAsync();
                ViewBag.cinema = cinema;

                var c = await _dbContext.ContentAdmins.FirstOrDefaultAsync(x => x.contentadminid == addMovieViewModel.MovieContentAdminId);

                ViewBag.ContentAdmins = c.contentadminid;

                ViewBag.flag = true;
                ViewBag.AlertMessage = "Something is incorrect or movie theatre is already in use for that time! Please check your inputs and try again.";
                return View("~/Views/Add/AddMovieIndex.cshtml");
            }

        }
    }
}
