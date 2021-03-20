using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTuDien.Models;

namespace WebsiteTuDien.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        private WebsiteTuDienDbContext db = new WebsiteTuDienDbContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.CountOrderSuccess = db.Order.Where(m => m.Status == 3).Count();
            ViewBag.CountOrderCancel = db.Order.Where(m => m.Status == 1).Count();
            ViewBag.CountContactDoneReply = db.Contact.Where(m => m.Flag == 0).Count();
            ViewBag.CountUser = db.User.Where(m => m.Status != 0).Count();
            return View();
        }
    }
}