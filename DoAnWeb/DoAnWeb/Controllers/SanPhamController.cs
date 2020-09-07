using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/
        KetNoi_DataContext data = new KetNoi_DataContext();
        public ActionResult SanPhamTH(string id)
        {
            var all_sp = from sp in data.SANPHAMs where sp.MATH==id || sp.MATUOI==id || sp.MALOAI==id || sp.MACL==id select sp;
            return View(all_sp);
        }
        
        public ActionResult CTSanPham(string id)
        {
            var all_sp = from sp in data.SANPHAMs
                         where sp.MASP == id
                         select sp;
            return View(all_sp);
        }
        public ActionResult TimKiem( FormCollection col)
        {
            var All_sp = data.SANPHAMs.Where(s => s.TENSP.Contains(col["search"].ToString()));
            if (!String.IsNullOrEmpty(col["search"].ToString()))
            {
                All_sp = data.SANPHAMs.Where(s => s.TENSP.Contains(col["search"].ToString()));
            }
            return View(All_sp);
        }
        

    }
}
