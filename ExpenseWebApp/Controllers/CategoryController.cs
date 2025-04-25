using ExpenseWebAppBLL.Services;
using Microsoft.AspNetCore.Mvc;
using ExpenseWebAppBLL.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseWebApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryService _categoryService;

        public CategoryController(ILogger<HomeController> logger, CategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategoriesAsync().Result;
            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/Category/CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.AddCategoryAsync(categoryDTO);
            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpGet("/Category/GetCategories")]
        public IActionResult GetCategories(int id)
        {
            var categories = _categoryService.GetAllCategoriesAsync().Result;

            return Json(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("/Category/EditCategory/{id}")]
        public async Task<IActionResult> EditCategory([FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.UpdateCategoryAsync(categoryDTO);

            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }

        [Authorize(Roles = "Admin")]
        [Route("/Category/EditCategory/{id}")]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("/Category/DeleteCategory/{id}")]
        public IActionResult Delete(int id)
        {
            if (!_categoryService.DeleteCategoryAsync(id).Result)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }
    }
}
