using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace WebShoppingOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Role
        public ActionResult Index()
        {
            var item = db.Roles.ToList();
            return View(item);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if(ModelState.IsValid)
            {
                var roleManage = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManage.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = db.Roles.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManage = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManage.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}