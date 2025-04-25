using ExpenseWebAppBLL.DTOs.UserDTOs;
using ExpenseWebAppBLL.Services;
using ExpenseWebAppDAL.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        public async Task<IActionResult> UserProfileViewByObject(UserReadDTO userReadDto)
        {
            var user = await _userService.GetUserByNameAndPasswordAsync(userReadDto.Username, userReadDto.Password);

            if (user is null) return NotFound();

            var totalExpenses = user.Expenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View("UserProfileView", user);
        }

        [Authorize]
        public async Task<IActionResult> UserProfileViewById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user is null) return NotFound();

            var totalExpenses = user.Expenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View("UserProfileView", user);
        }

        [Route("/User/Register")]
        public IActionResult RegistrationView()
        {
            return View();
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

            var tokenstr = _tokenProvider.CreateAccessToken(user.Id, user.Username);

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
