using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Reflection;
namespace DoAnWeb.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        KetNoi_DataContext data = new KetNoi_DataContext();
        public ActionResult Homequanly()
        {
            Double thang1 = 0;
            Double thang2 = 0;
            Double thang3 = 0;
            Double thang4 = 0;
            Double thang5 = 0;
            Double thang6 = 0;
            Double thang7 = 0;
            Double thang8 = 0;
            Double thang9 = 0;
            Double thang10 = 0;
            Double thang11 = 0;
            Double thang12 = 0;

            if (Session["Admin"] == null)
                    return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else 
            {
                var t1 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 1 select t;
                foreach (var tt in t1)
                {
                    thang1 = thang1 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang1 = thang1;
                var t2 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 2 select t;
                foreach (var tt in t2)
                {
                    thang2 = thang2 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang2 = thang2;
                var t3 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 3 select t;
                foreach (var tt in t3)
                {
                    thang3 = thang3 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang3 = thang3;
                var t4 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 4 select t;
                foreach (var tt in t4)
                {
                    thang4 = thang4 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang4 = thang4;
                var t5 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 5 select t;
                foreach (var tt in t5)
                {
                    thang5 = thang5 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang5 = thang5;
                var t6 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 6 select t;
                foreach (var tt in t6)
                {
                    thang6 = thang6 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang6 = thang6;
                var t7 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 7 select t;
                foreach (var tt in t7)
                {
                    thang7 = thang7 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang7 = thang7;
                var t8 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 8 select t;
                foreach (var tt in t8)
                {
                    thang8 = thang8 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang8 = thang8;
                var t9 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 9 select t;
                foreach (var tt in t9)
                {
                    thang9 = thang9 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang9 = thang9;
                var t10 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 10 select t;
                foreach (var tt in t10)
                {
                    thang10 = thang10 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang10 = thang10;
                var t11 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 11 select t;
                foreach (var tt in t11)
                {
                    thang11 = thang11 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang11 = thang11;
                var t12 = from t in data.HOADONs where Convert.ToDateTime(t.NGAYBAN).Month == 12 select t;
                foreach (var tt in t12)
                {
                    thang12 = thang12 + double.Parse(tt.THANHTIEN.ToString());
                }
                ViewBag.thang12 = thang12;
                return View();
            }
            
        }
        public ActionResult SoNV()
        {
            ViewBag.SoNV = data.ADMINs.Count();
            return PartialView();
        }
        public ActionResult SoKH()
        {
            ViewBag.SoKH = data.KHACHHANGs.Count();
            return PartialView();
        }
        public ActionResult SoHD()
        {
            int sohd=0;
            DateTime now = DateTime.Now;
            var hoadon = from hd in data.HOADONs where hd.NGAYBAN == now select hd;
            foreach (var item in hoadon)
            {
                sohd = sohd + 1;
            }
            ViewBag.SoHD = sohd;
            return PartialView();
        }
        public ActionResult TT()
        {
            double tongtien = 0;

            DateTime now = DateTime.Now;
            var tt = from s in data.HOADONs where Convert.ToDateTime(s.NGAYBAN).Month == now.Month select s;
            foreach (var item in tt)
            {
                tongtien = tongtien + double.Parse(item.THANHTIEN.ToString());
            }
            ViewBag.TT = tongtien;
            return PartialView();
        }
        //public List<SANPHAM> Danhsachsp()
        //{

        //    return data.SANPHAMs.OrderByDescending(a => a.TENSP).ToList();
        //}
        //public ActionResult Index(int ? page)
        //{
        //    int pageSize = 10;
        //    int pageNum = (page ?? 1);
        //    var sach = Danhsachsp();
        //    return View(sach.ToPagedList(pageNum,pageSize));
        //}
        public ActionResult Index(string sortProperty, string sortOrder, string searchString, int? size, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                if (sortOrder == "asc") ViewBag.SortOrder = "desc";
                if (sortOrder == "desc") ViewBag.SortOrder = "";
                if (sortOrder == "") ViewBag.SortOrder = "asc";

                ViewBag.searchValue = searchString;
                ViewBag.sortProperty = sortProperty;
                ViewBag.page = page;

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "10", Value = "10" });
                items.Add(new SelectListItem { Text = "20", Value = "20" });
                items.Add(new SelectListItem { Text = "25", Value = "25" });
                items.Add(new SelectListItem { Text = "50", Value = "50" });
                items.Add(new SelectListItem { Text = "100", Value = "100" });
                items.Add(new SelectListItem { Text = "200", Value = "200" });

                foreach (var item in items)
                {
                    if (item.Selected.ToString().Equals(size.ToString()) == true) item.Selected = true;
                }
                ViewBag.size = items;
                ViewBag.currentSize = size;

                var properties = typeof(SANPHAM).GetProperties();

                List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
                foreach (var item in properties)
                {
                    int order = 999;
                    var isVirtual = item.GetAccessors()[0].IsVirtual;
                    if (item.Name == "MASP") order = 1;
                    if (item.Name == "TENSP") order = 2;
                    if (item.Name == "HINHANH") order = 3;
                    if (item.Name == "DONGIA") order = 4;
                    if (item.Name == "LOAI") continue;
                    if (item.Name == "THUONGHIEU") continue;
                    if (item.Name == "CHATLIEU") continue;
                    if (item.Name == "CHITIETHDs") continue;
                    if (item.Name == "TUOI") continue;
                    Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
                    list.Add(t);
                }
                list = list.OrderBy(x => x.Item3).ToList();

                foreach (var item in list)
                {
                    // 2.2. Thuộc tính bình thường thì cho phép sắp xếp
                    if (!item.Item2) // Item2 dùng để kiểm tra thuộc tính ảo hay không?
                    {
                        // 2.3. So thuộc tính sortProperty và sortOrder để biết thuộc tính nào cần thay biểu tượng sắp giảm
                        if (sortOrder == "desc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-desc'></i></th></a></th>";
                        }
                        else if (sortOrder == "asc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-asc'></a></th>";
                        }
                        else
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                               ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort'></a></th>";
                        }

                    }
                    // 2.4. Thuộc tính virtual (public virtual Category Category...) thì không sắp xếp được
                    // cho nên không cần tạo liên kết
                    else ViewBag.Headings += "<th>" + item.Item1 + "</th>";
                }

                var sp = from l in data.SANPHAMs select l;

                // 4. Tạo thuộc tính sắp xếp mặc định là "LinkID"
                if (String.IsNullOrEmpty(sortProperty)) sortProperty = "MASP";

                // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
                if (sortOrder == "desc") sp = sp.OrderBy(sortProperty + " desc");
                else if (sortOrder == "asc") sp = sp.OrderBy(sortProperty);
                else sp = sp.OrderBy("MASP");

                // 5.1. Thêm phần tìm kiếm
                if (!String.IsNullOrEmpty(searchString))
                {
                    sp = sp.Where(s => s.TENSP.Contains(searchString));
                }
                page = page ?? 1;
                int pageSize = (size ?? 10);
                ViewBag.pageSize = pageSize;
                int pageNumber = (page ?? 1);
                int checkTotal = (int)(sp.ToList().Count / pageSize) + 1;
                if (pageNumber > checkTotal) pageNumber = checkTotal;
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult ChatLieu(string sortProperty, string sortOrder, string searchString, int? size, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else 
            {
                if (sortOrder == "asc") ViewBag.SortOrder = "desc";
                if (sortOrder == "desc") ViewBag.SortOrder = "";
                if (sortOrder == "") ViewBag.SortOrder = "asc";

                ViewBag.searchValue = searchString;
                ViewBag.sortProperty = sortProperty;
                ViewBag.page = page;

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "10", Value = "10" });
                items.Add(new SelectListItem { Text = "20", Value = "20" });
                items.Add(new SelectListItem { Text = "25", Value = "25" });
                items.Add(new SelectListItem { Text = "50", Value = "50" });
                items.Add(new SelectListItem { Text = "100", Value = "100" });
                items.Add(new SelectListItem { Text = "200", Value = "200" });

                foreach (var item in items)
                {
                    if (item.Selected.ToString().Equals(size.ToString()) == true) item.Selected = true;
                }
                ViewBag.size = items;
                ViewBag.currentSize = size;

                var properties = typeof(CHATLIEU).GetProperties();

                List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
                foreach (var item in properties)
                {
                    int order = 999;
                    var isVirtual = item.GetAccessors()[0].IsVirtual;
                    if (item.Name == "MACL") order = 1;
                    if (item.Name == "TENCL") order = 2;
                    if (item.Name == "SANPHAMs") continue;
                    Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
                    list.Add(t);
                }
                list = list.OrderBy(x => x.Item3).ToList();

                foreach (var item in list)
                {
                    // 2.2. Thuộc tính bình thường thì cho phép sắp xếp
                    if (!item.Item2) // Item2 dùng để kiểm tra thuộc tính ảo hay không?
                    {
                        // 2.3. So thuộc tính sortProperty và sortOrder để biết thuộc tính nào cần thay biểu tượng sắp giảm
                        if (sortOrder == "desc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-desc'></i></th></a></th>";
                        }
                        else if (sortOrder == "asc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-asc'></a></th>";
                        }
                        else
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                               ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort'></a></th>";
                        }

                    }
                    // 2.4. Thuộc tính virtual (public virtual Category Category...) thì không sắp xếp được
                    // cho nên không cần tạo liên kết
                    else ViewBag.Headings += "<th>" + item.Item1 + "</th>";
                }

                var sp = from l in data.CHATLIEUs select l;

                // 4. Tạo thuộc tính sắp xếp mặc định là "LinkID"
                if (String.IsNullOrEmpty(sortProperty)) sortProperty = "MACL";

                // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
                if (sortOrder == "desc") sp = sp.OrderBy(sortProperty + " desc");
                else if (sortOrder == "asc") sp = sp.OrderBy(sortProperty);
                else sp = sp.OrderBy("MACL");

                // 5.1. Thêm phần tìm kiếm
                if (!String.IsNullOrEmpty(searchString))
                {
                    sp = sp.Where(s => s.TENCL.Contains(searchString));
                }
                page = page ?? 1;
                int pageSize = (size ?? 10);
                ViewBag.pageSize = pageSize;
                int pageNumber = (page ?? 1);
                int checkTotal = (int)(sp.ToList().Count / pageSize) + 1;
                if (pageNumber > checkTotal) pageNumber = checkTotal;
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpGet]
        public ActionResult Delete(string masp)
        {            
                var all_sp = data.SANPHAMs.SingleOrDefault(s => s.MASP == masp);
                if (all_sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(all_sp);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleted(string masp)
        {
            try
            {
                if (Session["Admin"] == null)
                    return RedirectToAction("DangNhap", "DangKy_DangNhap");
                else if (Session["quyen"].ToString().Equals("Admin") == true)
                {
                    var sp = data.SANPHAMs.SingleOrDefault(s => s.MASP == masp);
                    data.SANPHAMs.DeleteOnSubmit(sp);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Homequanly", "Admin");
                }
            }
            catch { }
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(string masp)
        {
            var all_sp = data.SANPHAMs.SingleOrDefault(s => s.MASP == masp);

                if (Session["Admin"] == null)
                    return RedirectToAction("DangNhap", "DangKy_DangNhap");
                else
                {
                   
                    ViewBag.MALOAI = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI", all_sp.MALOAI);
                    ViewBag.MACL = new SelectList(data.CHATLIEUs.ToList().OrderBy(n => n.TENCL), "MACL", "TENCL", all_sp.MACL);
                    ViewBag.MATH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH", all_sp.MATH);
                    ViewBag.MATUOI = new SelectList(data.TUOIs.ToList().OrderBy(n => n.DOTUOI), "MATUOI", "DOTUOI", all_sp.MATUOI);
                    if (all_sp == null)
                    {
                        Response.StatusCode = 404;
                        return null;
                    }
                    return View(all_sp);
                }
            
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult updateEdit( SANPHAM sp, HttpPostedFileBase fileupload)
        {
                ViewBag.MALOAI = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MACL = new SelectList(data.CHATLIEUs.ToList().OrderBy(n => n.TENCL), "MACL", "TENCL");
                ViewBag.MATH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
                ViewBag.MATUOI = new SelectList(data.TUOIs.ToList().OrderBy(n => n.DOTUOI), "MATUOI", "DOTUOI");
                if (fileupload == null)
                {
                    ViewBag.ThongBao = "Vui lòng chọn ảnh";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var filename = Path.GetFileName(fileupload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                        if (System.IO.File.Exists(path))
                        {
                            fileupload.SaveAs(path);
                        }
                        sp.HINHANH = filename;
                        UpdateModel(sp);
                        data.SubmitChanges();
                    }
                    return RedirectToAction("Index");
                }
            


        }
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                ViewBag.MALOAI = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MACL = new SelectList(data.CHATLIEUs.ToList().OrderBy(n => n.TENCL), "MACL", "TENCL");
                ViewBag.MATH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
                ViewBag.MATUOI = new SelectList(data.TUOIs.ToList().OrderBy(n => n.DOTUOI), "MATUOI", "DOTUOI");
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp, HttpPostedFileBase file, FormCollection col)
        {
            try
            {
                if (Session["Admin"] == null)
                    return RedirectToAction("DangNhap", "DangKy_DangNhap");
                else
                {
                    ViewBag.MALOAI = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                    ViewBag.MACL = new SelectList(data.CHATLIEUs.ToList().OrderBy(n => n.TENCL), "MACL", "TENCL");
                    ViewBag.MATH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
                    ViewBag.MATUOI = new SelectList(data.TUOIs.ToList().OrderBy(n => n.DOTUOI), "MATUOI", "DOTUOI");
                    if (col["picture"] == null)
                    {
                        ViewBag.ThongBao = "Vui lòng chọn ảnh";
                        return View();
                    }
                    else
                    {
                        var kt = data.SANPHAMs.SingleOrDefault(s => s.MASP == col["MASP"]);
                        if (kt == null)
                        {
                            string[] a = col["picture"].Split('/');
                            sp.MASP = col["MASP"];
                            sp.TENSP = col["TENSP"];
                            sp.DONGIA = float.Parse(col["DONGIA"].ToString());
                            sp.CHITIET = col["CHITIET"];
                            sp.HINHANH = a[3];
                            sp.MALOAI = col["MALOAI"];
                            sp.MACL = col["MACL"];
                            sp.MATH = col["MATH"];
                            sp.MATUOI = col["MATUOI"];
                            data.SANPHAMs.InsertOnSubmit(sp);
                            data.SubmitChanges();
                            return RedirectToAction("Index", "Admin");
                        }
                        return View();
                    }
                }
            }
            catch { }
            return View();
        }
        public ActionResult LoaiSanPham(string sortProperty, string sortOrder, string searchString, int? size, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                if (sortOrder == "asc") ViewBag.SortOrder = "desc";
                if (sortOrder == "desc") ViewBag.SortOrder = "";
                if (sortOrder == "") ViewBag.SortOrder = "asc";

                ViewBag.searchValue = searchString;
                ViewBag.sortProperty = sortProperty;
                ViewBag.page = page;

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "10", Value = "10" });
                items.Add(new SelectListItem { Text = "20", Value = "20" });
                items.Add(new SelectListItem { Text = "25", Value = "25" });
                items.Add(new SelectListItem { Text = "50", Value = "50" });
                items.Add(new SelectListItem { Text = "100", Value = "100" });
                items.Add(new SelectListItem { Text = "200", Value = "200" });

                foreach (var item in items)
                {
                    if (item.Selected.ToString().Equals(size.ToString()) == true) item.Selected = true;
                }
                ViewBag.size = items;
                ViewBag.currentSize = size;

                var properties = typeof(LOAI).GetProperties();

                List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
                foreach (var item in properties)
                {
                    int order = 999;
                    var isVirtual = item.GetAccessors()[0].IsVirtual;
                    if (item.Name == "MALOAI") order = 1;
                    if (item.Name == "TENLOAI") order = 2;
                    if (item.Name == "SANPHAMs") continue;
                    Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
                    list.Add(t);
                }
                list = list.OrderBy(x => x.Item3).ToList();

                foreach (var item in list)
                {
                    // 2.2. Thuộc tính bình thường thì cho phép sắp xếp
                    if (!item.Item2) // Item2 dùng để kiểm tra thuộc tính ảo hay không?
                    {
                        // 2.3. So thuộc tính sortProperty và sortOrder để biết thuộc tính nào cần thay biểu tượng sắp giảm
                        if (sortOrder == "desc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-desc'></i></th></a></th>";
                        }
                        else if (sortOrder == "asc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-asc'></a></th>";
                        }
                        else
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                               ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort'></a></th>";
                        }

                    }
                    // 2.4. Thuộc tính virtual (public virtual Category Category...) thì không sắp xếp được
                    // cho nên không cần tạo liên kết
                    else ViewBag.Headings += "<th>" + item.Item1 + "</th>";
                }

                var sp = from l in data.LOAIs select l;

                // 4. Tạo thuộc tính sắp xếp mặc định là "LinkID"
                if (String.IsNullOrEmpty(sortProperty)) sortProperty = "MALOAI";

                // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
                if (sortOrder == "desc") sp = sp.OrderBy(sortProperty + " desc");
                else if (sortOrder == "asc") sp = sp.OrderBy(sortProperty);
                else sp = sp.OrderBy("MALOAI");

                // 5.1. Thêm phần tìm kiếm
                if (!String.IsNullOrEmpty(searchString))
                {
                    sp = sp.Where(s => s.TENLOAI.Contains(searchString));
                }
                page = page ?? 1;
                int pageSize = (size ?? 10);
                ViewBag.pageSize = pageSize;
                int pageNumber = (page ?? 1);
                int checkTotal = (int)(sp.ToList().Count / pageSize) + 1;
                if (pageNumber > checkTotal) pageNumber = checkTotal;
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult ThuongHieu(string sortProperty, string sortOrder, string searchString, int? size, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                if (sortOrder == "asc") ViewBag.SortOrder = "desc";
                if (sortOrder == "desc") ViewBag.SortOrder = "";
                if (sortOrder == "") ViewBag.SortOrder = "asc";

                ViewBag.searchValue = searchString;
                ViewBag.sortProperty = sortProperty;
                ViewBag.page = page;

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "10", Value = "10" });
                items.Add(new SelectListItem { Text = "20", Value = "20" });
                items.Add(new SelectListItem { Text = "25", Value = "25" });
                items.Add(new SelectListItem { Text = "50", Value = "50" });
                items.Add(new SelectListItem { Text = "100", Value = "100" });
                items.Add(new SelectListItem { Text = "200", Value = "200" });

                foreach (var item in items)
                {
                    if (item.Selected.ToString().Equals(size.ToString()) == true) item.Selected = true;
                }
                ViewBag.size = items;
                ViewBag.currentSize = size;

                var properties = typeof(THUONGHIEU).GetProperties();

                List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
                foreach (var item in properties)
                {
                    int order = 999;
                    var isVirtual = item.GetAccessors()[0].IsVirtual;
                    if (item.Name == "MATH") order = 1;
                    if (item.Name == "TENTH") order = 2;
                    if (item.Name == "SANPHAMs") continue;
                    Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
                    list.Add(t);
                }
                list = list.OrderBy(x => x.Item3).ToList();

                foreach (var item in list)
                {
                    // 2.2. Thuộc tính bình thường thì cho phép sắp xếp
                    if (!item.Item2) // Item2 dùng để kiểm tra thuộc tính ảo hay không?
                    {
                        // 2.3. So thuộc tính sortProperty và sortOrder để biết thuộc tính nào cần thay biểu tượng sắp giảm
                        if (sortOrder == "desc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-desc'></i></th></a></th>";
                        }
                        else if (sortOrder == "asc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-asc'></a></th>";
                        }
                        else
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                               ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort'></a></th>";
                        }

                    }
                    // 2.4. Thuộc tính virtual (public virtual Category Category...) thì không sắp xếp được
                    // cho nên không cần tạo liên kết
                    else ViewBag.Headings += "<th>" + item.Item1 + "</th>";
                }

                var sp = from l in data.THUONGHIEUs select l;

                // 4. Tạo thuộc tính sắp xếp mặc định là "LinkID"

                // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ

                //else sp = sp.OrderBy("MATH");

                // 5.1. Thêm phần tìm kiếm
                if (!String.IsNullOrEmpty(searchString))
                {
                    sp = sp.Where(s => s.TENTH.Contains(searchString));
                }
                page = page ?? 1;
                int pageSize = (size ?? 10);
                ViewBag.pageSize = pageSize;
                int pageNumber = (page ?? 1);
                int checkTotal = (int)(sp.ToList().Count / pageSize) + 1;
                if (pageNumber > checkTotal) pageNumber = checkTotal;
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult KhachHang(string sortProperty, string sortOrder, string searchString, int? size, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                if (sortOrder == "asc") ViewBag.SortOrder = "desc";
                if (sortOrder == "desc") ViewBag.SortOrder = "";
                if (sortOrder == "") ViewBag.SortOrder = "asc";

                ViewBag.searchValue = searchString;
                ViewBag.sortProperty = sortProperty;
                ViewBag.page = page;

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "10", Value = "10" });
                items.Add(new SelectListItem { Text = "20", Value = "20" });
                items.Add(new SelectListItem { Text = "25", Value = "25" });
                items.Add(new SelectListItem { Text = "50", Value = "50" });
                items.Add(new SelectListItem { Text = "100", Value = "100" });
                items.Add(new SelectListItem { Text = "200", Value = "200" });

                foreach (var item in items)
                {
                    if (item.Selected.ToString().Equals(size.ToString()) == true) item.Selected = true;
                }
                ViewBag.size = items;
                ViewBag.currentSize = size;

                var properties = typeof(KHACHHANG).GetProperties();

                List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
                foreach (var item in properties)
                {
                    int order = 999;
                    var isVirtual = item.GetAccessors()[0].IsVirtual;
                    if (item.Name == "MAKH") order = 1;
                    if (item.Name == "TENKH") order = 2;
                    if (item.Name == "DIACHI") order = 3;
                    if (item.Name == "SDT") order = 4;
                    if (item.Name == "NAMSINH") order = 5;
                    if (item.Name == "GIOITINH") order = 6;
                    if (item.Name == "LOAIKH") order = 7;
                    if (item.Name == "MATK") order = 8;
                    if (item.Name == "HOADONs") continue;
                    if (item.Name == "TAIKHOAN") continue;
                    Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
                    list.Add(t);
                }
                list = list.OrderBy(x => x.Item3).ToList();

                foreach (var item in list)
                {
                    // 2.2. Thuộc tính bình thường thì cho phép sắp xếp
                    if (!item.Item2) // Item2 dùng để kiểm tra thuộc tính ảo hay không?
                    {
                        // 2.3. So thuộc tính sortProperty và sortOrder để biết thuộc tính nào cần thay biểu tượng sắp giảm
                        if (sortOrder == "desc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-desc'></i></th></a></th>";
                        }
                        else if (sortOrder == "asc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-asc'></a></th>";
                        }
                        else
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                               ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort'></a></th>";
                        }

                    }
                    // 2.4. Thuộc tính virtual (public virtual Category Category...) thì không sắp xếp được
                    // cho nên không cần tạo liên kết
                    else ViewBag.Headings += "<th>" + item.Item1 + "</th>";
                }

                var sp = from l in data.KHACHHANGs select l;

                if (String.IsNullOrEmpty(sortProperty)) sortProperty = "MAKH";

                // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
                if (sortOrder == "desc") sp = sp.OrderBy(sortProperty + " desc");
                else if (sortOrder == "asc") sp = sp.OrderBy(sortProperty);
                else sp = sp.OrderBy("MAKH");

                // 5.1. Thêm phần tìm kiếm
                if (!String.IsNullOrEmpty(searchString))
                {
                    sp = sp.Where(s => s.TENKH.Contains(searchString));
                }
                page = page ?? 1;
                int pageSize = (size ?? 10);
                ViewBag.pageSize = pageSize;
                int pageNumber = (page ?? 1);
                int checkTotal = (int)(sp.ToList().Count / pageSize) + 1;
                if (pageNumber > checkTotal) pageNumber = checkTotal;
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Themnhanvien()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else if (Session["quyen"].ToString().Equals("Admin") == true)
            {
                if (Session["Admin"] == null)
                    return RedirectToAction("DangNhap", "DangKy_DangNhap");
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Homequanly", "Admin");
            }
        }
        [HttpPost]
        public ActionResult Themnhanvien(FormCollection col, ADMIN ad)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else if (Session["quyen"].ToString().Equals("Admin") == true)
            {
                var ma = col["MAAD"].ToString();
                var tenad = col["TENAD"];
                var tentkad = col["TENTKAD"];
                var matkhauad = col["MATKHAUAD"];
                var gioitinh = col["Nam"].ToString();
                var sdt = col["Sodienthoai"];
                var email = col["email"];
                var kt = data.ADMINs.SingleOrDefault(s => s.MAAD == ma);
                if (kt == null)
                {
                    var kt1 = data.ADMINs.SingleOrDefault(s => s.TENTKAD == tentkad);
                    if (kt1 == null)
                    {
                        ad.MAAD = ma;
                        ad.TENAD = tenad;
                        ad.TENTKAD = tentkad;
                        ad.MATKHAUAD = matkhauad;
                        ad.GIOITINH = gioitinh;
                        ad.SDT = sdt;
                        ad.MAIL = email;
                        data.ADMINs.InsertOnSubmit(ad);
                        data.SubmitChanges();
                        return RedirectToAction("PhanQuyen", "Admin");
                    }
                    else
                    {
                        ViewData["LoiKhoaChinh"] = "Tên tài khoản bị trùng";
                        return this.Themnhanvien();
                    }
                }
                else
                {
                    ViewData["LoiKhoaChinh"] = "Mã tài khoản bị trùng";
                    return this.Themnhanvien();
                }
            }
            else
            {
                return RedirectToAction("Homequanly", "Admin");
            }
        }
        public ActionResult PhanQuyen(string maad)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else if (Session["quyen"].ToString().Equals("Admin") == true)
            {
                var ad = data.ADMINs.SingleOrDefault(t => t.MAAD == maad);
                return View(ad);
            }
            else
            {
                return RedirectToAction("Homequanly", "Admin");
            }
        }
        [HttpPost]
        public ActionResult PhanQuyen(FormCollection col, QUYEN q)
        { 
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else if (Session["quyen"].ToString().Equals("Admin") == true)
            {
                var maq = col["MAQ"];
                var quyen = col["Nam"].ToString();
                var maad = col["MAAD"];
                var kt = data.QUYENs.SingleOrDefault(s => s.MAQ == maq);
                if (kt == null)
                {
                    var kt1 = data.QUYENs.SingleOrDefault(s => s.MAAD == maad);
                    if (kt1 == null)
                    {
                        q.MAQ = maq;
                        q.TENQ = quyen;
                        q.MAAD = maad;
                        data.QUYENs.InsertOnSubmit(q);
                        data.SubmitChanges();
                        return RedirectToAction("Quanlynhanvien", "Admin");
                    }
                    else
                    {
                        ViewData["LoiKhoaChinh"] = "Tên tài khoản bị trùng";
                        return View();
                    }

                }
                else
                {
                    ViewData["LoiKhoaChinh"] = "Mã quyền bị trùng";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Homequanly", "Admin");
            }
        }
        public ActionResult Quanlynhanvien(string sortProperty, string sortOrder, string searchString, int? size, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else if (Session["quyen"].ToString().Equals("Admin")==true)
            
            {
                if (sortOrder == "asc") ViewBag.SortOrder = "desc";
                if (sortOrder == "desc") ViewBag.SortOrder = "";
                if (sortOrder == "") ViewBag.SortOrder = "asc";

                ViewBag.searchValue = searchString;
                ViewBag.sortProperty = sortProperty;
                ViewBag.page = page;

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "10", Value = "10" });
                items.Add(new SelectListItem { Text = "20", Value = "20" });
                items.Add(new SelectListItem { Text = "25", Value = "25" });
                items.Add(new SelectListItem { Text = "50", Value = "50" });
                items.Add(new SelectListItem { Text = "100", Value = "100" });
                items.Add(new SelectListItem { Text = "200", Value = "200" });

                foreach (var item in items)
                {
                    if (item.Selected.ToString().Equals(size.ToString()) == true) item.Selected = true;
                }
                ViewBag.size = items;
                ViewBag.currentSize = size;
                
                var properties = typeof(PHANQUYEN).GetProperties();
                List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
                foreach (var item in properties)
                {
                    int order = 999;
                    var isVirtual = item.GetAccessors()[0].IsVirtual;
                    if (item.Name == "MAAD") order = 1;
                    if (item.Name == "TENAD") order = 2;
                    if (item.Name == "TENTKAD") order = 3;
                    if (item.Name == "MATKHAUAD") order = 4;
                    if (item.Name == "GIOITINH") order = 5;
                    if (item.Name == "SDT") order = 6;
                    if (item.Name == "MAIL") order = 7;
                    if (item.Name == "TENQ") order = 8;
                    if (item.Name == "QUYENs") continue;
                    Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
                    list.Add(t);
                }
                list = list.OrderBy(x => x.Item3).ToList();

                foreach (var item in list)
                {
                    // 2.2. Thuộc tính bình thường thì cho phép sắp xếp
                    if (!item.Item2) // Item2 dùng để kiểm tra thuộc tính ảo hay không?
                    {
                        // 2.3. So thuộc tính sortProperty và sortOrder để biết thuộc tính nào cần thay biểu tượng sắp giảm
                        if (sortOrder == "desc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-desc'></i></th></a></th>";
                        }
                        else if (sortOrder == "asc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-asc'></a></th>";
                        }
                        else
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                               ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort'></a></th>";
                        }

                    }
                    // 2.4. Thuộc tính virtual (public virtual Category Category...) thì không sắp xếp được
                    // cho nên không cần tạo liên kết
                    else ViewBag.Headings += "<th>" + item.Item1 + "</th>";
                }

                var sp = from l in data.PHANQUYENs select l;

                if (String.IsNullOrEmpty(sortProperty)) sortProperty = "MAAD";

                // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
                if (sortOrder == "desc") sp = sp.OrderBy(sortProperty + " desc");
                else if (sortOrder == "asc") sp = sp.OrderBy(sortProperty);
                else sp = sp.OrderBy("MAAD");

                // 5.1. Thêm phần tìm kiếm
                if (!String.IsNullOrEmpty(searchString))
                {
                    sp = sp.Where(s => s.TENAD.Contains(searchString));
                }
                page = page ?? 1;
                int pageSize = (size ?? 10);
                ViewBag.pageSize = pageSize;
                int pageNumber = (page ?? 1);
                int checkTotal = (int)(sp.ToList().Count / pageSize) + 1;
                if (pageNumber > checkTotal) pageNumber = checkTotal;
                return View(sp.ToPagedList(pageNumber, pageSize));
            
             }
            else
            {
                return RedirectToAction("Homequanly", "Admin");
            }
        }
        public string ProcessCreate(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Content/HinhAnh/"+file.FileName));
            return "/Content/HinhAnh/" + file.FileName;
        }
        public ActionResult QLDonHang(string sortProperty, string sortOrder, string searchString, int? size, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                if (sortOrder == "asc") ViewBag.SortOrder = "desc";
                if (sortOrder == "desc") ViewBag.SortOrder = "";
                if (sortOrder == "") ViewBag.SortOrder = "asc";

                ViewBag.searchValue = searchString;
                ViewBag.sortProperty = sortProperty;
                ViewBag.page = page;

                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "10", Value = "10" });
                items.Add(new SelectListItem { Text = "20", Value = "20" });
                items.Add(new SelectListItem { Text = "25", Value = "25" });
                items.Add(new SelectListItem { Text = "50", Value = "50" });
                items.Add(new SelectListItem { Text = "100", Value = "100" });
                items.Add(new SelectListItem { Text = "200", Value = "200" });

                foreach (var item in items)
                {
                    if (item.Selected.ToString().Equals(size.ToString()) == true) item.Selected = true;
                }
                ViewBag.size = items;
                ViewBag.currentSize = size;

                var properties = typeof(HOADON).GetProperties();

                List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
                foreach (var item in properties)
                {
                    int order = 999;
                    var isVirtual = item.GetAccessors()[0].IsVirtual;
                    if (item.Name == "MAHD") order = 1;
                    if (item.Name == "MAKH") order = 2;
                    if (item.Name == "NGAYBAN") order = 3;
                    if (item.Name == "TIENBAN") order = 4;
                    if (item.Name == "GIAMGIA") continue;
                    if (item.Name == "THANHTIEN") continue;
                    if (item.Name == "PHISHIP") continue;
                    if (item.Name == "KHACHHANG") continue;
                    if (item.Name == "CHITIETHDs") continue;
                    Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
                    list.Add(t);
                }
                list = list.OrderBy(x => x.Item3).ToList();

                foreach (var item in list)
                {
                    // 2.2. Thuộc tính bình thường thì cho phép sắp xếp
                    if (!item.Item2) // Item2 dùng để kiểm tra thuộc tính ảo hay không?
                    {
                        // 2.3. So thuộc tính sortProperty và sortOrder để biết thuộc tính nào cần thay biểu tượng sắp giảm
                        if (sortOrder == "desc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-desc'></i></th></a></th>";
                        }
                        else if (sortOrder == "asc" && sortProperty == item.Item1)
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                                ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort-asc'></a></th>";
                        }
                        else
                        {
                            ViewBag.Headings += "<th><a href='?sortProperty=" + item.Item1 + "&sortOrder=" +
                               ViewBag.SortOrder + "&searchString=" + searchString + "'>" + item.Item1 + "<i class='fa fa-fw fa-sort'></a></th>";
                        }

                    }
                    // 2.4. Thuộc tính virtual (public virtual Category Category...) thì không sắp xếp được
                    // cho nên không cần tạo liên kết
                    else ViewBag.Headings += "<th>" + item.Item1 + "</th>";
                }

                var sp = from l in data.HOADONs select l;

                if (String.IsNullOrEmpty(sortProperty)) sortProperty = "MAHD";

                // 5. Sắp xếp tăng/giảm bằng phương thức OrderBy sử dụng trong thư viện Dynamic LINQ
                if (sortOrder == "desc") sp = sp.OrderBy(sortProperty + " desc");
                else if (sortOrder == "asc") sp = sp.OrderBy(sortProperty);
                else sp = sp.OrderBy("MAHD");

                // 5.1. Thêm phần tìm kiếm
                if (!String.IsNullOrEmpty(searchString))
                {
                    sp = sp.Where(s => s.MAHD.Contains(searchString));
                }
                page = page ?? 1;
                int pageSize = (size ?? 10);
                ViewBag.pageSize = pageSize;
                int pageNumber = (page ?? 1);
                int checkTotal = (int)(sp.ToList().Count / pageSize) + 1;
                if (pageNumber > checkTotal) pageNumber = checkTotal;
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpGet]
        public ActionResult CTHoaDon(string mahd)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                var cthd = data.CHITIETHDs.Where(ct => ct.MAHD.Contains(mahd));
                return View(cthd);
            }
        }
        DatModelContainer db = new DatModelContainer();
        public ActionResult AdTinTuc()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                var tt = from t in db.TINTUCs select t;
                return View(tt);
            }
        }
        public ActionResult ThemTinTuc()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
               return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTinTuc(TINTUC tt, FormCollection col)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                string[] a = col["picture"].Split('/');
                tt.TENTC = col["TENTC"];
                tt.MOTA = col["MOTA"];
                tt.VT = int.Parse(col["VT"]);
                tt.HINHANH = a[3];
                db.TINTUCs.Add(tt);
                db.SaveChanges();
                return RedirectToAction("ADTinTuc", "Admin");
            }
        }
        public ActionResult EditTT(int matc)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                var tt = db.TINTUCs.SingleOrDefault(t => t.MATC == matc);
                return View(tt);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTT(FormCollection col, TINTUC tt)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                var tintuc = db.TINTUCs.FirstOrDefault(t => t.MATC == tt.MATC);
                string[] a = col["picture"].Split('/');
                tintuc.HINHANH = a[3];
                tintuc.TENTC = col["TENTC"];
                tintuc.VT = int.Parse(col["VT"]);
                tintuc.MOTA = col["MOTA"];
                UpdateModel(tintuc);
                db.SaveChanges();
                return RedirectToAction("AdTinTuc", "Admin");
            }
        }
        [HttpGet]
        public ActionResult DeleteTT(int matc)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                var all_sp = db.TINTUCs.SingleOrDefault(s => s.MATC == matc);
                if (all_sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(all_sp);
            }
        }
        [HttpPost, ActionName("DeleteTT")]
        public ActionResult DeletedTT(int matc)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else
            {
                var sp = db.TINTUCs.SingleOrDefault(s => s.MATC == matc);
                db.TINTUCs.Remove(sp);
                db.SaveChanges();
                return RedirectToAction("ADTinTuc");
            }
        }

        public ActionResult QLGiaoDienTC()
        {
           if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
           else if (Session["quyen"].ToString().Equals("Admin") == true)
           {

               return View();
           }
           else
           {
               return RedirectToAction("Homequanly","Admin");
           }
        }
        [HttpPost]
        public ActionResult QLGiaoDienTC(FormCollection col)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
           else if (Session["quyen"].ToString().Equals("Admin") == true)
           
            {
                Session["tintucchinh"] = col["tintuchinh"];
                Session["tintucphu1"] = col["tintucphu1"];
                Session["tintucphu2"] = col["tintucphu2"];
                Session["tintucphu3"] = col["tintucphu3"];
                return RedirectToAction("AdTinTuc", "Admin");
            }
            else
            {
                return RedirectToAction("Homequanly", "Admin");
            }
        }
        public ActionResult GDGioiThieu()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else if (Session["quyen"].ToString().Equals("Admin") == true)
           
            {
                var ttgt = from t in data.GioiThieus select t;
                return View(ttgt);
            }
            else
            {
                return RedirectToAction("Homequanly", "Admin");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GDGioiThieu(FormCollection col, GioiThieu gt)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "DangKy_DangNhap");
            else if (Session["quyen"].ToString().Equals("Admin") == true)
            {
                string[] s;
                gt = (from t in data.GioiThieus select t).First();
                if (gt != null)
                {
                    gt.ThongTin = col["tt"];
                    gt.DiaChi = col["dc"];
                    gt.SDT = col["phone"];
                    gt.Email = col["email"];
                    gt.fax = col["fax"];
                    s = col["ha"].Split('/');
                    gt.HinhAnh = s[s.Length - 1];
                }
                UpdateModel(gt);
                data.SubmitChanges();
                return RedirectToAction("GDGioiThieu", "Admin");
                // return View();
            }
            else
            {
                return RedirectToAction("Homequanly", "Admin");
            }
        }
        public PartialViewResult Footer()
        {
            var ttgt = from t in data.GioiThieus where t.MaGT == 1 select t;
            return PartialView(ttgt);
        }
    }
    
}
