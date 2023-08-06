using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;
using PagedList;
using System.Data.Entity.Migrations;

namespace WebShoppingOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var items = db.Orders.OrderByDescending(x => x.CreatedDate).ToList();
           
            // nếu page == null thì pageNumber =1;
            // nếu page != null thì pageNumber = page
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ViewOrder(int id)
        {
            var order = db.Orders.Find(id);
            if (order != null)
            {
                return View(order);
            }
            return View();
        }

        public ActionResult Partial_SanPham(int idOrder)
        {
            var item = db.OrderDetails.Where(x=>x.OrderId==idOrder).ToList();
            return PartialView(item);
        }
        [HttpPost]
        public ActionResult UpdateTT (int id, int TrangThai) {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = TrangThai;
                db.Entry(item).Property(x=>x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { Success = true, msg = "Success"});
            }
            return Json(new { Success = false, msg = "Fail" });
        }

    }
}