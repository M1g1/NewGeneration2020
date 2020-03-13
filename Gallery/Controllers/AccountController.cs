using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Gallery.DAL;
using Gallery.DAL.Models;
using Gallery.Service;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {
        private IUsersService _usersService;
        public AccountController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public AccountController() : this(new UsersService(new UsersRepository(new UserContext()))) { }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var isUserExist = await _usersService.IsUserExistAsync(model.Name, model.Password);

                if (isUserExist == false)
                {

                    using (UserContext database = new UserContext())
                    {
                        database.Users.Add(new User { Email = model.Name, Password = model.Password });
                        database.SaveChanges();
                    }

                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "User already exists");
                }
            }
            return View(model);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var canAuthorize = await _usersService.IsUserExistAsync(model.Name, model.Password);

                if (canAuthorize)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User not found");
                }
            }
            return View(model);
        }
    }
}