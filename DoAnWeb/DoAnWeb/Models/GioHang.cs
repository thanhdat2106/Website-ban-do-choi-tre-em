using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    public class GioHang
    {
        KetNoi_DataContext data = new KetNoi_DataContext();
        public string MASP{set; get;}

        public string TENSP { set; get; }

        public string HINHANH { set; get; }

        public double DONGIA { set; get; }

        public int SOLUONG { set; get; }

        public double ThanhTien
        {
            get { return SOLUONG * DONGIA; }
        }
        public GioHang(string ma)
        {
            MASP = ma;
            SANPHAM sp = data.SANPHAMs.Single(s => s.MASP==MASP);
            TENSP = sp.TENSP;
            HINHANH = sp.HINHANH;
            DONGIA = (double)sp.DONGIA;
            SOLUONG = 1;
        }

    }
}