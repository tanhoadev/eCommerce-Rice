using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class HoaDonController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public HoaDonController(KhoGaoDbContext khoGaoDbContext, IToastNotification toast)
        {
            context= khoGaoDbContext;
            this.toast = toast;
        }
        public async Task<IActionResult> Index()
        {
            var hoadon = await context.HoaDon.AsNoTracking().OrderByDescending(x => x.NgayThanhToan).ToListAsync();
            for (int i = 0; i < hoadon.Count; i++)
            {
                var ct = await context.ChiTietHoaDon.Where(x => x.IDHoaDon == hoadon[i].Id).AsNoTracking().ToListAsync();
                ViewData["hoadon" + i] = ct;
            }
            return View(hoadon);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, int trangth)
        {
            var hoadon = await context.HoaDon.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            hoadon.TrangThai = trangth;
            context.Update(hoadon);
            await context.SaveChangesAsync();
            var ctHoaDon = await context.ChiTietHoaDon.Where(x => x.IDHoaDon == hoadon.Id).AsNoTracking().ToListAsync();
            foreach (var x in ctHoaDon)
            {
                //var gao = context.Gao.Where(x => x.)
            }
            toast.AddSuccessToastMessage("Sửa thành công", new ToastrOptions { Title = "Thông báo"});
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            HoaDon x = new HoaDon() { Id = id};
            context.Remove(x);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Xóa thành công");
            return RedirectToAction("Index");
        }
    }
}
