using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class KhachHangController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public KhachHangController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            var khach = await context.KhachHang.Where(x => x.taiKhoan.loaiTaiKhoan == 0).Include(x => x.taiKhoan).AsNoTracking().ToListAsync();
            return View(khach);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
    }
}
