using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class DangKy_DangNhapController : Controller
    {
        //
        // GET: /DangKy_DangNhap/
        KetNoi_DataContext data = new KetNoi_DataContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy( FormCollection col, TAIKHOAN tk, KHACHHANG kh)
        {
            
               int dem = data.TAIKHOANs.Count() ;
               var Matk =dem.ToString();
               var Tendangnhap = col["TENTK"];
               var Matkhau = col["MATKHAU"];
               var Nhaplaimatkhau = col["Nhaplaimatkhau"];
               var Tenkh = col["Tenkhachhang"];
               var Diachi = col["DiaChi"];
               var SDT = col["Sodienthoai"];
                var Gioitinh = col["Nam"].ToString();
                var taikhoan = from taikhoan1 in data.TAIKHOANs where taikhoan1.TENTK.Equals(Tendangnhap)==true select taikhoan1.TENTK;
                if (string.IsNullOrEmpty(Tendangnhap))
                {
                    ViewData["Loi1"] = "Tên đăng nhập không được để trống";
                }
                else if (string.IsNullOrEmpty(Matkhau))
                    ViewData["Loi2"] = "Mật khẩu không được để trống";
                else if (Nhaplaimatkhau.Equals(Matkhau)==false)
                    ViewData["Loi3"] = "Nhập lại mật khẩu không được để trống";
                else if (string.IsNullOrEmpty(Tenkh))
                    ViewData["Loi4"] = "Tên khách hàng không được để trống";
                else if (taikhoan!= null)
                    ViewData["Loi5"] = "Tài khoản đã trùng";
                else
                {
                    tk.TENTK = Tendangnhap;
                    tk.MATK = Matkhau;
                    tk.MATK = Matk;
                    kh.MAKH = "KH"+dem.ToString();
                    kh.TENKH = Tenkh;
                    kh.DIACHI = Diachi;
                    kh.MATK = Matk;
                    kh.SDT = SDT;
                    kh.GIOITINH = Gioitinh;
                    data.TAIKHOANs.InsertOnSubmit(tk);
                    data.KHACHHANGs.InsertOnSubmit(kh);
                    data.SubmitChanges();
                    return RedirectToAction("Home", "Home");
                }
                return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendn = collection["TENTK"];
            var matkhau = collection["MATKHAU"];

            if (String.IsNullOrEmpty(tendn))
            {
                ViewBag.Loi1 = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewBag.Loi2 = "Phải nhập mật khẩu";
            }
            else
            {
                TAIKHOAN kh = data.TAIKHOANs.SingleOrDefault(n => n.TENTK.Equals(tendn) && n.MATKHAU.Equals(matkhau));
                PHANQUYEN ad = data.PHANQUYENs.SingleOrDefault(n => n.TENTKAD.Equals(tendn) && n.MATKHAUAD.Equals(matkhau));
                if (kh != null || ad != null)
                {
                    if (kh != null && ad == null)
                    {
                        ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                        Session["Taikhoan"] = kh;
                        KHACHHANG username =   data.KHACHHANGs.SingleOrDefault(n=> n.MATK.Equals(kh.MATK));
                        Session["Tentk"] = username.TENKH;
                        Session["matk"] = kh.MATK;
                        Session["makh"] = username.MAKH;
                        return RedirectToAction("Home", "Home");
                        
                    }
                    if (kh == null && ad != null)
                    {
                        ViewBag.Thongbao = "Đăng nhập thành công";
                        Session["Admin"] = ad;
                        Session["quyen"] = ad.TENQ;
                        //return RedirectToAction("Home", "Home");
                        return RedirectToAction("Homequanly", "Admin");
                    }
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc tài khoản không đúng";
            }

            return View();
        }
        public ActionResult DangXuat()
        {
            if (Session["Taikhoan"] != null)
            {
                Session["Taikhoan"] = null;
                Session["Tentk"] = null;
                Session["Giohang"] = null;
                return RedirectToAction("Home", "Home");
            }
            else if (Session["Admin"] != null)
            {
                Session["Admin"] = null;
                return RedirectToAction("Home", "Home");
            }
            else
            {
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            }
        }
        public PartialViewResult TaiKhoan()
        {
            if (Session["Tenkh"] == null)
                Session["Tenkh"] = " quý khách";
            return PartialView();
        }

    }
}
