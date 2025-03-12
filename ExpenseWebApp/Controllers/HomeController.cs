using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExpenseWebApp.Models;
using ExpenseWebAppBLL.Services;
using ExpenseWebAppBLL.DTOs;
using ExpenseWebAppDAL.Entities;

namespace WebAppTest.Controllers
{
    //need async refactor
    //[Route("Home/[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseService _expenseService;

        public HomeController(ILogger<HomeController> logger, ExpenseService expenseService)
        {
            _logger = logger;
            _expenseService = expenseService;
        }

        public IActionResult Index() //NOT supposed to be named like an cshtml file
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();//can be called with args, args are name of cshtml file to redirect
            //!!! it seeks for file with that name
        }

        public IActionResult ExpensesView() //same as cshtml
        {
            var expenses = _expenseService.GetAllExpensesAsync().Result;

            var totalExpenses = expenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(expenses);
        }
        public IActionResult ExpenseRedirection(int? id)
        {
            if (id != null)
            {
                var exp = _expenseService.GetExpenseByIdAsync(Convert.ToInt32(id)).Result;
                return View("EditExpense", exp);
            }
            return View("CreateExpense"); //returns cshtml
        }

        [HttpPost]
        public IActionResult CreateExpenseView([FromBody] ExpenseDTO expenseDTO)
        {

            if (!_expenseService.AddExpenseAsync(expenseDTO).Result) //async refactor
                return BadRequest("Data not sent");

            return Json(new { success = true, redirectUrl = Url.Action("ExpensesView") });
            //return RedirectToAction("ExpensesView"); //returns method
        }

        //[HttpPut]
        [HttpPut("/Home/EditExpense/{id}")]
        public IActionResult EditExpense([FromBody] ExpenseDTO expenseDTO) //int id
        {
            //if (id != expenseDTO.Id)
            //    return BadRequest("ID mismatch");

            if (!_expenseService.UpdateExpenseAsync(expenseDTO).Result)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("ExpensesView") });
        }

        [HttpGet("Home/GetExpense/{id}")]
        public IActionResult GetExpense(int id)
        {
            var expense = _expenseService.GetExpenseByIdAsync(id).Result;

            if (expense == null)
                return NotFound(new { success = false, message = "Expense not found" });

            return Ok(expense);
        }

        [HttpDelete("Home/DeleteExpense/{id}")]
        public IActionResult DeleteExpense(int id)
        {
            if (!_expenseService.DeleteExpenseAsync(id).Result)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("ExpensesView") });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
