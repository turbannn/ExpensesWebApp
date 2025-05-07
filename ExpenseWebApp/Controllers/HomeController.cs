using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExpenseWebApp.Models;

namespace ExpenseWebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Home/TryLogin")]
        public IActionResult Login()
        {
            var AT = Request.Cookies["jwt"];

            if (AT is null)
            {
                return View();
            }
            
            return RedirectToAction("UserProfileView", "User");
        }

        [HttpGet("/Home/Register")]
        public IActionResult RegistrationView()
        {
            return View("RegistrationView");
        }

        [HttpGet("/Home/ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
