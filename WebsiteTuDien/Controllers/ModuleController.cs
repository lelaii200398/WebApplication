using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTuDien.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Modules
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            return View("_Header");
        }
        public ActionResult Footer()
        {
            return View("_Footer");
        }
        public ActionResult SlideShow()
        {
            return View("_SlideShow");
        }
        public ActionResult Post()
        {
            return View("_Post");
        }
        public ActionResult Discount()
        {
            return View("_Discount");
        }
        public ActionResult Subscribe()
        {
            return View("_Subscribe");
        }
    }
}