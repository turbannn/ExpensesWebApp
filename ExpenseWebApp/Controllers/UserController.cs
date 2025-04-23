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
        public IActionResult UserProfileView(UserReadDTO userReadDto)
        {
            return View(userReadDto);
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

            var tokenstr = _tokenProvider.CreateAccessToken(userCreateDto.Id, userCreateDto.Username);

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
