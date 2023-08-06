using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;

namespace WebShoppingOnline.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var item = db.Categoties.OrderBy(x=>x.Position).ToList();
            return PartialView("MenuTop",item);
        }
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.OrderBy(x => x.Id).ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public ActionResult MenuNewArrivals()
        {
            var items = db.ProductCategories.OrderBy(x => x.Id).ToList();
            return PartialView("_MenuNewArrivals",items);
        }
        public ActionResult MenuLeftProductCategory(int? id)
        {
            if(id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.ProductCategories.OrderBy(x => x.Id).ToList();
            return PartialView("_MenuLeftProductCategory", items);
        }

    }
}