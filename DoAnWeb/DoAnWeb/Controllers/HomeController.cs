using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using PagedList;
using PagedList.Mvc;
namespace DoAnWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        KetNoi_DataContext data = new KetNoi_DataContext();
        
        public ActionResult Home()
        { 
            var sanphamloai = data.SANPHAMs.Where(sp => sp.MALOAI.Contains("L2")).Take(5);
            var sanphamloai1 = data.SANPHAMs.Where(sp => sp.MALOAI.Contains("L1")).Take(5);
            var sanphamchatlieu = data.SANPHAMs.Where(sp => sp.MACL.Contains("CL1")).Take(5);
            var sanphamchatlieu1 = data.SANPHAMs.Where(sp => sp.MACL.Contains("CL2")).Take(5);
            var sanphamthuonghieu = data.SANPHAMs.Where(sp => sp.MATH.Contains("TH1")).Take(5);
            var sanphamthuonghieu1 = data.SANPHAMs.Where(sp => sp.MATH.Contains("TH2")).Take(5);
            var sanphamdotuoi = data.SANPHAMs.Where(sp => sp.MATUOI.Contains("T3")).Take(5);
            var sanphamdotuoi1 = data.SANPHAMs.Where(sp => sp.MATUOI.Contains("T4")).Take(5);
            List<Object> ld = new List<Object>();
            ld.Add(sanphamloai);
            ld.Add(sanphamloai1);
            ld.Add(sanphamchatlieu);
            ld.Add(sanphamchatlieu1);
            ld.Add(sanphamthuonghieu);
            ld.Add(sanphamthuonghieu1);
            ld.Add(sanphamdotuoi);
            ld.Add(sanphamdotuoi1);
            return View(ld);
        }
        public PartialViewResult DanhMucLoai()
        {
            var loai_sp = from loai in data.LOAIs select loai;
            return PartialView(loai_sp);
        }
        public PartialViewResult SliderSanPham()
        {
            var All_sp = from sp in data.SANPHAMs where  sp.MASP == "SP51" || sp.MASP == "SP56" select sp;
            return PartialView(All_sp);
        }
        public PartialViewResult SliderThuongHieu()
        {
            var thuonghieu1 = from th in data.THUONGHIEUs where th.MATH == "TH1" || th.MATH == "TH2" || th.MATH == "TH3" || th.MATH == "TH4" || th.MATH == "TH5" select th;
            var thuonghieu2 = from th in data.THUONGHIEUs where th.MATH == "TH6" || th.MATH == "TH7" || th.MATH == "TH8" || th.MATH == "TH9" || th.MATH == "TH10" select th;
            var thuonghieu3 = from th in data.THUONGHIEUs where th.MATH == "TH11" || th.MATH == "TH12" || th.MATH == "TH13" || th.MATH == "TH14" || th.MATH == "TH15" select th;
            var thuonghieu4 = from th in data.THUONGHIEUs where th.MATH == "TH16" || th.MATH == "TH17" || th.MATH == "TH18" || th.MATH == "TH19" || th.MATH == "TH20" select th;
            var thuonghieu5 = from th in data.THUONGHIEUs where th.MATH == "TH21" || th.MATH == "TH22" || th.MATH == "TH23" || th.MATH == "TH24" || th.MATH == "TH25" select th;
            var thuonghieu6 = from th in data.THUONGHIEUs where th.MATH == "TH26" || th.MATH == "TH27" || th.MATH == "TH28" || th.MATH == "TH29" || th.MATH == "TH30" select th;
            List<Object> ld = new List<Object>();
            ld.Add(thuonghieu1);
            ld.Add(thuonghieu2);
            ld.Add(thuonghieu3);
            ld.Add(thuonghieu4);
            ld.Add(thuonghieu5);
            ld.Add(thuonghieu6);
            return PartialView(ld);
        }

        public ActionResult DanhMucTuoi()
        {
            var tuoi = from t in data.TUOIs select t;
            return PartialView(tuoi);
        }

        public ActionResult GioiThieu()
        {
            var ttgt = from t in data.GioiThieus select t;
            return View(ttgt);
        }
        DatModelContainer db = new DatModelContainer();
        public ActionResult TinTuc()
        {
            List<Object> list = new List<Object>();
            var tinchinh = from tt in db.TINTUCs where tt.VT == 1 select tt;
            var tinphu1 = from tt in db.TINTUCs where tt.VT == 2 select tt;
            var tin1 = from tt in db.TINTUCs where tt.VT == 3 select tt;
            var tin2 = from tt in db.TINTUCs where tt.VT == 4 select tt;
            var tin3 = from tt in db.TINTUCs where tt.VT == 5 select tt;
            list.Add(tinchinh);
            list.Add(tinphu1);
            list.Add(tin1);
            list.Add(tin2);
            list.Add(tin3);
            return View(list);
        }
        [HttpGet]
        public ActionResult CTTinTuc(string ma)
        {
            var tt = db.TINTUCs.SingleOrDefault(t=>t.MATC==int.Parse(ma));
                return View(tt);
        }
    }
}
