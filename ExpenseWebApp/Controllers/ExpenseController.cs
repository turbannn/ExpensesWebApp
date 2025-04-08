using ExpenseWebApp.Models;
using ExpenseWebAppBLL.DTOs.ExpenseDTOs;
using ExpenseWebAppBLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppTest.Controllers;

namespace ExpenseWebApp.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseService _expenseService;

        public ExpenseController(ILogger<HomeController> logger, ExpenseService expenseService)
        {
            _logger = logger;
            _expenseService = expenseService;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();

            var totalExpenses = expenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(expenses);
        }

        [HttpPost("/Expense/CreateExpense")]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseCreateDTO expenseDTO)
        {
            var createTaskResult = await _expenseService.AddExpenseAsync(expenseDTO);

            if (!createTaskResult)
                return BadRequest("Data not sent");

            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }
        [Route("/Expense/CreateExpense")]
        public IActionResult CreateExpense()
        {
            return View();
        }

        //[HttpPut]
        [HttpPut("/Expense/EditExpense/{id}")]
        public async Task<IActionResult> EditExpense([FromBody] ExpenseUpdateDTO expenseDTO)
        {
            var updateTaskResult = await _expenseService.UpdateExpenseAsync(expenseDTO);

            if (!updateTaskResult)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }

        [Route("/Expense/EditExpense/{id}")]
        public async Task<IActionResult> EditExpense(int id)
        {
            var expense = await _expenseService.GetUpdateExpenseByIdAsync(id);

            if (expense == null) return NotFound();

            return View(expense);
        }

        [HttpGet("/Expense/GetExpense/{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            var expense = await _expenseService.GetReadExpenseByIdAsync(id);

            if (expense == null)
                return NotFound(new { success = false, message = "Expense not found" });

            return Ok(expense);
        }

        [HttpDelete("Expense/DeleteExpense/{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var deleteTaskResult = await _expenseService.DeleteExpenseAsync(id);

            if (!deleteTaskResult)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
