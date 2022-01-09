using DownyShoes.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DownyShoes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Shop(int? id,int? page)
        {
            DBContext db = new DBContext();
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in db.GIAYs
                         select l).OrderBy(x => x.ID);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 6;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);
            
            string name=null;
            try
            {
                name = Request.Form["search"];
            }catch(Exception ex)
            {

            }
            if (!string.IsNullOrEmpty(name))
            {
                var model = links.Where(x => x.NAME.Contains(name)).ToPagedList(pageNumber, pageSize);
                name = null;
                return View(model);
            }
            if (id >0)
            {
                return View(links.Where(x => x.IDLOAIGIAY == id).ToPagedList(pageNumber, pageSize));
            }
            return View(links.ToPagedList(pageNumber, pageSize));




        }
        public ActionResult SingleProduct(int? id)
        {
            GIAY gIAY = db.GIAYs.Where(x => x.ID == id).FirstOrDefault();
            return View(gIAY);
        }
        public ActionResult Checkout()
        {
            List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)Session["GioHang"];
            return View(shoppingCarts);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        DBContext db = new DBContext();
        public ActionResult TangSoLuong(int id)
        {
            List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)Session["GioHang"];
            int index = isExist(id);
            shoppingCarts[index].Soluong++;
            Session["GioHang"] = shoppingCarts;
            return View("Checkout", shoppingCarts);
        }
        public ActionResult GiamSoLuong(int id)
        {
            List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)Session["GioHang"];
            int index = isExist(id);
            shoppingCarts[index].Soluong--;
            Session["GioHang"] = shoppingCarts;
            return View("Checkout", shoppingCarts);
        }
        public ActionResult XoaCart(int id)
        {
            List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)Session["GioHang"];
            int index = isExist(id);
            shoppingCarts.RemoveAt(index);
            Session["GioHang"] = shoppingCarts;
            return View("Checkout", shoppingCarts);
        }
        public string AddCart(int IDSP)
        {
            GIAY giay = db.GIAYs.Find(IDSP);
            if (Session["GioHang"] == null)
            {
                List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
                ShoppingCart shoppingCart = new ShoppingCart();
                shoppingCart.Id = giay.ID;
                shoppingCart.Tensanpham = giay.NAME;
                shoppingCart.Hinhanh = giay.IMAGE;
                shoppingCart.Gia = giay.COST.Value;
                shoppingCart.Soluong = 1;
                shoppingCarts.Add(shoppingCart);
                Session["GioHang"] = shoppingCarts;
            }
            else
            {
                List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)Session["GioHang"];
                int index = isExist(IDSP);
                if (index != -1)
                {
                    shoppingCarts[index].Soluong++;
                }
                else
                {
                    ShoppingCart shoppingCart = new ShoppingCart();
                    shoppingCart.Id = giay.ID;
                    shoppingCart.Tensanpham = giay.NAME;
                    shoppingCart.Hinhanh = giay.IMAGE;
                    shoppingCart.Gia = giay.COST.Value;
                    shoppingCart.Soluong = 1;
                    shoppingCarts.Add(shoppingCart);
                }
                Session["GioHang"] = shoppingCarts;
            }
            return "Thanh cong";
        }

        public int isExist(int id)
        {
            List<ShoppingCart> shoppingCarts = (List<ShoppingCart>)Session["GioHang"];
            for (int i = 0; i < shoppingCarts.Count; i++)
            {
                if (shoppingCarts[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}