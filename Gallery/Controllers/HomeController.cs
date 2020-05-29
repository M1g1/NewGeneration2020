using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Gallery.Filters;
using Gallery.Service;
using Gallery.Manager;
using Gallery.MessageQueues;
using Gallery.Service.Contract;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IHashService _hashService;
        private readonly IPublisher _publisher;
        public HomeController(IImageService imageService, IHashService hashService, IPublisher publisher)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            
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
            byte[] fileBytes;

            using (Stream fileInputStream = files.InputStream)
            {

                if (!(fileInputStream is MemoryStream fileMemoryStream))
                {
                    fileMemoryStream = new MemoryStream();
                    await fileInputStream.CopyToAsync(fileMemoryStream);
                }
                fileBytes = fileMemoryStream.ToArray();
                fileMemoryStream.Close();
            }

            var defaultTempPath = GalleryConfigurationManager.GetPathToTempSave();
            var dirTempPath = Server.MapPath(defaultTempPath);
            var extension = Path.GetExtension(files.FileName);
            var uniqFileName = _imageService.FileNameCreation();
            var fileTempPath = Path.Combine(dirTempPath, uniqFileName + extension);
            var userId = Convert.ToInt32(User.Identity.Name);
            var ipAddress = HttpContext.Request.UserHostAddress;
            var mediaUploadAttemptDto = new MediaUploadAttemptDto
            {
                Label = uniqFileName,
                UserId = userId,
                IsInProgress = true,
                IsSuccess = false,
                IpAddress = ipAddress,
                TimeStamp = DateTime.Now
            };
            var isOk = await _imageService.UploadImageTemporaryAsync(mediaUploadAttemptDto, fileBytes, fileTempPath);
            if (!isOk)
            {
                ViewBag.Error = "Something went wrong, try again.";
                return View("Error");
            }
            _publisher.SendMessage(fileTempPath, uniqFileName);
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
