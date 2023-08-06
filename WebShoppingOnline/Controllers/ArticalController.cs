using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;

namespace WebShoppingOnline.Controllers
{
    public class ArticalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Artical
        public ActionResult Index(string alias)
        {
            var items = db.Posts.FirstOrDefault(x => x.Alias == alias);
            return View(items);
        }
    }
}