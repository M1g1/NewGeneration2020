using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gallery.Filters;
using Gallery.Models;
using Gallery.Service;

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
            var isUserExist = await _usersService.IsUserExistAsync(model.Email, model.Password);

            if (isUserExist == false)
            {

                await _usersService.AddUserToDatabaseAsync(model.Email, model.Password);

                var userId = _usersService.GetUserByEmail(model.Email).Id.ToString();

                ClaimsIdentity claims = _authenticationService.CreateClaimsIdentity(userId);

                _authenticationService.AutorizeContext(HttpContext.GetOwinContext(), claims);

                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "User already exists");
            }

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
            var canAuthorize = await _usersService.IsUserExistAsync(model.Email, model.Password);

            var ipAddress = HttpContext.Request.UserHostAddress;
            
            await _usersService.AddLoginAttemptToDatabaseAsync(model.Email, ipAddress, canAuthorize);

            if (canAuthorize)
            {

                var userId = _usersService.GetUserByEmail(model.Email).Id.ToString();

                ClaimsIdentity claims = _authenticationService.CreateClaimsIdentity(userId);

                _authenticationService.AutorizeContext(HttpContext.GetOwinContext(), claims);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View(model);
        }
    }
}