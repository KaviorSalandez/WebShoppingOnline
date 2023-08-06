using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebShoppingOnline.Models;
using WebShoppingOnline.Models.EF;
namespace WebShoppingOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Count >0)
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }


        public ActionResult SuccessOrder()
        {
            return View();
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]            
        
        public ActionResult CheckOut(OrderViewModel model)
        {
            var code = new { Success = false, Code = -1 };
            if(ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart != null && cart.Items.Any())
                {
                    Order or = new Order();
                    or.CustomerName = model.CustomerName;
                    or.Address = model.Address;
                    or.Phone    = model.Phone;
                    or.Email = model.Email;
                    cart.Items.ForEach(x => or.OrderDetails.Add(new OrderDetail
                    {
                        OrderId  = or.Id,
                        ProductId= x.ProductId,
                        Quantity = x.Quantity,
                        Price    = x.Price
                    }));
                    or.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));
                    or.TypePayment = model.TypePayment;
                    or.CreatedDate  = DateTime.Now;
                    or.CreatedBy = model.Phone;
                    or.ModifiedDate = DateTime.Now;
                    Random rd = new Random();
                    or.Code = "DH" + rd.Next(0,9)+ rd.Next(0, 9)+ rd.Next(0, 9)+ rd.Next(0, 9);
                    db.Orders.Add(or);
                    db.SaveChanges();
                    // send mail for khách hàng
                    var strSanPham = "";
                    var thanhTien = decimal.Zero;
                    var tongTien = decimal.Zero;
                    foreach (var sp in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>"+sp.ProductName+"</td>";
                        strSanPham += "<td>"+"x"+sp.Quantity+"</td>";
                        strSanPham += "<td>"+WebShoppingOnline.Common.Common.FormatNumber(sp.PriceTotal,0) +"</td>";
                        strSanPham += "</tr>";
                        thanhTien += sp.Price * sp.Quantity;
                    }
                    // nếu chuẩn thì tổng tiền = thnahf tiền + phí vận chuyển hoặc 1 số phí khác.
                    tongTien = thanhTien;
                    string contentCustomer = System.IO.File.ReadAllText (Server.MapPath("~/Content/template_sendmail/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}",or.Code);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{SanPham}}",strSanPham);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", WebShoppingOnline.Common.Common.FormatNumber(thanhTien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", WebShoppingOnline.Common.Common.FormatNumber(tongTien, 0));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", or.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{SoDienThoai}}", or.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", or.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", or.Address);
                    WebShoppingOnline.Common.Common.SendMail("CDK_Shop", "Đơn hàng #" + or.Code, contentCustomer.ToString(), or.Email);


                    // nội dung thư gửi về cho người quản trị
                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/template_sendmail/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", or.Code);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", WebShoppingOnline.Common.Common.FormatNumber(thanhTien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", WebShoppingOnline.Common.Common.FormatNumber(tongTien, 0));
                    contentAdmin = contentAdmin.Replace("{{AdminEmail}}", ConfigurationManager.AppSettings["EmailAdmin"].ToString());
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", or.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{SoDienThoai}}", or.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", or.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", or.Address);
                    WebShoppingOnline.Common.Common.SendMail("CDK_Shop","Đơn hàng mới #"+or.Code,contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    cart.ClearCart();
                    code = new { Success = true, Code = 1 };
                    //return RedirectToAction("SuccessOrder");
                }

            }
               return Json(code);
        }     


        // tách ra thành một PartialView xử lí phần tăng giảm số lượng trong giỏ hàng
        public ActionResult Partial_Item_Cart()
        {
            // lấy ra các sản phẩm trong Cart để hiển thị
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }

        public ActionResult Partial_Item_ThanhToan()
        {
            // lấy ra các sản phẩm trong Cart để hiển thị
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                return Json(new {Count = cart.Items.Count},JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {Count =0,JsonRequestBehavior.AllowGet});
            }
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            // trong trường hợp add thất bại
            var code = new { Success = false, msg = "", code = -1,Count =0 };
            var itemCheck  = db.Products.FirstOrDefault(p => p.Id == id);    
            if(itemCheck != null) {
                ShoppingCart cart =(ShoppingCart) Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem cartItem = new ShoppingCartItem
                {
                    ProductId = itemCheck.Id,
                    ProductName = itemCheck.Title,
                    CategoryName = itemCheck.ProductCategory.Title,
                    Alias = itemCheck.Alias,
                    Quantity = quantity
                };
                // kiểm tra ảnh
                if (itemCheck.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    cartItem.ProductImage = itemCheck.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                // kiếm tra số lượng
                if (itemCheck.PriceSale > 0)
                {
                    cartItem.Price = (decimal)itemCheck.PriceSale;
                }
                else
                {
                    cartItem.Price = (decimal)itemCheck.Price;
                }
                cartItem.PriceTotal = cartItem.Quantity * cartItem.Price;
                cart.AddToCart(cartItem, quantity);
                code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công", code = 1,Count = cart.Items.Count };
                Session["Cart"] = cart;
            }
            return Json(code);
        }
        [HttpPost]        
        
        public ActionResult DeleteItem (int id)
        {
            var code = new { Success = false, msg = "", code = -1 ,Count =0};
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x=>x.ProductId==id);
                if (checkProduct != null) {
                    cart.Items.Remove(checkProduct);
                    code = new { Success = true, msg = "Xóa thành công khỏi giỏ hàng", code = 1 ,Count = cart.Items.Count};
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult DeleteAllItem()
        {
            var code = new { Success = false};
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                code = new { Success = true };
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult UpdateQuantityOfItem(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new {Success =true});
            }
            return Json(new { Success = false });
        }


    }
}