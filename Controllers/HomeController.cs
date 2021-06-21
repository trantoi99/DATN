using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electric.Models;

namespace Electric.Controllers
{
    public class HomeController : Controller
    {
        private dbContext dbContext = new dbContext();

        public ActionResult Index()
        {
            var listPro = dbContext.Products.ToList();
            return View(listPro);
        }
        public ActionResult ProductDetail(int? id)
        {
           if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Product  product= dbContext.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        
        public JsonResult removeSession()
        {
            Session.Remove("Name");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult About()
        {
            Session["Name"] = "My Name";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}