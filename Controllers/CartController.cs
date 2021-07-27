using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Electric.Models;

using Electric.EmailService;
using Electric.EmailService.Response;

namespace Electric.Controllers
{
    public class CartController : Controller
    {
        private readonly ElectricDbContext _context = new ElectricDbContext();
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Cart()
        {
            return View();
        }

       
        [HttpGet]
        public ActionResult CartProduct()
        {
            try
            {
                var cart = Session[CartSession];
                var list = (List<CartItem>)cart;
                var mycart = list.Where(x => x.UserName == Session["Account"].ToString()).ToList();
                Session["CartCount"] = mycart.Count();
                return Json(mycart, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        //Add cart
        [HttpPost]
        public JsonResult AddItems(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            var cart = Session[CartSession];
            var account = Session["Account"].ToString();
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.UserName == account))
                {
                    if (list.Exists(x => x.Product.ProductID == productId && x.UserName == account))
                    {
                        foreach (var item in list)
                        {
                            if (item.Product.ProductID == productId)
                            {
                                item.Quantity += quantity;
                            }
                        }
                    }
                    else
                    {
                        var item = new CartItem();
                        item.Product = product;
                        item.Quantity = quantity;
                        item.UserName = account;
                        list.Add(item);
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    item.UserName = account;
                    list.Add(item);
                }
                Session[CartSession] = list;
                var count = list.Where(x => x.UserName == account).Count();
                Session["CartCount"] = count;
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                item.UserName = account;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
                var count = list.Count();
                Session["CartCount"] = count;
                return Json(count, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]

        public JsonResult Delete(int Id)
        {
            try
            {
                var account = Session["Account"].ToString();
                var sessionCart = (List<CartItem>)Session[CartSession];
                sessionCart.RemoveAll(x => x.Product.ProductID == Id && x.UserName == account);
                Session[CartSession] = sessionCart;
                var count = sessionCart.Where(x => x.UserName == account).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Update(int productId, int quantity)
        {
            try
            {
                var cart = Session[CartSession];
                var list = (List<CartItem>)cart;
                var check = list.SingleOrDefault(x => x.Product.ProductID == productId && x.UserName == Session["Account"].ToString());
                if (check != null)
                {
                    check.Quantity = quantity;
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckOut()
        {
            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;
            var mycart = list.Where(x => x.UserName == Session["Account"].ToString()).ToList();
            var account = Session["Account"].ToString();
            var checkout = _context.Customers.FirstOrDefault(x => x.UserName == account);
           /* ViewBag.link = _context.MENUs.ToList();*/
            ViewBag.username = checkout.UserName;
            ViewBag.address = checkout.Address;
            ViewBag.email = checkout.Email;
            ViewBag.phone = checkout.PhoneNumber;
            return View(mycart);
        }
        [HttpPost]

        public async Task<JsonResult> oderProduct(Order order, string address, string phoneNumber)
        {
            var orderDetail = new OrderDetail();

            var userName = Session["Account"].ToString();
            var findUser = _context.Customers.FirstOrDefault(x => x.UserName == userName);
            findUser.PhoneNumber = phoneNumber;
            findUser.Address = address;
            order.CustomerID = findUser.CustomerID;
            // luu vao bang order
            _context.Orders.Add(order);
           
            var lstCart = (List<CartItem>) Session[CartSession];
            var total = 0;            
            foreach( var item in lstCart)
            {
                total = Convert.ToInt32(item.Quantity * item.Product.UnitPrice);
                orderDetail.OrderID = order.OrderID;
                orderDetail.Quantity = item.Quantity;
                orderDetail.ProductID = item.Product.ProductID;
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();

            var identityMessage = new IdentityMessage
            {
                Body = "<p>Dat order</p>",
                Destination = "Data order",
                EmailAddress = findUser.Email,
                NameObject = findUser.CustomerName,
                Subject = "Data order"
            };

            var isSendEmail = await EmailService.EmailService.SendAsync(identityMessage);
            if(isSendEmail.Successed == true) {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

       /* [HttpPost]
        public async Task<JsonResult> Payment(Order order)
        {
            try
            {
                var detailorder = new OrderDetail();
                order.DateOrder = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                _context.Orders.Add(order);
                _context.SaveChanges();
                var email = new EmailClass();
                var cart = (List<CartItem>)Session[CartSession];
                decimal total = 0;
                foreach (var item in cart)
                {
                    var totalProduct = ((item.Product.Price - item.Product.Price_Sale) * item.Quantity);
                    var productID = _context.PRODUCTs.Find(item.Product.Id);
                    productID.Views += 1;
                    detailorder.OrderId = order.Id;
                    detailorder.Product_ID = item.Product.Id;
                    detailorder.Quantity = item.Quantity;
                    detailorder.Size = item.Size;
                    detailorder.Price = item.Product.Price;
                    detailorder.Price_Sale = item.Product.Price_Sale;
                    detailorder.DateCreate = order.DateCreate;
                    detailorder.Status = false;
                    detailorder.Total = totalProduct;
                    _context.DETAIL_ORDER.Add(detailorder);
                    _context.SaveChanges();

                    total += totalProduct;
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Admin/template/sendorder.html"));

                content = content.Replace("{{CustomerName}}", order.FullName);
                content = content.Replace("{{Phone}}", order.PhoneNumber);
                content = content.Replace("{{Email}}", order.Email);
                content = content.Replace("{{Address}}", order.Address);
                content = content.Replace("{{Total}}", total.ToString("N0"));

                email.subject = "VEGEFOOD - Fresh food comes to every home";
                email.body = content;
                email.toMail = order.Email;
                var identity = new IdentityMessage()
                {
                    Destination = email.toMail,
                    Subject = email.subject,
                    Body = email.body
                };
                var emailservice = new EmailService();
                await emailservice.SendAsync(identity);
                return Json("Oke", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }*/
    }
}

