using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Threading.Tasks;
using Gallery.Service;
using Gallery.Manager;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IHashService _hashService;
        private readonly IUsersService _usersService;
        public HomeController(IImageService imageService, IHashService hashService, IUsersService usersService)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(string fileToDelete)
        {

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase files)
        {
            byte[] data;
            using (Stream inputStream = files.InputStream)
            {

                if (!(inputStream is MemoryStream memoryStream))
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();

            }

            var defaultPath = GalleryConfigurationManager.GetPathToSave();
            var DirPath = Server.MapPath(defaultPath) + _hashService.ComputeSha256Hash(User.Identity.Name);
            var filePath = Path.Combine(DirPath, files.FileName);
            var userDto = await _usersService.GetUserByIdAsync(Convert.ToInt32(User.Identity.Name));
            if (userDto == null)
            {
                ViewBag.Error = "Something went wrong, try again.";
                return View("Error");
            }

            await _imageService.UploadImageAsync(data, filePath, userDto);
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
