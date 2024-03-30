using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class DongGoiController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public DongGoiController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }
        public async Task<IActionResult> Index()
        {
            var donggoi = await context.DongGoi.Include(x => x.quyCachDongGoi).Include(x => x.gao).AsNoTracking().ToListAsync();
            return View(donggoi);
        }
        public IActionResult Create()
        {
            List<SelectListItem> quycach = new List<SelectListItem>();
            List<SelectListItem> gao = new List<SelectListItem>();
            quycach = context.QuyCachDongGoi.Select(x => new SelectListItem { Text = x.khoiLuong + "kg", Value = x.Id.ToString() }).ToList();
            gao = context.Gao.Select(x => new SelectListItem { Text = x.tenGao + " - " + x.Id, Value = x.Id.ToString() }).ToList();

            ViewBag.quyCach = quycach;
            ViewBag.gao = gao;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DongGoi donggoi)
        {
            try
            {
                context.Add(donggoi);
                await context.SaveChangesAsync();
                toast.AddSuccessToastMessage("Thêm thành công", new ToastrOptions { Title = "Thông báo" });
            }
            catch 
            {
                toast.AddErrorToastMessage("Bảng ghi này đã tồn tại", new ToastrOptions { Title = "Thông báo" });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int id1)
        {
            DongGoi donggoi = new DongGoi { IDGao = id, IDQuyCach = id1 };
            try
            {
                context.Remove(donggoi);
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
