using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;
using WebShoppingOnline.Models.EF;

namespace WebShoppingOnline.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();   
        // GET: Admin/ProductImage
        public ActionResult Index(int productId)
        {
            ViewBag.ProductID = productId;
            var items = db.ProductImages.Where(x=>x.ProductId == productId).ToList();
            return View(items);
        }
       
        [HttpPost]
        public ActionResult AddImage(int id, string url)  
        {
            db.ProductImages.Add(new ProductImage
            {
                ProductId = id,
                Image = url,
                IsDefault = false,
            });
            db.SaveChanges();
            return Json(new {Success=true});
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id);
            db.ProductImages.Remove(item);
            db.SaveChanges();
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult ChangeImageDefault(int idProduct,int id)
        {


            var item1 = db.ProductImages.Where(x=>x.ProductId==idProduct).ToList();
            foreach (var image in item1)
            {
                image.IsDefault = false;
            }
            var item = db.ProductImages.Find(id);
            item.IsDefault = true;
            db.SaveChanges();
            return Json(new { Success = true });
        }
    }
}