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

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategoriesAsync().Result;
            return View(categories);
        }

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

        [HttpGet("/Category/GetCategories")]
        public IActionResult GetCategories(int id)
        {
            var categories = _categoryService.GetAllCategoriesAsync().Result;

            return Json(categories);
        }

        [HttpPut("/Category/EditCategory/{id}")]
        public async Task<IActionResult> EditCategory([FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.UpdateCategoryAsync(categoryDTO);

            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }

        [Route("/Category/EditCategory/{id}")]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpDelete("/Category/DeleteCategory/{id}")]
        public IActionResult Delete(int id)
        {
            if (!_categoryService.DeleteCategoryAsync(id).Result)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }
    }
}
