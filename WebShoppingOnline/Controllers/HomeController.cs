using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;
using WebShoppingOnline.Models.EF;

namespace WebShoppingOnline.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Shop Online";
            ViewBag.SeoTitle = "Shop Online";
            ViewBag.SeoDescription = "Shop Online";
            ViewBag.SeoKeywords = "Shop Online";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Refresh()
        {
            var item = new ThongKeModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            item.TuanNay = HttpContext.Application["TuanNay"].ToString();
            item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
            item.ThangNay = HttpContext.Application["ThangNay"].ToString();
            item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            item.TatCa = HttpContext.Application["TatCa"] .ToString();
            return PartialView(item);
        }

        public ActionResult Partial_Subcribe()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subcribe(Subscribe model)
        {
           if(ModelState.IsValid)
            {
                var checkEmail = db.Subscribes.FirstOrDefault(x => x.Email.Equals(model.Email));
                if (checkEmail != null)
                {
                    return Json(new { Success = false });
                }
                else
                {
                    db.Subscribes.Add(new Subscribe { Email = model.Email, CreatedDate = DateTime.Now });
                    db.SaveChanges();
                    return Json(new { Success = true });
                }
            }
            return View("Partial_Subcribe",model);
        }
    }
}