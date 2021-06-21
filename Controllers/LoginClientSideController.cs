using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electric.Models;

namespace Electric.Controllers
{
    public class LoginClientSideController : Controller
    {
        public dbContext dbcontext = new dbContext();
       
        // GET: LoginClientSide
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            if (username != null && password != null)
            {
                var sigin = dbcontext.Customers.FirstOrDefault(x => x.UserName == username);
                if (sigin == null)
                {
                    return Json("Your account is not existed!" , JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var checkPass = dbcontext.Customers.FirstOrDefault(x => x.Password == password);
                    if (checkPass == null)
                    {
                        return Json("Wrong password", JsonRequestBehavior.AllowGet);
                    }
                    Session["Account"] = username;
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(Customer obj)
        {
            
            try {
                var checkUser = dbcontext.Customers.FirstOrDefault(x => x.UserName == obj.UserName);
                if(checkUser != null)
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    dbcontext.Customers.Add(obj);
                    dbcontext.SaveChanges();
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(ex , JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Logout()
        {
            Session.Remove("Account");
            return Json("Logout", JsonRequestBehavior.AllowGet);
        }
    }
}