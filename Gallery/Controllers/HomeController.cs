using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using System.Drawing;
using Gallery.Core;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private static string pathToSave = GalleryConfigurationManager.GetPathToSave();
        private static string imageType = GalleryConfigurationManager.GetAvailableImageTypes();

       
        [HttpPost]
        public ActionResult Delete(string fileToDelete)
        {
            try
            { 
                if (!string.IsNullOrEmpty(fileToDelete))
                {
                    string dirHashName = fileToDelete.Replace(pathToSave, "").Replace(Path.GetFileName(fileToDelete), "").Replace("/", "");

                    if (dirHashName == Servises.ComputeSha256Hash(User.Identity.Name))
                    {
                        if (Directory.Exists(Server.MapPath(fileToDelete.Replace(Path.GetFileName(fileToDelete), ""))))
                            System.IO.File.Delete(Server.MapPath(fileToDelete));
                        else
                        {
                            ViewBag.Error = "File not found!";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Authorisation Error!";
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.Error = "Path not found!";
                    return View("Error");
                }
            }
            catch(Exception err)
            {
                ViewBag.Error = "Unexpected error: " + err.Message;
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files)
        {
            try
            {
                // Verify that the user selected a file
                if (files != null)
                {
                    // Verify that the User is logged in
                    if (!string.IsNullOrEmpty(User.Identity.Name))
                    {
                        if (imageType.Contains(files.ContentType))
                        {
                            FileStream TempFileStream;
                            
                            if (files.ContentLength > 0)
                            {
                                bool IsLoad = true;

                                // Encrypted User's directory path
                                string DirPath = Server.MapPath(pathToSave) + Servises.ComputeSha256Hash(User.Identity.Name);

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
                                                
                                                if (Servises.CompareBitmapsFast(TempBmp, CheckBmp))
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
                        ViewBag.Error = "Log in please!";
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
            //System.Windows.MessageBox.Show(pathToSave);
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