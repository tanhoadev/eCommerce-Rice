using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Policy;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class LoaiGaoController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public LoaiGaoController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            var loaigao = await context.LoaiGao.AsNoTracking().ToListAsync();
            return View(loaigao);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LoaiGao loaig, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload"));
                loaig.hinhAnh = Path.GetFileName(image.FileName);
                using (var filestream = new FileStream(Path.Combine(path, loaig.hinhAnh), FileMode.Create))
                {
                    await image.CopyToAsync(filestream);
                }
            }
            context.Add(loaig);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Thêm thành công", new ToastrOptions { Title = "Thông báo"});
            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult> Update(int id)
        {
            var loaigao = await context.LoaiGao.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(loaigao);
        }
        [HttpPost]
        public async Task<IActionResult> Update(LoaiGao loaigao, IFormFile image1)
        {
            if (image1 != null && image1.Length > 0)
            {
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload"));
                loaigao.hinhAnh = Path.GetFileName(image1.FileName);
                using (var filestream = new FileStream(Path.Combine(path, loaigao.hinhAnh), FileMode.Create))
                {
                    await image1.CopyToAsync(filestream);
                }
            }
            context.Update(loaigao);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Sửa thành công", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            LoaiGao loaig = new LoaiGao { Id = id };
            try
            {
                context.Remove(loaig);
                int num = await context.SaveChangesAsync();
                if(num > 0)
                {
                    toast.AddSuccessToastMessage("Xóa thành công", new ToastrOptions { Title = "Thông báo" });
                }
            }
            catch
            {
                toast.AddErrorToastMessage("Không thể xóa bản ghi này", new ToastrOptions { Title = "Thông báo" });
            }
            return RedirectToAction("Index");
        }
    }
}
