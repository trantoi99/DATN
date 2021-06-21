using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Electric.Models;

namespace Electric.Controllers
{
    public class ProductAdminController : Controller
    {
        private dbContext dbcontext = new dbContext();
        // GET: ProductAdmin
        public ActionResult Index()
        {
            var cate = dbcontext.Categories.ToList();
            return View(cate);
        }
        [HttpGet]
        public ActionResult ListProduct()
        {
            var list = dbcontext.Products.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getInfoId(int id)
        {
            if (id > 0)
            {
                var product = dbcontext.Products.FirstOrDefault(x => x.ProductID.Equals(id));
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult addPro(Product prod, HttpPostedFileBase prodFile)
        {
            var checkCate = dbcontext.Products.FirstOrDefault(x => x.ProductName == prod.ProductName);
            if (checkCate != null)
            {
                return Json(false);
            }
            else
            {
                if (prodFile != null)
                {
                    // lấy tên file
                    string fileName = Path.GetFileName(prodFile.FileName);
                    string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
                    // kiểm tra đuôi file
                    string extension = Path.GetExtension(prodFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Photos/Admin/Product/" + _fileName));
                    prod.ImageURL = _fileName;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        // kiểm tra độ rộng của file
                        if (prodFile.ContentLength <= 1000000000)
                        {
                            prodFile.SaveAs(path);
                        }
                        else
                        {
                            return Json(false);
                        }
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
                dbcontext.Products.Add(prod);
                dbcontext.SaveChanges();
                return Json(true);
            }
        }
        [HttpPost]
        public JsonResult editPro(string ProductID, string ProductNameNew, string CategoryNew, string AmountNew,
           string Description, string NewDetail, string PriceNew, HttpPostedFileBase prodFile , Product prod)
        {
            int id = Convert.ToInt32(ProductID);
            var checkUpadte = dbcontext.Products.FirstOrDefault(x => x.ProductID == id);
            if (checkUpadte != null)
            {
                //Tìm ảnh
                var findImage = Request.MapPath("~/Photos/Admin/Product/" + checkUpadte.ImageURL);
                // kiem tra xem anh co ton tai ko
                var checkImage = System.IO.File.Exists(findImage);
                if (checkImage == true)
                {
                    System.IO.File.Delete(findImage);
                }

                string fileExtension = Path.GetExtension(prodFile.FileName);
                string fileName = Path.GetFileName(prodFile.FileName);
                string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
                // kiểm tra đuôi file
                string extension = Path.GetExtension(prodFile.FileName);
                string path = Path.Combine(Server.MapPath("~/Photos/Admin/Product/" + _fileName));
                checkUpadte.ImageURL = _fileName;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    // kiểm tra độ rộng của file
                    if (prodFile.ContentLength <= 1000000000)
                    {
                        prodFile.SaveAs(path);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
                checkUpadte.ProductName = ProductNameNew;
                checkUpadte.CategoryID = Convert.ToInt32(CategoryNew);
                checkUpadte.Description = Description;
                checkUpadte.UnitPrice = Convert.ToInt32(PriceNew);
                checkUpadte.Detail = NewDetail;
                checkUpadte.Amount = Convert.ToInt32(AmountNew);

                dbcontext.SaveChanges();
            }

            return Json(checkUpadte,JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            try
            {
                var delete = dbcontext.Products.FirstOrDefault(x => x.ProductID == id);
                if (delete != null)
                {

                    dbcontext.Products.Remove(delete);
                    dbcontext.SaveChanges();
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

    }
}
