using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfiniteScrollMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Collects images from gallery and creates an Json object from anonymous result set
        /// </summary>
        /// <param name="page">Page to get</param>
        /// <param name="rows">Number of images to retrieve</param>
        /// <returns>Json: Collection of image details</returns>
        public JsonResult GetGallery(int page = 0, int rows = 16)
        {
            var dir = new DirectoryInfo(Server.MapPath("~/Images/gallery"));
            if (dir.Exists)
            {
                var filters = new String[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp" };
                var images = (from i in dir.EnumerateFiles()
                              where filters.Contains(i.Extension.ToLower())
                              select new
                              {
                                  Name = i.Name,
                                  Path = "../Images/gallery/" + i.Name
                              }).Skip(page * rows).Take(rows);

                return Json(images, JsonRequestBehavior.AllowGet);
            }

            return null;
        } 
    }
}