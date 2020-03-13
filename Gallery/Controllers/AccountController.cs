using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gallery.DAL.Models;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }
    }
}