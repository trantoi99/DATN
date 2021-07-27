using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Electric.Models;

namespace Electric.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ElectricDbContext dbContext = new ElectricDbContext();

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listCategory()
        {
            var listCate = new List<Category>();
            var list = dbContext.Categories.ToList();
            foreach (var item in list)
            {
                var model = new Category();
                model.CategoryID = item.CategoryID;
                model.CategoryName = item.CategoryName;
                model.ImageURL = item.ImageURL;
                listCate.Add(model);
            }
            return Json(listCate, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult addCate(Category cate, HttpPostedFileBase cateFile)
        {
            var checkCate = dbContext.Categories.FirstOrDefault(x => x.CategoryName == cate.CategoryName);
            if (checkCate != null)
            {
                return Json(false);
            }
            else
            {
                if (cateFile != null)
                {
                    var findImage = Request.MapPath("~/Photos/Admin/Product/" + checkCate.ImageURL);
                    // kiem tra xem anh co ton tai ko
                    var checkImage = System.IO.File.Exists(findImage);
                    if (checkImage == true)
                    {
                        System.IO.File.Delete(findImage);
                    }
                    // lấy tên file
                    string fileName = Path.GetFileName(cateFile.FileName);
                    string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
                    // kiểm tra đuôi file
                    string extension = Path.GetExtension(cateFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Photos/Admin/Category/" + _fileName));
                    cate.ImageURL = _fileName;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        // kiểm tra độ rộng của file
                        if (cateFile.ContentLength <= 1000000000)
                        {
                            cateFile.SaveAs(path);
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
                dbContext.Categories.Add(cate);
                dbContext.SaveChanges();
                return Json(true);
            }
        }


        [HttpPost]
        public JsonResult editCate(string CateID, string CategoryNameNew, HttpPostedFileBase cateFile)
        {
            int id = Convert.ToInt32(CateID);
            var checkUpadte = dbContext.Categories.FirstOrDefault(x => x.CategoryID == id);
            if (checkUpadte != null)
            {
                //Tìm ảnh
                var findImage = Request.MapPath("~/Photos/Admin/Category/" + checkUpadte.ImageURL);
                // kiem tra xem anh co ton tai ko
                var checkImage = System.IO.File.Exists(findImage);
                if (checkImage == true)
                {
                    System.IO.File.Delete(findImage);
                    string fileExtension = Path.GetExtension(cateFile.FileName);
                    string fileName = Path.GetFileName(cateFile.FileName);
                    string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
                    // kiểm tra đuôi file
                    string extension = Path.GetExtension(cateFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Photos/Admin/Category/" + _fileName));
                    checkUpadte.ImageURL = _fileName;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        // kiểm tra độ rộng của file
                        if (cateFile.ContentLength <= 1000000000)
                        {
                            cateFile.SaveAs(path);
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
                    checkUpadte.CategoryName = CategoryNameNew;
                    checkUpadte.ImageURL = cateFile.FileName;
                    dbContext.SaveChanges();
                }
                else
                {
                    return Json(false);
                }
            }


            return Json(false);

        }

        [HttpPost]
        public ActionResult Search(string tuKhoa)
        {

            IQueryable<Category> lst = dbContext.Categories;
            //Nếu có từ khóa cần tìm kiếm

            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.CategoryName.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.CategoryID));
        }


        //[HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                var delete = dbContext.Categories.FirstOrDefault(x => x.CategoryID == id);
                if (delete != null)
                {

                    dbContext.Categories.Remove(delete);
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
                var cate = dbContext.Categories.FirstOrDefault(x => x.CategoryID == id);
                return Json(cate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}