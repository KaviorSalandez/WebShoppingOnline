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
    public class CategoryController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var items = _dbContext.Categoties.ToList();
            return View(items);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebShoppingOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title);
                _dbContext.Categoties.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbContext.Categoties.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var item = _dbContext.Categoties.Find(model.Id);
                item.ModifiedDate = DateTime.Now;
                item.Alias = WebShoppingOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title);
                item.Position = model.Position;
                item.Description = model.Description;
                item.Title = model.Title;
                item.SeoTitle = model.SeoTitle;
                item.SeoDescription = model.SeoDescription;
                item.SeoKeywords = model.SeoKeywords;
                item.IsActive = model.IsActive;
                item.Link = model.Link;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }



        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Categoties.Find(id);
            if (item != null)
            {
                _dbContext.Categoties.Remove(item);
                _dbContext.SaveChanges();
                return Json(new {success = true });
            }
            return Json(new { success = false });
        }



        [HttpPost]
        public ActionResult ChangeActive(int id)
        {
            var item = _dbContext.Categoties.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbContext.SaveChanges();
                return Json(new { success = true ,isActive = item.IsActive});
            }
            return Json(new { success = false });
        }




    }
}