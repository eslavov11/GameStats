using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStats.Models;

namespace GameStats.Controllers
{
    public class ImageController : Controller
    {
        private Context db = new Context();

        public ActionResult Show()
        {
            return View();
        }
        public ActionResult GetImage(int id)
        {
            var image = db.HUMANS.FirstOrDefault(i => i.ID == id);
            byte[] imageData = image.PICTURE;
            return File(imageData, "image/jpg");
        }
	}
}