using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTuDien.Models;

namespace WebsiteTuDien.Controllers
{
    public class SiteController : Controller
    {
        private WebsiteTuDienDbContext db = new WebsiteTuDienDbContext();
        // GET: Site
        public ActionResult Index(String slug = "")
        {
            int pageNumber = 1;
            Session["keywords"] = null;
            if (!string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                pageNumber = int.Parse(Request.QueryString["page"].ToString());
            }
            if (slug == "")
            {
                return this.Home();
            }
            else
            {
                var link = db.Link.Where(m => m.Slug == slug);
                if (link.Count() > 0)
                {
                    var res = link.First();
                    if (res.Type == "page")
                    {
                        return this.PostPage(slug);
                    }
                    else if (res.Type == "topic")
                    {
                        return this.PostTopic(slug, pageNumber);
                    }
                    else if (res.Type == "category")
                    {
                        return this.ProductCategory(slug, pageNumber);
                    }
                }
                else
                {
                    if (db.Product.Where(m => m.Slug == slug && m.Status == 1).Count() > 0)
                    {
                        return this.ProductDetail(slug);
                    }
                    else if (db.Post.Where(m => m.Slug == slug && m.Type == "post" && m.Status == 1).Count() > 0)
                    {
                        return this.PostDetail(slug);
                    }
                }
                return this.Error(slug);
            }
        }

        public ActionResult ProductDetail(String slug)
        {
            var model = db.Product
                .Where(m => m.Slug == slug && m.Status == 1)
                .First();
            int catid = model.CatID;

            List<int> listcatid = new List<int>();
            listcatid.Add(catid);

            var list2 = db.Category
                .Where(m => m.ParentId == catid)
                .Select(m => m.Id)
                .ToList();
            foreach (var id2 in list2)
            {
                listcatid.Add(id2);
                var list3 = db.Category
                    .Where(m => m.ParentId == id2)
                    .Select(m => m.Id)
                    .ToList();
                foreach (var id3 in list3)
                {
                    listcatid.Add(id3);
                }
            }
            // danh mục cùng sản phẩm
            ViewBag.listother = db.Product
                .Where(m => m.Status == 1 && listcatid
                .Contains(m.CatID) && m.Id != model.Id)
                .OrderByDescending(m => m.Created_at)
                .Take(12)
                .ToList();
            // sản phẩm mới nhập
            ViewBag.news = db.Product
                .Where(m => m.Status == 1 /*&& listcatid.Contains(m.CatId)*/ && m.Id != model.Id)
                .OrderByDescending(m => m.Created_at).Take(4).ToList();
            return View("ProductDetail", model);
        }
        public ActionResult PostDetail(String slug)
        {
            var model = db.Post
                 .Where(m => m.Slug == slug && m.Status == 1)
                 .First();
            int topid = model.TopicID;

            List<int> listtopid = new List<int>();
            listtopid.Add(topid);

            var list2 = db.Topic
                .Where(m => m.ParentID == topid)
                .Select(m => m.ID)
                .ToList();
            foreach (var id2 in list2)
            {
                listtopid.Add(id2);
                var list3 = db.Topic
                    .Where(m => m.ParentID == id2)
                    .Select(m => m.ID)
                    .ToList();
                foreach (var id3 in list3)
                {
                    listtopid.Add(id3);
                }
            }
            // danh mục cùng bài viết
            ViewBag.listother = db.Post
                .Where(m => m.Status == 1 && listtopid
                .Contains(m.TopicID) && m.ID != model.ID)
                .OrderByDescending(m => m.Created_at)
                .Take(12)
                .ToList();

            return View("PostDetail", model);
        }
        public ActionResult Error(String slug)
        {
            return View("Error");
        }

        public ActionResult PostPage(String slug)
        {
            var item = db.Post
                .Where(m => m.Slug == slug && m.Type == "page")
                 .First();
            return View("PostPage", item);
        }

        public ActionResult PostTopic(String slug, int pageNumber)
        {
            int pageSize = 8;
            var row_cat = db.Topic
                .Where(m => m.Slug == slug)
                .First();
            List<int> listtopid = new List<int>();
            listtopid.Add(row_cat.ID);

            var list2 = db.Topic
                .Where(m => m.ParentID == row_cat.ID)
                .Select(m => m.ID)
                .ToList();
            foreach (var id2 in list2)
            {
                listtopid.Add(id2);
                var list3 = db.Topic
                    .Where(m => m.ParentID == id2)
                    .Select(m => m.ID)
                    .ToList();
                foreach (var id3 in list3)
                {
                    listtopid.Add(id3);
                }
            }
            var list = db.Post
                .Where(m => m.Status == 1 && listtopid.Contains(m.TopicID))
                .OrderByDescending(m => m.Created_at);


            ViewBag.Slug = slug;
            ViewBag.CatName = row_cat.Name;
            return View("PostTopic", list
                .ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ProductCategory(String slug, int pageNumber)
        {
            int pageSize = 8;
            var row_cat = db.Category
                .Where(m => m.Slug == slug)
                .First();
            List<int> listcatid = new List<int>();
            listcatid.Add(row_cat.Id);

            var list2 = db.Category
                .Where(m => m.ParentId == row_cat.Id)
                .Select(m => m.Id)
                .ToList();
            foreach (var id2 in list2)
            {
                listcatid.Add(id2);
                var list3 = db.Category
                    .Where(m => m.ParentId == id2)
                    .Select(m => m.Id)
                    .ToList();
                foreach (var id3 in list3)
                {
                    listcatid.Add(id3);
                }
            }
            var list = db.Product
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatID))
                .OrderByDescending(m => m.Created_at);
            //var list = from p in db.Product
            //           join c in db.Category
            //           on p.CatID equals c.Id
            //           where p.Status != 0 && listcatid.Contains(p.CatID)
            //           orderby p.Created_at descending
            //           select new ProductCategory()
            //           {
            //               ProductId = p.Id,
            //               ProductImg = p.Img,
            //               ProductName = p.Name,
            //               Producttatus = p.Status,
            //               ProductDetail = p.Detail,
            //               ProductPrice = p.Price,
            //               ProductPrice_Sale = p.Price_sale,
            //               Productlug = p.Slug,
            //               CategoryName = c.Name
            //           };

            ViewBag.Slug = slug;
            ViewBag.CatName = row_cat.Name;
            return View("ProductCategory", list
                .ToPagedList(pageNumber, pageSize));
        }
        //Trang Chủ
        public ActionResult Home()
        {
            var list = db.Category
               .Where(m => m.Status == 1 && m.ParentId == 0)
               .Take(8)
               .ToList();
            return View("Home", list);
        }
        public ActionResult Other()
        {
            return View("Other");
        }
        //Sản phẩm trang chủ
        public ActionResult ProductHome(int catid)
        {
            List<int> listcatid = new List<int>();
            listcatid.Add(catid);

            var list2 = db.Category
                .Where(m => m.ParentId == catid).Select(m => m.Id)
                .ToList();
            foreach (var id2 in list2)
            {
                listcatid.Add(id2);
                var list3 = db.Category
                    .Where(m => m.ParentId == id2)
                    .Select(m => m.Id).ToList();
                foreach (var id3 in list3)
                {
                    listcatid.Add(id3);
                }
            }

            var list = db.Product
                .Where(m => m.Status == 1 && listcatid
                .Contains(m.CatID))
                .Take(12)
                .OrderByDescending(m => m.Created_at);
            //var list = from p in db.Product
            //           join c in db.Category
            //           on p.CatID equals c.Id
            //           where (p.Status == 1 && listcatid
            //           .Contains(p.CatID))

            //           orderby p.Created_at descending

            //           select new ProductCategory()
            //           {
            //               ProductId = p.Id,
            //               ProductImg = p.Img,
            //               ProductName = p.Name,
            //               Producttatus = p.Status,
            //               ProductDetail = p.Detail,
            //               ProductPrice = p.Price,
            //               ProductPrice_Sale = p.Price_sale,
            //               Productlug = p.Slug,
            //               CategoryName = c.Name
            //           };
            return View("_ProductHome", list);
        }
        //Tat ca sp
        public ActionResult Product(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            var list = db.Product.Where(m => m.Status == 1)
                .OrderByDescending(m => m.Created_at)
                .ToPagedList(pageNumber, pageSize);
            return View(list);
        }
        // tìm kiếm sản phẩm
        public ActionResult Search(String key, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var list = db.Product.Where(m => m.Status == 1);
            if (String.IsNullOrEmpty(key.Trim()))
            {
                return RedirectToAction("Index", "Site");

            }
            else
            {
                list = list.Where(m => m.Name.Contains(key)).OrderByDescending(m => m.Created_at);
            }

            Session["keywords"] = key;
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        //Tat ca bai viet
        public ActionResult Post(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            var list = db.Post.Where(m => m.Status == 1 && m.Type == "post")
                .OrderByDescending(m => m.Created_at)
                .ToPagedList(pageNumber, pageSize);
            return View(list);
        }
        //Trang Chủ Bài viết Topic
        public ActionResult HomePost() // url tin-tuc
        {
            var list = db.Topic
               .Where(m => m.Status == 1 && m.ParentID == 0)
               .Take(8)
               .ToList();
            return View("_HomePost", list);
        }

        //Trang chủ bài viết Post
        public ActionResult PostHome(int topid)
        {
            List<int> listtopid = new List<int>();
            listtopid.Add(topid);

            var list2 = db.Topic
                .Where(m => m.ParentID == topid).Select(m => m.ID)
                .ToList();
            foreach (var id2 in list2)
            {
                listtopid.Add(id2);
                var list3 = db.Topic
                    .Where(m => m.ParentID == id2)
                    .Select(m => m.ID).ToList();
                foreach (var id3 in list3)
                {
                    listtopid.Add(id3);
                }
            }

            var list = db.Post
                .Where(m => m.Status == 1 && listtopid
                .Contains(m.TopicID))
                .Take(12)
                .OrderByDescending(m => m.Created_at);
            return View("_PostHome", list);
        }
        //Tin tức mới nhất trang chủ
        public ActionResult PostNew(int topid)
        {
            List<int> listtopid = new List<int>();
            listtopid.Add(topid);

            var list2 = db.Topic
                .Where(m => m.ParentID == topid).Select(m => m.ID)
                .ToList();
            foreach (var id2 in list2)
            {
                listtopid.Add(id2);
                var list3 = db.Topic
                    .Where(m => m.ParentID == id2)
                    .Select(m => m.ID).ToList();
                foreach (var id3 in list3)
                {
                    listtopid.Add(id3);
                }
            }

            var list = db.Post
                .Where(m => m.Status == 1 && listtopid
                .Contains(m.TopicID))
                .Take(12)
                .OrderByDescending(m => m.Created_at);
            //var list = from p in db.Post
            //           join t in db.Topic
            //           on p.Topid equals t.Id
            //           where (p.Status == 1 && listtopid
            //           .Contains(p.Topid))

            //           orderby p.Created_At descending

            //           select new PostTopic()
            //           {
            //               PostId = p.Id,
            //               PostImg = p.Img,
            //               PostName = p.Title,
            //               Posttatus = p.Status,
            //               PostDetail = p.Detail,
            //               Postlug = p.Slug,
            //               TopicName = t.Name
            //           };
            return View("_PostNew", list);
        }
        public ActionResult HomePostNew()
        {
            var list = db.Topic
               .Where(m => m.Status == 1 && m.ParentID == 0)
               .Take(2)
               .OrderByDescending(m => m.Created_at)
               .ToList();
            return View("HomePostNew", list);
        }
        //home sale (show sản phẩm khuyến mãi)
        public ActionResult HomeProductale()
        {
            var list = db.Product.Where(m => m.Status == 1 && m.Discount == 1)
                .OrderByDescending(m => m.Created_at);
            return View("_HomeProductale", list);
        }
        //sản phẩm khuyến mãi  (san-pham-khuyen-mai)
        public ActionResult Productale(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            var list = db.Product.Where(m => m.Status == 1 && m.Discount == 1)
                .OrderByDescending(m => m.Created_at)
                .ToPagedList(pageNumber, pageSize);
            return View(list);
        }
    }
}