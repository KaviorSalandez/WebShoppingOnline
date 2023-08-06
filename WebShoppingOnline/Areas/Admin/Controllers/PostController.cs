using PagedList;
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
    public class PostController : Controller
    {
        // GET: Admin/Post
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string searchText,int? page)
        {
            var pageSize = 4;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Post> posts = db.Posts.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(searchText))
            {
              posts =  posts.Where(x=>x.Title.ToLower().Contains(searchText.ToLower()));
            }
            var pageNumber = page.HasValue? Convert.ToInt32(page):1;
            posts = posts.ToPagedList(pageNumber,pageSize);
            ViewBag.PageSize = pageSize;    
            ViewBag.Page = page;    
            return View(posts);
        }
        public ActionResult Create()
        {
            ViewBag.listCategory = db.Categoties.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebShoppingOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title);
                db.Posts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.listCategory = db.Categoties.ToList();
            var model = db.Posts.Find(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                var item = db.Posts.Find(model.Id);
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
            var item = db.Posts.Find(id);
            if (item != null)
            {
                db.Posts.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public ActionResult ChangeActive1(int id)
        {
            var item = db.Posts.Find(id);
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
                if (items != null && items.Any())
                {
                    //xóa từng thằng một
                    foreach (var item in items)
                    {
                        var obj = db.Posts.Find(Convert.ToInt32(item));
                        db.Posts.Remove(obj);
                        db.SaveChanges();
                    }

                }
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}