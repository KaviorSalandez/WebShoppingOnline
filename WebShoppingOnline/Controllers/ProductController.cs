using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;
namespace WebShoppingOnline.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? id)
        {
            var items = db.Products.ToList();

            if (id!= null)
            {
                items = items.Where(x => x.Id == id).ToList();

            }
            return View(items);
        }
        public ActionResult ProductOfCategory(string alias, int? id)
        {
            var items = db.Products.ToList();

            if (id > 0 )
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();

            }
            var cate = db.ProductCategories.Find(id);
            if(cate != null)
            {
                ViewBag.Name = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);
        }
        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x=>x.IsHome && x.IsActive).Take(12).ToList();;    
            return PartialView(items);
        }
        public ActionResult ProductBestSale()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(10).ToList(); ;
            return PartialView(items);
        }

        //trang chi tiết sản phẩm
        public ActionResult Detail(string alias,int id)
        {
            var item = db.Products.Find(id);
           if(item != null)
            {
                ViewBag.CateName = item.ProductCategory.Title;
                ViewBag.ProductName = item.Title;
                //viết hàm đếm số lượt truy cập mỗi khi có người xem chi tiết 1 sản phẩm
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x=>x.ViewCount).IsModified=true;
                db.SaveChanges();   
            }
           
            return View(item);  
        }

    }
}