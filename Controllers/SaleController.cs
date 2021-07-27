using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electric.Models;

namespace Electric.Controllers
{
    public class SaleController : Controller
    {
        private readonly ElectricDbContext dbContext = new ElectricDbContext();
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listSale()
        {
            var listSale = new List<Sale>();
            var list = dbContext.Sales.ToList();
            foreach (var item in list)
            {
                var model = new Sale();
                model.SaleID = item.SaleID;
                model.Name = item.Name;
                model.DateStart = item.DateEnd;
                model.DateEnd = item.DateEnd;
                listSale.Add(model);
            }
            return Json(listSale, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult addSale(Sale sale)
        {
            var checkSale = dbContext.Sales.FirstOrDefault(x => x.Name == sale.Name);
            if (checkSale != null)
            {
                return Json(false);
            }
            else
            {
                dbContext.Sales.Add(sale);
                dbContext.SaveChanges();
                return Json(true);
            }
        }


        /*[HttpPost]
        public JsonResult editCate(string CateID, string CategoryNameNew, HttpPostedFileBase cateFile)
        {
           

        }*/

       


        //[HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                var delete = dbContext.Sales.FirstOrDefault(x => x.SaleID == id);
                if (delete != null)
                {

                    dbContext.Sales.Remove(delete);
                    dbContext.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch
            {
                return Json(false);
            }
        }
        [HttpGet]
        public JsonResult getInfoId(int id)
        {
            if (id > 0)
            {
                var cate = dbContext.Sales.FirstOrDefault(x => x.SaleID == id);
                return Json(cate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}