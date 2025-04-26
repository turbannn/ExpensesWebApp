using ExpenseWebAppBLL.DTOs.UserDTOs;
using ExpenseWebAppBLL.Services;
using ExpenseWebAppDAL.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly TokenProvider _tokenProvider;

        public UserController(UserService userService, TokenProvider tokenProvider)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
        }

        [HttpPost("/User/SubmitLogin")]
        public async Task<IActionResult> SubmitLogin([FromBody] UserLogin userLogin)
        {
            var user = await _userService.GetUserByNameAndPasswordAsync(userLogin.Username, userLogin.Password);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found Or password does not match"
                });
            }

            var tokenstr = _tokenProvider.CreateAccessToken(user.Id, user.Role);

            Response.Cookies.Append("jwt", tokenstr, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(15)
            });

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "User") });
        }

        [Authorize(Roles = "User,Admin")]
        [Route("/User/UserProfile")]
        public async Task<IActionResult> UserProfileView()
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

            var user = await _userService.GetUserByIdAsync(id);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            var totalExpenses = user.Expenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(user);
        }

        [HttpPost("/User/Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok();
        }

        [HttpPost("/User/SubmitRegistration")]
        public async Task<IActionResult> SubmitRegistration([FromBody] UserCreateDTO userCreateDto)
        {
            var addResult = await _userService.AddUserAsync(userCreateDto);

            if(!addResult)
            {
                return Json(new
                {
                    success = false,
                    message = "User hasn't been added"
                });
            }

            var user = await _userService.GetUserByNameAndPasswordAsync(userCreateDto.Username, userCreateDto.Password);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            var tokenstr = _tokenProvider.CreateAccessToken(user.Id, user.Role);

            Response.Cookies.Append("jwt", tokenstr, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(15)
            });

            return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
