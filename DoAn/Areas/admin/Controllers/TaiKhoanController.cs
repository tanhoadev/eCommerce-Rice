using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class TaiKhoanController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public TaiKhoanController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            var taikhoan = await context.TaiKhoan.AsNoTracking().ToListAsync();
            return View(taikhoan);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var taikhoan = await context.TaiKhoan.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(taikhoan);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TaiKhoan tk)
        {
            context.Update(tk);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Sửa thành công", new ToastrOptions { Title = "Thông báo"});
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            TaiKhoan x = new TaiKhoan() { Id = id};
            context.Remove(x);
            context.SaveChanges();
            toast.AddSuccessToastMessage("Xóa thành công", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index");
        }
    }
}
