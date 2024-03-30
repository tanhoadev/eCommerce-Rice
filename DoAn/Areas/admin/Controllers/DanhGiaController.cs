using DoAn.Areas.admin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class DanhGiaController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public DanhGiaController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            var danhgia = await context.DanhGia.Include(x => x.gao).Include(x => x.taiKhoan).AsNoTracking().ToListAsync();
            return View(danhgia);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var danhGia = await context.DanhGia.Where(x => x.Id == id).Include(x => x.taiKhoan).AsNoTracking().FirstOrDefaultAsync();
            if(danhGia.taiKhoan.loaiTaiKhoan == 0)
            {
                var gao = await context.Gao.Where(x => x.Id == danhGia.IDGao).AsNoTracking().FirstOrDefaultAsync();
                var dg = await context.DanhGia.Include(x => x.taiKhoan).Where(x => x.IDGao == gao.Id && x.taiKhoan.loaiTaiKhoan == 0).AsNoTracking().ToListAsync();
                int sum = 0;
                foreach(var x in dg)
                {
                    sum += x.soSao;
                }
                gao.soSao = sum/ 5;
                context.Update(gao);
                await context.SaveChangesAsync();
            }
            context.Remove(danhGia);
            await context.SaveChangesAsync();
            toast.AddSuccessToastMessage("Xóa thành công", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index");
        }
    }
}
