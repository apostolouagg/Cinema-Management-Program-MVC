using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCERGASIA.Data;
using MVCERGASIA.Models;

namespace MVCERGASIA.Controllers
{
    public class AdminsController : Controller
    {
        private readonly MVCDbContext _dbContext;

        public AdminsController(MVCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Users");
        }

        public async Task<IActionResult> AdminsIndex(Admins admins)
        {
            ViewBag.Admins = admins.AdminsUsername;

            List<Content_Admin> cont = new List<Content_Admin>();
            List<Customers> cust = new List<Customers>();

            cont = await _dbContext.ContentAdmins.ToListAsync();
            cust = await _dbContext.Customers.ToListAsync();



            List<DeleteContentAndCustomerViewModel> cont2 = new List<DeleteContentAndCustomerViewModel>();

            foreach (Content_Admin c in cont)
            {
                var y = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == c.ContentAdminUsername);

                var model1 = new DeleteContentAndCustomerViewModel()
                {
                    Did = c.contentadminid,
                    Dname = c.contentadminname,
                    Demail = y.Email,
                    Dpassword = y.Password,
                    Drole = y.Role,
                    AddDusername = y.Username,
                    adUsername = admins.AdminsUsername
                };

                cont2.Add(model1);
            }

            ViewBag.ContentAdmin = cont2;



            List<DeleteContentAndCustomerViewModel> cust2 = new List<DeleteContentAndCustomerViewModel>();

            foreach (Customers cu in cust)
            {
                var z = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == cu.CustomerUsername);

                var model2 = new DeleteContentAndCustomerViewModel()
                {
                    Did = cu.customerid,
                    Dname = cu.customername,
                    Demail = z.Email,
                    Dpassword = z.Password,
                    Drole = z.Role,
                    AddDusername = z.Username,
                    adUsername = admins.AdminsUsername
                };

                cust2.Add(model2);
            }

            ViewBag.Customers = cust2;

            return View();
        }

        public IActionResult AddPerson(LoginViewModel addperson)
        {
            return RedirectToAction("AddIndex", "Add", addperson);
        }

        [HttpGet]
        public async Task<IActionResult> ViewContentAdminIndex(DeleteContentAndCustomerViewModel del)
        {
            int Id = del.Did;
            var c = await _dbContext.ContentAdmins.FirstOrDefaultAsync(x => x.contentadminid == Id);

            if (c != null)
            {
                var viewSelected = new DeleteContentAndCustomerViewModel()
                {
                    Did = Id,
                    Demail = del.Demail,
                    Dname = del.Dname,
                    Dpassword = del.Dpassword,
                    Drole = del.Drole,
                    AddDusername = del.AddDusername
                };

                ViewBag.adUsername = del.adUsername;

                return View(viewSelected);
            }

            return View(c);
        }


        [HttpGet]
        public async Task<IActionResult> ViewCustomerIndex(DeleteContentAndCustomerViewModel del)
        {
            int Id = del.Did;
            var c = await _dbContext.Customers.FirstOrDefaultAsync(x => x.customerid == Id);

            if (c != null)
            {
                var viewSelected = new DeleteContentAndCustomerViewModel()
                {
                    Did = Id,
                    Demail = del.Demail,
                    Dname = del.Dname,
                    Dpassword = del.Dpassword,
                    Drole = del.Drole,
                    AddDusername = del.AddDusername
                };

                ViewBag.adUsername = del.adUsername;

                return View(viewSelected);
            }

            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteContentAndCustomerViewModel dcontentAndCustomerViewModel)
        {
            // auto to kanoume etsi wste otan ginei to return thn arxikh selida tou admin
            // na fainontai ksana ta stoixeia tou
            Admins admins = new Admins();
            admins.AdminsUsername = dcontentAndCustomerViewModel.adUsername;

            if (dcontentAndCustomerViewModel.Drole.Equals("Customer"))
            {
                var customer = await _dbContext.Customers.FindAsync(dcontentAndCustomerViewModel.Did);
                var user = await _dbContext.Users.FindAsync(dcontentAndCustomerViewModel.AddDusername);

                if (customer != null)
                {
                    _dbContext.Customers.Remove(customer);
                    _dbContext.Users.Remove(user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else if (dcontentAndCustomerViewModel.Drole.Equals("Content Admin"))
            {
                var contentAdmin = await _dbContext.ContentAdmins.FindAsync(dcontentAndCustomerViewModel.Did);
                var user = await _dbContext.Users.FindAsync(dcontentAndCustomerViewModel.AddDusername);

                if (contentAdmin != null)
                {
                    _dbContext.ContentAdmins.Remove(contentAdmin);
                    _dbContext.Users.Remove(user);
                    await _dbContext.SaveChangesAsync();
                }
            }

            return RedirectToAction("AdminsIndex", "Admins", admins);

        }
    }
}
