using CodeMegaVNPay.Services;
using DoAn.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using DoAn.Models;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using CodeMegaVNPay.Models;
using DoAn.Areas.admin.Data;
using NToastNotify;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DoAn.Areas.user.Controllers
{
    [Area("user")]
    public class CheckOutController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;
        public CheckOutController(IVnPayService vnPayService, KhoGaoDbContext context, IToastNotification toast)
        {
            _vnPayService = vnPayService;
            this.toast = toast;
            this.context = context;
        }

        public IActionResult CreatePaymentUrl(HoaDon model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }

        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            string vnp_ResponseCode = Request.Query["vnp_ResponseCode"].ToString();
            string vnp_TransactionStatus = Request.Query["vnp_TransactionStatus"].ToString();
            if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
            {
                int idtk = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
                var idGiohang = await context.GioHang.Where(x => x.IDTaiKhoan == idtk).AsNoTracking().FirstOrDefaultAsync();
                var hangs = await context.ChiTietGioHang.Where(x => x.IDGioHang == idGiohang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
                string jsonData = HttpContext.Session.GetString("MyObjectKey");
                HoaDon hoadon = JsonConvert.DeserializeObject<HoaDon>(jsonData);
                hoadon.TrangThai = 0;
                hoadon.NgayThanhToan = DateTime.Now;
                context.Add(hoadon);
                await context.SaveChangesAsync();
                foreach (var item in hangs)
                {
                    ChiTietHoaDon ct = new ChiTietHoaDon() { IDHoaDon = hoadon.Id, tenGao = item.gao.tenGao, soLuong = item.soLuong, gia = item.gao.giaBan, hinhAnh = item.gao.HinhAnh, khoiLuong = item.khoiLuong };
                    context.Add(ct);
                    await context.SaveChangesAsync();
                }
                foreach (var item in hangs)
                {
                    ChiTietGioHang x = new ChiTietGioHang() { Id = item.Id };
                    context.Remove(x);
                    await context.SaveChangesAsync();
                }
                var hang = await context.ChiTietGioHang.Where(x => x.IDGioHang == idGiohang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
                HttpContext.Session.SetString("soluong", hang.Count.ToString());
                toast.AddSuccessToastMessage("Đặt hàng thành công. Quý khách vui lòng kiểm tra thông tin chi tiết đơn hàng tại mục 'Đơn mua'", new ToastrOptions { Title = "Thông báo" });
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
