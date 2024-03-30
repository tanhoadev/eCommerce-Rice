using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public HomeController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            int year = DateTime.Now.Year;
            List<HoaDon> hoaDons= new List<HoaDon>();
            for(int i = 1; i <= 12; i++)
            {
                DateTime x = new DateTime(2023, i, 1);
                var hoadonx = await context.HoaDon.Where(x => x.NgayThanhToan.Year == year && x.NgayThanhToan.Month == i).AsNoTracking().ToListAsync();
                decimal gia = 0;
                foreach(var hd in hoadonx)
                {
                    gia += hd.Gia;
                }
                HoaDon h = new HoaDon() { NgayThanhToan = x, Gia = gia};
                hoaDons.Add(h);
            }
            ViewData["hoadon"] = hoaDons;
            var nguoidung = await context.TaiKhoan.Where(x => x.loaiTaiKhoan == 0).AsNoTracking().ToListAsync();
            var hoadon = await context.HoaDon.AsNoTracking().OrderByDescending(x => x.NgayThanhToan).ToListAsync();
            decimal sumTotal = 0;
            foreach(var x in hoadon)
            {
                sumTotal += x.Gia;
            }
            var choDuyet = await context.HoaDon.Where(x => x.TrangThai == 0).AsNoTracking().ToListAsync();
            ViewBag.nguoiDung = nguoidung.Count();
            ViewBag.hoaDonx = hoadon.Count();
            ViewBag.choDuyet = choDuyet.Count();
            ViewBag.doanhThu = sumTotal;
            return View(hoadon);
        }
    }
}
