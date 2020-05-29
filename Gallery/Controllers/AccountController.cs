using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gallery.Filters;
using Gallery.Models;
using Gallery.Service;
using Gallery.Service.Contract;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IAuthentication _authenticationService;
        public AccountController(IUsersService usersService, IAuthentication authenticationService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [LogFilter]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModelState]
        [LogFilter]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            var isUserExist = await _usersService.IsUserExistAsync(model.Email);

            if (isUserExist == false)
            {

                await _usersService.AddUserToDatabaseAsync(new UserDto
                {
                    Email = model.Email, 
                    Password = model.Password
                });

                var userDto = await _usersService.GetUserByEmailAsync(model.Email);

                if (userDto == null)
                {
                    ModelState.AddModelError("", "Something went wrong, try again.");
                    return View(model);
                }

                var userId = userDto.Id;

                ClaimsIdentity claims = _authenticationService.CreateClaimsIdentity(userId.ToString());

                _authenticationService.AutorizeContext(HttpContext.GetOwinContext(), claims);

                var ipAddress = HttpContext.Request.UserHostAddress;

                var loginAttemptDto = new LoginAttemptDto
                {
                    IsSuccess = true,
                    IpAddress = ipAddress,
                    UserId = userId,
                    TimeStamp = DateTime.Now
                };

                await _usersService.AddLoginAttemptToDatabaseAsync(loginAttemptDto);

                return RedirectToAction("Index", "Home");

            }

            ModelState.AddModelError("", "User already exists");
            
            return View(model);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModelState]
        [LogFilter]
        public async Task<ActionResult> Login(LoginModel model)
        {

            var isUserExist = await _usersService.IsUserExistAsync(model.Email);

            if (isUserExist)
            {
                
                var canAuthorize = await _usersService.IsUserExistAsync(model.Email, model.Password);
                
                var userDto = await _usersService.GetUserByEmailAsync(model.Email);

                if (userDto == null)
                {
                    ModelState.AddModelError("", "Something went wrong, try again.");
                    return View(model);
                }

                var userId = userDto.Id;

                var ipAddress = HttpContext.Request.UserHostAddress;

                var loginAttemptDto = new LoginAttemptDto
                {
                    IsSuccess = canAuthorize,
                    IpAddress = ipAddress,
                    UserId = userId,
                    TimeStamp = DateTime.Now
                };

                await _usersService.AddLoginAttemptToDatabaseAsync(loginAttemptDto);

                if (canAuthorize)
                {

                    ClaimsIdentity claims = _authenticationService.CreateClaimsIdentity(userId.ToString());

                    _authenticationService.AutorizeContext(HttpContext.GetOwinContext(), claims);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "User not found");

            return View(model);
        }
    }
}