using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExpenseWebApp.Models;
using ExpenseWebAppBLL.Services;
using ExpenseWebAppBLL.DTOs.UserDTOs;
using ExpenseWebAppDAL.Authentication;
using System.Security.Claims;
using System.Text.Json;

namespace ExpenseWebApp.Controllers
{
    //need async refactor
    //[Route("Home/[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {
        private readonly UserService _userService;
        private readonly TokenProvider _tokenProvider;
        public HomeController(UserService userService, TokenProvider tokenProvider)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Home/TryLogin")]
        public async Task<IActionResult> Login()
        {
            var AT = Request.Cookies["jwt"];

            if (AT is null)
            {
                return View();
            }

            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "no";

            if (!int.TryParse(idStr, out int id))
            {
                return Json(new
                {
                    success = false,
                    message = "Id parse error"
                });
            }

            var user = await _userService.GetUserByIdAsync(id);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            return RedirectToAction("UserProfileViewByObject", "User", user);
        }

        [HttpPost("/Home/SubmitLogin")]
        public async Task<IActionResult> SubmitLogin([FromBody] UserReadDTO userReadDto)
        {
            var user = await _userService.GetUserByNameAndPasswordAsync(userReadDto.Username, userReadDto.Password);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found Or password does not match"
                });
            }

            var tokenstr = _tokenProvider.CreateAccessToken(user.Id, user.Username);

            Response.Cookies.Append("jwt", tokenstr, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(15)
            });
            
            return Json(new { success = true, redirectUrl = Url.Action("UserProfileViewById", "User", new { userId = user.Id }) });
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
