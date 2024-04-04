using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCERGASIA.Data;
using MVCERGASIA.Models;

namespace MVCERGASIA.Controllers
{
    public class UsersController : Controller
    {
        private readonly MVCDbContext _dbContext;

        public UsersController(MVCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            List<Users> users = new List<Users>();
            List<Admins> admins = new List<Admins>();
            List<Content_Admin> content_Admin = new List<Content_Admin>();
            List<Customers> customers = new List<Customers>();

            users = await _dbContext.Users.ToListAsync();
            admins = await _dbContext.Admins.ToListAsync();
            content_Admin = await _dbContext.ContentAdmins.ToListAsync();
            customers = await _dbContext.Customers.ToListAsync();

            ViewBag.flag = false;

            foreach (var data in users)
            {
                if (data.Username.Equals(loginViewModel.Username) &&
                    data.Password.Equals(loginViewModel.Password) &&
                    data.Role.Equals(loginViewModel.Role))
                {
                    if (data.Role == "Customer")
                    {
                        foreach (Customers cust in customers)
                        {
                            if (cust.CustomerUsername.Equals(data.Username))
                            {
                                return RedirectToAction("CustomersIndex", "Customers", cust);
                            }
                        }
                    }
                    else if (data.Role == "Content " + "Admin")
                    {
                        foreach (Content_Admin cont in content_Admin)
                        {
                            if (cont.ContentAdminUsername.Equals(data.Username))
                            {
                                return RedirectToAction("ContentAdminsIndex", "ContentAdmins", cont);
                            }
                        }
                    }
                    else if (data.Role == "Admin")
                    {
                        foreach (Admins admin in admins)
                        {
                            if (admin.AdminsUsername.Equals(data.Username))
                            {
                                return RedirectToAction("AdminsIndex", "Admins", admin);
                            }
                        }
                    }

                }
                else
                {
                    ViewBag.flag = true;
                    ViewBag.AlertMessage = "Username, Password or Role is incorrect! Please try again.";
                }
            }


            return View();
        }

    }
}
