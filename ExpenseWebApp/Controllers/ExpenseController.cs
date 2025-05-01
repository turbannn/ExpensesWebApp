using ExpenseWebApp.Models;
using ExpenseWebAppBLL.DTOs.ExpenseDTOs;
using ExpenseWebAppBLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [Authorize(Roles = "Admin")]
        [HttpGet("/Expense/")]
        public async Task<IActionResult> Index()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();

            var totalExpenses = expenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(expenses);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("/Expense/CreateExpense")]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseCreateDTO expenseDTO)
        {
            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "no";

            if (!int.TryParse(idStr, out int id))
            {
                return Json(new
                {
                    success = false,
                    message = "Id parse error"
                });
            }

            expenseDTO.UserId = id;

            var creationTaskResult = await _expenseService.AddExpenseAsync(expenseDTO);

            if (!creationTaskResult)
                return BadRequest("Data not sent");

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "User") });
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("/Expense/CreateExpense")]
        public IActionResult CreateExpense()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut("/Expense/EditExpense/{id}")]
        public async Task<IActionResult> EditExpense([FromBody] ExpenseUpdateDTO expenseDTO)
        {
            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "no";

            if (!int.TryParse(idStr, out int id))
            {
                return Json(new
                {
                    success = false,
                    message = "Id parse error"
                });
            }

            expenseDTO.UserId = id;

            var updateTaskResult = await _expenseService.UpdateExpenseAsync(expenseDTO);

            if (!updateTaskResult)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "User") });
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("/Expense/EditExpense/{id}")]
        public async Task<IActionResult> EditExpense(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);

            if (expense == null) return NotFound();

            return View(expense);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("/Expense/GetExpense/{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);

            if (expense == null)
                return NotFound(new { success = false, message = "Expense not found" });

            return Ok(expense);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpDelete("Expense/DeleteExpense/{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var deleteTaskResult = await _expenseService.DeleteExpenseAsync(id);

            if (!deleteTaskResult)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "User") });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
