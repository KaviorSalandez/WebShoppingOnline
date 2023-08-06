using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;
using WebShoppingOnline.Models.EF;

namespace WebShoppingOnline.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index(int? page)
        {
            if(page == null)
            {
                page = 1;
            }
            var pageSize = 2;
            IEnumerable<New> news = db.News.OrderBy(x => x.CreatedDate);
          
            var pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            news = news.ToPagedList(pageNumber, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(news);
        }
        public ActionResult Details( string alias,int? id)
        {
            var item = db.News.Find(id);
            return View(item);      
        }
        public ActionResult Partial_New_Home()
        {
            var item = db.News.Take(3).ToList();
            return PartialView(item);  
        }
    }
}