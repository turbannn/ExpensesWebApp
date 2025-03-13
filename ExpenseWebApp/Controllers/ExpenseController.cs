using Microsoft.AspNetCore.Mvc;

namespace ExpenseWebApp.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
