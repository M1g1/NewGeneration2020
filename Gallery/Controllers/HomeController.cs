using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using System.Drawing;
using Gallery.Service;
using Gallery.Manager;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IHashService _hashService;
        public HomeController(IImageService imageService, IHashService hashService)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(string fileToDelete)
        {
            
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files)
        {
            
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

    }
}
