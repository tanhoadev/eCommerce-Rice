using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class GaoController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public GaoController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }
        public async Task<IActionResult> Index()
        {
            var gao = await context.Gao.AsNoTracking().Include(x => x.loaiGao).ToListAsync();
            return View(gao);
        }
        public IActionResult Create()
        {
            List<SelectListItem> loaigao = new List<SelectListItem>();
            loaigao = context.LoaiGao.Select(x => new SelectListItem { Text = x.tenLoaiGao, Value = x.Id.ToString() }).ToList();
            ViewBag.LoaiGao = loaigao;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Gao gao, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload"));
                gao.HinhAnh = Path.GetFileName(image.FileName);
                using (var filestream = new FileStream(Path.Combine(path, gao.HinhAnh), FileMode.Create))
                {
                    await image.CopyToAsync(filestream);
                }
            }
            context.Add(gao);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Thêm thành công", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            List<SelectListItem> loaigao = new List<SelectListItem>();
            loaigao = context.LoaiGao.Select(x => new SelectListItem { Text = x.tenLoaiGao, Value=x.Id.ToString() }).ToList();
            ViewBag.LoaiGao = loaigao;
            var gao = await context.Gao.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(gao);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Gao gao, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload"));
                gao.HinhAnh = Path.GetFileName(image.FileName);
                using (var filestream = new FileStream(Path.Combine(path, gao.HinhAnh), FileMode.Create))
                {
                    await image.CopyToAsync(filestream);
                }
            }
            context.Update(gao);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Sửa thành công", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Gao gao = new Gao {Id = id};
            
            try
            {
                context.Remove(gao);
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
