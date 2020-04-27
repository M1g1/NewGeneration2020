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
            try
            {
                if (string.IsNullOrEmpty(fileToDelete))
                {
                    ViewBag.Error = "Path not found!";
                    return View("Error");
                }
                else
                { 
                    var fullPathToImage = Server.MapPath(fileToDelete);
                    var fileExist = System.IO.File.Exists(fullPathToImage);
                    if (!fileExist)
                    {
                        ViewBag.Error = "File not found!";
                        return View("Error");
                    }
                    _imageService.Delete(fullPathToImage);
                }
            }
            catch (Exception err)
            {
                ViewBag.Error = "Unexpected error: " + err.Message;
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files)
        {
            try
            {
                // Verify that the user selected a file
                if (files != null)
                {
                    var imgTypes = GalleryConfigurationManager.GetAvailableImageTypes();
                    if (imgTypes.Contains(files.ContentType))
                    {
                        FileStream TempFileStream;

                        if (files.ContentLength > 0)
                        {
                            bool IsLoad = true;

                            var pathToSave = GalleryConfigurationManager.GetPathToSave();

                            // Encrypted User's directory path
                            string DirPath = Server.MapPath(pathToSave) + _hashService.ComputeSha256Hash("Temporary");

                            // extract only the filename
                            var fileName = Path.GetFileName(files.FileName);
                            // store the file inside ~/Content/Temp folder
                            var TempPath = Path.Combine(Server.MapPath("~/Content/Temp"), fileName);
                            files.SaveAs(TempPath);
                            TempFileStream = new FileStream(TempPath, FileMode.Open);
                            BitmapSource img = BitmapFrame.Create(TempFileStream);
                            BitmapMetadata md = (BitmapMetadata)img.Metadata;
                            var DateTaken = md.DateTaken;

                            TempFileStream.Close();

                            if (!string.IsNullOrEmpty(DateTaken) || files.ContentType != "image/jpeg")
                            {
                                if (Convert.ToDateTime(DateTaken) >= DateTime.Now.AddYears(-1) || files.ContentType != "image/jpeg")
                                {
                                    TempFileStream = new FileStream(TempPath, FileMode.Open);
                                    Bitmap TempBmp = new Bitmap(TempFileStream);
                                    TempBmp = new Bitmap(TempBmp, 64, 64);
                                    TempFileStream.Close();

                                    // List of all Directories names
                                    List<string> dirsname = Directory.GetDirectories(Server.MapPath(pathToSave)).ToList<string>();

                                    FileStream CheckFileStream;
                                    Bitmap CheckBmp;

                                    List<string> filesname;

                                    // foreach inside foreach in order to check a new photo for its copies in all folders of all users
                                    foreach (string dir in dirsname)
                                    {
                                        filesname = Directory.GetFiles(dir).ToList<string>();
                                        foreach (string fl in filesname)
                                        {
                                            CheckFileStream = new FileStream(fl, FileMode.Open);
                                            CheckBmp = new Bitmap(CheckFileStream);
                                            CheckBmp = new Bitmap(CheckBmp, 64, 64);

                                            CheckFileStream.Close();

                                            if (_imageService.CompareBitmaps(TempBmp, CheckBmp))
                                            {
                                                IsLoad = false;
                                                ViewBag.Error = "Photo already exists!";
                                                CheckBmp.Dispose();
                                                break;
                                            }
                                            else
                                                CheckBmp.Dispose();
                                        }
                                    }
                                }
                                else
                                {
                                    ViewBag.Error = "Photo created more than a year ago!";
                                    IsLoad = false;
                                }
                            }
                            else
                            {
                                ViewBag.Error = "Photo creation date not found!";
                                IsLoad = false;
                            }

                            if (IsLoad)
                            {
                                // extract only the filename
                                var OriginalFileName = Path.GetFileName(files.FileName);
                                // store the file inside User's folder
                                var OriginalPath = Path.Combine(DirPath, OriginalFileName);
                                //System.Windows.MessageBox.Show(OriginalPath);
                                files.SaveAs(OriginalPath);
                                System.IO.File.Delete(TempPath);
                            }
                            else
                            {
                                System.IO.File.Delete(TempPath);
                                return View("Error");
                            }
                        }
                        else
                        {
                            ViewBag.Error = "File too small!";
                            return View("Error");
                        }
                        // redirect back to the index action to show the form once again
                    }
                    else
                    {
                        ViewBag.Error = "Inappropriate format!";
                        return View("Error");
                    }
                }
                else
                {
                    //System.Windows.MessageBox.Show("pusto");
                    return View();
                }
            }
            catch (Exception err)
            {

                /*ViewBag.Error = "Unexpected error: " + err.Message;
                return View("Error");*/

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
