using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;

namespace WebShoppingOnline.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();   

        // GET: Contact
        public ActionResult Index()
        {

            return View();
        }

    }
}