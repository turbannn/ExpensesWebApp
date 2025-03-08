using ExpenseWebAppBLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppTest.Controllers;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryService _categoryService;
        //private readonly List<Category> categories; - test

        public CategoryController(ILogger<HomeController> logger, CategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
            //categories = [new Category() { Id = 1, Name="asdrf" }]; - test
        }

        public IActionResult Index()
        {
            return View(); //categories - test
        }

        // GET: CategoryController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        public IActionResult Edit(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
