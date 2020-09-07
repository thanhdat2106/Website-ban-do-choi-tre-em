using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        KetNoi_DataContext data = new KetNoi_DataContext();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if (lstGioHang == null)
            { 
                lstGioHang = new List<GioHang>();
                Session["Giohang"]=lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(string ma, string strURL)
        {
            List<GioHang> lstGiohang = LayGioHang();
            GioHang sanpham = lstGiohang.Find(n => n.MASP == ma);
            if (Session["Taikhoan"] != null)
            {
                if (sanpham == null)
                {
                    sanpham = new GioHang(ma);
                    lstGiohang.Add(sanpham);
                    return Redirect(strURL);
                }
                else
                {
                    sanpham.SOLUONG++;
                    return Redirect(strURL);
                }
            }
            else
                return RedirectToAction("DangNhap","DangKy_DangNhap");
        }

        private int TongSoLuong()
        {
            int TongSL = 0;
            List<GioHang> lstGiohang= Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                TongSL = lstGiohang.Sum(n => n.SOLUONG);
            }
            return TongSL;
        }

        private double TongTien()
        {
            double Tongtien = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                Tongtien = lstGiohang.Sum(n => n.ThanhTien);
            }
            return Tongtien;
        }

        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = LayGioHang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Home","Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult XoaGiohang(string masp)
        {
            List<GioHang> lstGiohang = LayGioHang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.MASP == masp);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.MASP == masp);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Home", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            return PartialView();
        }
        public ActionResult Dathang()
        {
            List<GioHang> lstGiohang = LayGioHang();
            List<Object> lst = new List<Object>();
            var kh = data.KHACHHANGs.Where(k => k.MATK.Contains(Session["matk"].ToString()));
            lst.Add(kh);
            lst.Add(lstGiohang);
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lst);
        }
        public ActionResult Dathang1(HOADON hd)
        {
            List<CHITIETHD> lstct = new List<CHITIETHD>();
            List<GioHang> lstGiohang = LayGioHang();
            int slhd = data.HOADONs.Count()+1;
            hd.MAHD = "HD" + slhd;
            hd.MAKH = Session["makh"].ToString();
            hd.NGAYBAN = DateTime.Now;
            hd.TIENBAN = float.Parse(TongTien().ToString());
            data.HOADONs.InsertOnSubmit(hd);
            data.SubmitChanges();
            foreach (var item in lstGiohang)
            {
                CHITIETHD cthd = new CHITIETHD();
                cthd.MAHD = "HD" + slhd ;
                cthd.MASP = item.MASP;
                cthd.SOLUONG = item.SOLUONG;
                cthd.DONGIA = float.Parse(item.DONGIA.ToString());
                cthd.THANHTIEN = float.Parse(item.ThanhTien.ToString());
                lstct.Add(cthd);
                
            }
            data.CHITIETHDs.InsertAllOnSubmit(lstct);
            data.SubmitChanges();
            return RedirectToAction("Home", "Home");
        }
    }
}
