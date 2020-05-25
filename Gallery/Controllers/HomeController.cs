using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Gallery.Filters;
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
        [LogFilter]
        public async Task<ActionResult> Delete(string fileToDelete)
        {
            var path = Server.MapPath(fileToDelete);
            var isOk = await _imageService.DeleteAsync(path);
            if (!isOk)
            {
                ViewBag.Error = "Something went wrong, try again.";
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [LogFilter]
        public async Task<ActionResult> Upload(HttpPostedFileBase files)
        {
            byte[] data;
            using (Stream inputStream = files.InputStream)
            {

                if (!(inputStream is MemoryStream memoryStream))
                {
                    memoryStream = new MemoryStream();
                    await inputStream.CopyToAsync(memoryStream);
                }
                data = memoryStream.ToArray();

            }

            var defaultPath = GalleryConfigurationManager.GetPathToSave();
            var DirPath = Server.MapPath(defaultPath) + _hashService.ComputeSha256Hash(User.Identity.Name);
            var filePath = Path.Combine(DirPath, files.FileName);
            var userId = Convert.ToInt32(User.Identity.Name);
            var isOk = await _imageService.UploadImageAsync(userId, data, filePath);
            if (!isOk)
            {
                ViewBag.Error = "Something went wrong, try again.";
                return View("Error");
            }
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
