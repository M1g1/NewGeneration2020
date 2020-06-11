using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Gallery.Config.Manager;
using Gallery.Filters;
using Gallery.Service;
using Gallery.MessageQueues;
using Gallery.Service.Contract;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IHashService _hashService;
        private readonly IUsersService _usersService;
        private readonly IPublisher _publisher;
        public HomeController(IImageService imageService, IHashService hashService, IUsersService usersService, IPublisher publisher)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
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
            var fullDirTempPath = Server.MapPath(defaultTempPath);
            var extension = Path.GetExtension(files.FileName);
            var uniqFileName = _imageService.FileNameCreation();
            var fileTempPath = Path.Combine(fullDirTempPath, uniqFileName + extension);
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

            var pathToSave = GalleryConfigurationManager.GetPathToSave();
            // Directory path with all User's directories
            var fullPathToSave = Server.MapPath(pathToSave);
            // Encrypted User's directory path
            var userDirPath = fullPathToSave + _hashService.ComputeSha256Hash(User.Identity.Name);

            var filePath = Path.Combine(userDirPath, uniqFileName + extension);

            var message = new MessageDto
            {
                UserId = userId,
                Label = uniqFileName,
                Path = filePath, 
                TempPath = fileTempPath
            };
            _publisher.SetFormat(new Type[]
            {
                typeof(MessageDto)
            });
            _publisher.SendMessage(message, uniqFileName);
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Index()
        {
            var pathToSave = GalleryConfigurationManager.GetPathToSave();

            // Directory path with all User's directories
            var fullPathToSave = Server.MapPath(pathToSave);

            // Directory path with temp files
            var pathToTempDirs = Server.MapPath(GalleryConfigurationManager.GetPathToTempSave());

            if (!Directory.Exists(fullPathToSave))
            {
                Directory.CreateDirectory(fullPathToSave);
            }

            if (!Directory.Exists(pathToTempDirs))
            {
                Directory.CreateDirectory(pathToTempDirs);
            }

            if (Request.IsAuthenticated)
            {
                // Encrypted User's directory path
                var userDirPath = fullPathToSave + _hashService.ComputeSha256Hash(User.Identity.Name);
                if (!Directory.Exists(userDirPath))
                {
                    Directory.CreateDirectory(userDirPath);
                }
                var userId = Convert.ToInt32(User.Identity.Name);
                var user = await _usersService.GetUserByIdAsync(userId);
                ViewData["Email"] = user.Email;
            }
            //Directory paths with all User's files
            var imgDirsNames = Directory.GetDirectories(fullPathToSave);
            ViewBag.Titles = (from dir in imgDirsNames from fl in Directory.GetFiles(dir) select _imageService.GetTitle(fl)).ToList();
            ViewBag.Manufacturers = (from dir in imgDirsNames from fl in Directory.GetFiles(dir) select _imageService.GetManufacturer(fl)).ToList();
            ViewBag.CameraModels = (from dir in imgDirsNames from fl in Directory.GetFiles(dir) select _imageService.GetModelOfCamera(fl)).ToList();
            ViewBag.FileSizes = (from dir in imgDirsNames from fl in Directory.GetFiles(dir) select _imageService.GetFileSize(fl)).ToList();
            ViewBag.CreationDates = (from dir in imgDirsNames from fl in Directory.GetFiles(dir) select _imageService.GetDateCreation(fl)).ToList();
            ViewBag.UploadDates = (from dir in imgDirsNames from fl in Directory.GetFiles(dir) select _imageService.GetDateUpload(fl)).ToList();
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

        public async Task<ActionResult> Upload()
        {
            if (Request.IsAuthenticated)
            {
                var userId = Convert.ToInt32(User.Identity.Name);
                var user = await _usersService.GetUserByIdAsync(userId);
                ViewData["Email"] = user.Email;
            }
            return View();
        }

    }
}
