using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace DoAn.Controllers
{
    public class RegisterController : Controller
    {
        private KhoGaoDbContext context;
        private readonly IToastNotification _notify;
        public string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8= new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
        public RegisterController (KhoGaoDbContext context, IToastNotification toast)
        {
            this.context = context;
            this._notify = toast;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index( TaiKhoan tk, string hoTen, string sDT, string email, string repeat, DateTime birthday)
        {
            ViewData["hoTen"] = hoTen;
            ViewData["sdt"] = sDT;
            ViewData["email"] = email;
            ViewData["pass"] = tk.matKhau;
            ViewData["repeat"] = repeat;
            if (tk.matKhau != repeat)
            {
                _notify.AddWarningToastMessage("Mật khẩu không trùng khớp, Vui lòng kiểm tra lại");
                return View(tk);
            }
            if (DateTime.Now.Year - birthday.Year > 100)
            {
                _notify.AddWarningToastMessage("Ngày sinh của bạn không hợp lệ. Vui lòng kiểm tra lại", new ToastrOptions { Title = "Thông báo" });
                return View(tk);
            }
            if (DateTime.Now.Year - birthday.Year < 18)
            {
                _notify.AddWarningToastMessage("Xin lỗi, ngày sinh của bạn không hợp lệ. Để đăng ký, bạn cần phải đủ 18 tuổi. Vui lòng kiểm tra lại.", new ToastrOptions { Title = "Thông báo" });
                return View(tk);
            }
            var taik = context.TaiKhoan.Where(x => x.taiKhoan == tk.taiKhoan).FirstOrDefault();
            if (taik == null)
            {
                tk.loaiTaiKhoan = 0;
                ViewData["pass"] = tk.matKhau;
                tk.matKhau = Encrypt(tk.matKhau);
                context.Add(tk);
                int recored = await context.SaveChangesAsync();

                if (recored > 0)
                {
                    _notify.AddSuccessToastMessage("Đăng kí tài khoản thành công", new ToastrOptions { Title = "Thông báo" });
                    var kH = new KhachHang();
                    kH.AnhDaiDien = "avt-user.png";
                    kH.hoTen = hoTen;
                    kH.sDT = sDT;
                    kH.email = email;
                    kH.IDTaiKhoan = tk.Id;
                    kH.ngaySinh = birthday;
                    context.Add(kH);
                    int recoredkh = await context.SaveChangesAsync();

                    var gioHang = new GioHang();
                    gioHang.IDTaiKhoan = tk.Id;
                    context.Add(gioHang);
                    int recoredgh = await context.SaveChangesAsync();

                    TempData["pass"] = ViewData["pass"];
                    return RedirectToAction("Index", "Login", tk);
                }
            }
            else
            {
                _notify.AddWarningToastMessage("Tài khoản này đã tồn tại. Vui lòng đăng kí tài khoản khác", new ToastrOptions { Title= "Thông báo"});
            }
            return View(tk);
        }
    }
}
