using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Cryptography;
using System.Text;
namespace DoAn.Controllers
{
    public class LoginController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
        public LoginController(KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this.toast = toast;
        }
        public IActionResult Index(TaiKhoan tk)
        {
            var passInfo = TempData["pass"];
            ViewData["pass"] = passInfo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(TaiKhoan tk, string x)
        {
            ViewData["pass"] = tk.matKhau;
            tk.matKhau = Encrypt(tk.matKhau);
            var taik = await context.TaiKhoan.Where(x => x.taiKhoan == tk.taiKhoan && x.matKhau == tk.matKhau).AsNoTracking().FirstOrDefaultAsync();
            
            if(taik == null)
            {
                toast.AddInfoToastMessage("Tên đăng nhập hoặc mật khẩu không chính xác", new ToastrOptions { Title = "Thông báo"});
            }
            else
            {
                var khach = await context.KhachHang.Where(x => x.IDTaiKhoan == taik.Id).Include(x => x.taiKhoan).AsNoTracking().FirstOrDefaultAsync();
                if(khach.taiKhoan.loaiTaiKhoan == 1)
                {
                    HttpContext.Session.SetString("admin", tk.taiKhoan);
                }
                toast.AddSuccessToastMessage("Đăng nhập thành công", new ToastrOptions { Title = "Thông báo" });
                HttpContext.Session.SetString("username", tk.taiKhoan);
                HttpContext.Session.SetString("avatar", khach.AnhDaiDien);
                HttpContext.Session.SetString("iDTaiKhoan", taik.Id.ToString());
                var giohang = await context.GioHang.Where(x => x.IDTaiKhoan == taik.Id).AsNoTracking().FirstOrDefaultAsync();
                var hang = await context.ChiTietGioHang.Where(x => x.IDGioHang == giohang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
                HttpContext.Session.SetString("soluong", hang.Count.ToString());

                return RedirectToAction("Index", "Home", new { area = "user" });
            }
            return View(tk);
        }
    }
}
