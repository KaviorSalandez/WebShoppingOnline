using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;
using WebShoppingOnline.Models.EF;

namespace WebShoppingOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class NewController : Controller
    {
        // GET: Admin/New
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var news = db.News.OrderByDescending(x=>x.Id).ToList();
            return View(news);
        }
        public ActionResult Create()
        {
            ViewBag.listCategory = db.Categoties.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(New model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebShoppingOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title);
                db.News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.listCategory = db.Categoties.ToList();
            var model = db.News.Find(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(New model)
        {
            if (ModelState.IsValid)
            {
                var item = db.News.Find(model.Id);
                item.ModifiedDate = DateTime.Now;
                item.Alias = WebShoppingOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title);
                item.Description = model.Description;
                item.Title = model.Title;
                item.CategoryID = model.CategoryID;
                item.SeoTitle = model.SeoTitle;
                item.SeoDescription = model.SeoDescription;
                item.SeoKeywords = model.SeoKeywords;
                item.Detail = model.Detail;
                item.Image = model.Image;
                item.IsActive = model.IsActive;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public ActionResult ChangeActive1(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items != null && items.Any())
                {
                    //xóa từng thằng một
                    foreach (var item in items)
                    {
                        var obj = db.News.Find(Convert.ToInt32(item));
                        db.News.Remove(obj);
                        db.SaveChanges();
                    }
                    
                }
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }




    }
}