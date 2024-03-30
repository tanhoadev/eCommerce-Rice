using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class QuyCachDongGoiController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public QuyCachDongGoiController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            var quycach = await context.QuyCachDongGoi.AsNoTracking().ToListAsync();
            return View(quycach);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(QuyCachDongGoi quyc)
        {
            context.Add(quyc);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Thêm thành công", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var quycach = await context.QuyCachDongGoi.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(quycach);
        }
        [HttpPost]
        public async Task<IActionResult> Update(QuyCachDongGoi quyc)
        {
            context.Update(quyc);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Sửa thành công", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            QuyCachDongGoi quyc= new QuyCachDongGoi { Id= id };
            try
            {
                context.Remove(quyc);
                await context.SaveChangesAsync();
                toast.AddSuccessToastMessage("Xóa thành công", new ToastrOptions { Title = "Thông báo" });
            }
            catch 
            {
                toast.AddErrorToastMessage("Không thể xóa bảng ghi này", new ToastrOptions { Title = "Thông báo" });
            }
            return RedirectToAction("Index");
        }
    }
}
