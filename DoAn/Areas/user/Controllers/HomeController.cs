using DoAn.Areas.admin.Data;
using DoAn.Areas.admin.Models;
using DoAn.Areas.user.Utilities;
using DoAn.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using Stripe.Checkout;
using System;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using X.PagedList;
using static System.Net.Mime.MediaTypeNames;

namespace DoAn.Areas.user.Controllers
{
    [Area("user")]
    public class HomeController : Controller
    {
        private readonly KhoGaoDbContext context;
        private readonly IToastNotification toast;

        public HomeController(KhoGaoDbContext khoGaoDbContext, IToastNotification toast)
        {
            context = khoGaoDbContext;
            this.toast = toast;
        }
        [Route("")]
        public async Task<IActionResult> Index(int? page)
        {
            var loaig = await context.LoaiGao.AsNoTracking().ToListAsync();
            for(int i = 0; i < loaig.Count; i++)
            {
                var loaigao = await context.Gao.Where(x => x.IDLoaiGao == loaig[i].Id).AsNoTracking().ToListAsync();
                ViewData["soLoai" + i] = loaigao.Count;
            }
            var gao = await context.Gao.Where(x => x.DangBanChay == 1).AsNoTracking().ToListAsync();
            for (int i = 0; i < gao.Count; i++)
            {
                var danhgia = await context.DanhGia.Where(x => x.IDGao == gao[i].Id).AsNoTracking().ToListAsync();
                ViewData["danhgia" + i] = danhgia.Count();
            }
            var pageNumber = page ?? 1;
            ViewBag.gao = await context.Gao.Where(x => x.DangBanChay == 1).AsNoTracking().ToPagedListAsync(pageNumber, 10);
            return View(loaig);
        }
        public async Task<IActionResult> Shop(int? page, int all)
        {
            if(all == 1)
            {
                HttpContext.Session.Remove("idLoai");
            }
            ViewBag.ActionName = "Shop";
            var pageNumber = page ?? 1;
            var loaigao = await context.LoaiGao.AsNoTracking().ToListAsync();
            ViewBag.loai = loaigao;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("idLoai")))
            {
                int idLoai = int.Parse(HttpContext.Session.GetString("idLoai"));
                var gaox = await context.Gao.Where(x => x.IDLoaiGao == idLoai).AsNoTracking().ToPagedListAsync(pageNumber, 10);
                for (int i = 0; i < gaox.Count; i++)
                {
                    var danhgia = await context.DanhGia.Where(x => x.IDGao == gaox[i].Id).AsNoTracking().ToListAsync();
                    ViewData["danhgia" + i] = danhgia.Count();
                }
                return View(gaox);
            }
            var gao = await context.Gao.AsNoTracking().ToPagedListAsync(pageNumber, 10);
            for (int i = 0; i < gao.Count; i++)
            {
                var danhgia = await context.DanhGia.Where(x => x.IDGao == gao[i].Id).AsNoTracking().ToListAsync();
                ViewData["danhgia" + i] = danhgia.Count();
            }
            return View(gao);
        }
        public async Task<IActionResult> SearchProduct(string nameProduct, int? page)
        {
            ViewBag.ActionName = "SearchProduct";
            if (!string.IsNullOrEmpty(nameProduct))
            {
                ViewBag.riceName = nameProduct;
                var loaigaox = await context.LoaiGao.AsNoTracking().ToListAsync();
                ViewBag.loai = loaigaox;
                var pageNum = page?? 1;
                var gaoProduct = await context.Gao.Where(x => x.tenGao.Contains(nameProduct)).AsNoTracking().ToPagedListAsync(pageNum, 10);
                for (int i = 0; i < gaoProduct.Count; i++)
                {
                    var danhgia = await context.DanhGia.Where(x => x.IDGao == gaoProduct[i].Id).AsNoTracking().ToListAsync();
                    ViewData["danhgia" + i] = danhgia.Count();
                }
                return View("~/Areas/user/Views/Home/Shop.cshtml", gaoProduct);
            }
            ViewBag.riceName = "";
            var loaigao = await context.LoaiGao.AsNoTracking().ToListAsync();
            ViewBag.loai = loaigao;
            var pageNumber = page ?? 1;
            var gaoProductx = await context.Gao.AsNoTracking().ToPagedListAsync(pageNumber, 10);
            for (int i = 0; i < gaoProductx.Count; i++)
            {
                var danhgia = await context.DanhGia.Where(x => x.IDGao == gaoProductx[i].Id).AsNoTracking().ToListAsync();
                ViewData["danhgia" + i] = danhgia.Count();
            }
            return View("~/Areas/user/Views/Home/Shop.cshtml", gaoProductx);
        }
        public async Task<IActionResult> SortProduct(int num, int? page)
        {
            var loaigao = await context.LoaiGao.AsNoTracking().ToListAsync();
            ViewBag.loai = loaigao;
            ViewBag.ActionName = "SortProduct";
            if (num == 1)
            {
                ViewBag.numSort = 1;
                var pageNumber = page ?? 1;
                var gao = await context.Gao.Where(x => x.DangBanChay == 1).AsNoTracking().ToPagedListAsync(pageNumber, 10);
                for (int i = 0; i < gao.Count; i++)
                {
                    var danhgia = await context.DanhGia.Where(x => x.IDGao == gao[i].Id).AsNoTracking().ToListAsync();
                    ViewData["danhgia" + i] = danhgia.Count();
                }
                return View("~/Areas/user/Views/Home/Shop.cshtml", gao);
            }
            else if(num == 2)
            {
                ViewBag.numSort = 2;
                var pageNumber = page ?? 1;
                var gao = await context.Gao.OrderByDescending(x => x.soSao).AsNoTracking().ToPagedListAsync(pageNumber, 10);
                for (int i = 0; i < gao.Count; i++)
                {
                    var danhgia = await context.DanhGia.Where(x => x.IDGao == gao[i].Id).AsNoTracking().ToListAsync();
                    ViewData["danhgia" + i] = danhgia.Count();
                }
                return View("~/Areas/user/Views/Home/Shop.cshtml", gao);
            }
            else if(num == 3)
            {
                ViewBag.numSort = 3;
                var pageNumber = page ?? 1;
                var gao = await context.Gao.OrderBy(x => x.giaBan).AsNoTracking().ToPagedListAsync(pageNumber, 10);
                for (int i = 0; i < gao.Count; i++)
                {
                    var danhgia = await context.DanhGia.Where(x => x.IDGao == gao[i].Id).AsNoTracking().ToListAsync();
                    ViewData["danhgia" + i] = danhgia.Count();
                }
                return View("~/Areas/user/Views/Home/Shop.cshtml", gao);
            }
            else if(num == 4)
            {
                ViewBag.numSort = 4;
                var pageNumber = page ?? 1;
                var gao = await context.Gao.OrderByDescending(x => x.giaBan).AsNoTracking().ToPagedListAsync(pageNumber, 10);
                for (int i = 0; i < gao.Count; i++)
                {
                    var danhgia = await context.DanhGia.Where(x => x.IDGao == gao[i].Id).AsNoTracking().ToListAsync();
                    ViewData["danhgia" + i] = danhgia.Count();
                }
                return View("~/Areas/user/Views/Home/Shop.cshtml", gao);
            }
            return View("kk");
        }
        [HttpPost]
        public async Task<IActionResult> Shop(int id, int? page)
        {
            HttpContext.Session.SetString("idLoai", id.ToString());
            var loaigao = await context.LoaiGao.AsNoTracking().ToListAsync();
            ViewBag.loai = loaigao;
            var pageNumber = page ?? 1;
            ViewBag.ActionName = "Shop";
            var gao = await context.Gao.Where(x => x.IDLoaiGao == id).AsNoTracking().ToPagedListAsync(pageNumber, 10);
            for (int i = 0; i < gao.Count; i++)
            {
                var danhgia = await context.DanhGia.Where(x => x.IDGao == gao[i].Id).AsNoTracking().ToListAsync();
                ViewData["danhgia" + i] = danhgia.Count();
            }
            return View(gao);
        }
        public async Task<IActionResult> Detail(int id)
        {
            HttpContext.Session.SetString("IDGao", id.ToString());
            var gao = await context.Gao.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            var goi = await context.DongGoi.Where(x => x.IDGao == id).Include(x => x.quyCachDongGoi).AsNoTracking().ToListAsync();
            ViewBag.goi = goi;
            var binhluan = await context.DanhGia.Include(x => x.taiKhoan).Where(x => x.IDGao == id).OrderByDescending(x => x.thoiGian).AsNoTracking().ToListAsync();
            ViewBag.commentss = binhluan;
            List<string> hinh = new List<string>();
            foreach (var img in binhluan)
            {
                var tmp = await context.KhachHang.Where(x => x.IDTaiKhoan == img.IDTaiKhoan).AsNoTracking().FirstOrDefaultAsync();
                hinh.Add(tmp.AnhDaiDien);
            }
            ViewBag.hinh = hinh;
            return View(gao);
        }
        [HttpPost]
        public async Task<IActionResult> Detail(int quatityProduct, int idGao, int size, string hinhanh)
        {
            try
            {
                int idTK = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
                var gioHang = await context.GioHang.Where(x => x.IDTaiKhoan == idTK).AsNoTracking().FirstOrDefaultAsync();
                ChiTietGioHang ct = new ChiTietGioHang();
                TempData["idGioHang"] = gioHang.Id;
                ct.IDGioHang = gioHang.Id;
                ct.IDGao = idGao;
                ct.soLuong = quatityProduct;
                ct.khoiLuong = size;
                ct.hinhAnh = hinhanh;
                context.Add(ct);
                await context.SaveChangesAsync();
                toast.AddSuccessToastMessage("Thêm thành công sản phẩm vào giỏ hàng", new ToastrOptions { Title = "Thông báo" });
                var hang = await context.ChiTietGioHang.Where(x => x.IDGioHang == gioHang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
                HttpContext.Session.SetString("soluong", hang.Count.ToString());
                return RedirectToAction("Cart");
            }
            catch
            {
                toast.AddErrorToastMessage("Vui lòng đăng nhập để mua hàng", new ToastrOptions { Title = "Thông báo" });
                return RedirectToAction("Detail");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Comment([FromBody] CommentData data)
        {
            try
            {
                int id = int.Parse(HttpContext.Session.GetString("IDGao"));
                int idTK = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
                int numStar = data.numStar;
                string comment = data.comment;
                int idgao = data.idgao;
                int sosao = 0;
                var gao = await context.Gao.Where(x => x.Id == idgao).AsNoTracking().FirstOrDefaultAsync();
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("admin")))
                {
                    var danhgia = await context.DanhGia.Include(x => x.taiKhoan).Where(x => x.IDGao == idgao && x.taiKhoan.loaiTaiKhoan == 0).AsNoTracking().ToListAsync();
                    if(danhgia.Count == 0)
                    {
                        gao.soSao = 5;
                    }
                    sosao = (numStar + gao.soSao) / 2;
                    gao.soSao = sosao;
                    context.Update(gao);
                    await context.SaveChangesAsync();
                }
                else
                {
                    sosao = gao.soSao;
                }
                DanhGia xx = new DanhGia() { IDTaiKhoan = idTK, soSao = numStar, noiDung = comment, IDGao = idgao, thoiGian = DateTime.Now };
                context.Add(xx);
                context.SaveChanges();
                var name = await context.TaiKhoan.Where(x => x.Id == idTK).AsNoTracking().FirstOrDefaultAsync();
                var khachang = await context.KhachHang.Where(x => x.IDTaiKhoan == name.Id).AsNoTracking().FirstOrDefaultAsync();
                var danhg = new { Tentk = name.taiKhoan, SoSao = numStar, NoiDung = comment, IDGao = idgao, LoaiTK = name.loaiTaiKhoan, ThoiGian = DateTime.Now, Hinh = khachang.AnhDaiDien };
                return Json(new { success = true, data = danhg, sao = sosao});
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine(ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetComment()
        {
            try
            {
                int id = int.Parse(HttpContext.Session.GetString("IDGao"));
                var binhluan = await context.DanhGia.Include(x => x.taiKhoan).Where(x => x.IDGao == id).OrderByDescending(x => x.thoiGian).AsNoTracking().ToListAsync();
                List<string> hinhx = new List<string>();
                foreach (var img in binhluan)
                {
                    var tmp = await context.KhachHang.Where(x => x.IDTaiKhoan == img.IDTaiKhoan).AsNoTracking().FirstOrDefaultAsync();
                    hinhx.Add(tmp.AnhDaiDien);
                }

                var binhluan1 = await context.DanhGia
                    .Where(x => x.IDGao == id)
                    .OrderByDescending(x => x.thoiGian)
                    .AsNoTracking()
                    .Select(x => new
                    {
                        TaiKhoanTen = x.taiKhoan.taiKhoan,
                        ThoiGian = x.thoiGian,
                        NoiDung = x.noiDung,
                        LoaiTaiKhoan = x.taiKhoan.loaiTaiKhoan,
                        NumStar = x.soSao,
                    })
                    .ToListAsync();
                var binluanNew = binhluan1.Select((item, index) => new
                {
                    item.TaiKhoanTen,
                    item.ThoiGian,
                    item.NoiDung,
                    item.LoaiTaiKhoan,
                    item.NumStar,
                    Hinh = hinhx[index],
                }).ToList();
                return Json(binluanNew);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine(ex.Message);
                return Json(new { success = false, error = ex.Message });
            }
        }
        public class CommentData
        {
            public int numStar { get; set; }
            public string comment { get; set; }
            public int idgao { get; set; }
        }
        public async Task<IActionResult> Cart()
        {
            string trangthai = HttpContext.Session.GetString("TrangThai");
            if (!string.IsNullOrEmpty(trangthai))
            {
                toast.AddSuccessToastMessage("Giỏ hàng đã được cập nhật", new ToastrOptions { Title = "Thông báo"});
            }
            else
            {
                toast.AddInfoToastMessage("Giỏ hàng đã được cập nhật mới nhất và không cần cập nhật thêm", new ToastrOptions { Title = "Thông báo" });
            }
            int idtk = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
            var giohang = await context.GioHang.Where(x => x.IDTaiKhoan == idtk).AsNoTracking().FirstOrDefaultAsync();
            var hang = await context.ChiTietGioHang.Where(x => x.IDGioHang == giohang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
            return View(hang);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ChiTietGioHang chitiet = new ChiTietGioHang { Id = id};
            context.Remove(chitiet);
            await context.SaveChangesAsync();
            int idtk = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
            var giohang = await context.GioHang.Where(x => x.IDTaiKhoan == idtk).AsNoTracking().FirstOrDefaultAsync();
            var hang = await context.ChiTietGioHang.Where(x => x.IDGioHang == giohang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
            HttpContext.Session.SetString("soluong", hang.Count.ToString());
            toast.AddSuccessToastMessage("Đã xóa thành công 1 sản phẩm khỏi giỏ hàng", new ToastrOptions { Title = "Thông báo"});
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody] List<ChiTietGioHang> myData, List<int> listSize, int IDD)
        {
            //JsonConvert.DeserializeObject<List<ChiTietGioHang>>;
            try
            {
                var listQuantity = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("quantityx"));
                if (listQuantity != null && listQuantity.Count > 0)
                {
                    int updated = 0;
                    for (int i = 0; i < listQuantity.Count; i++)
                    {
                        if (listQuantity[i] != myData[i].soLuong)
                        {
                            myData[i].soLuong = listQuantity[i];
                            context.Update(myData[i]);
                            await context.SaveChangesAsync();
                            updated++;
                        }
                    }
                    HttpContext.Session.Remove("quantityx");
                    if (updated > 0)
                    {
                        HttpContext.Session.SetString("TrangThai", "OK");
                    }
                    else
                    {
                        HttpContext.Session.Remove("TrangThai");
                    }

                }
                else
                {
                    HttpContext.Session.Remove("TrangThai");
                }
            }
            catch 
            {
                return Json(myData);
            }
            return Json(myData);
        }

        [HttpPost]
        public IActionResult Quantityx([FromBody] List<int> qua)
        {
            HttpContext.Session.SetString("quantityx", JsonConvert.SerializeObject(qua));
            return Json(qua);
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
            int idtk = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
            var idGiohang = await context.GioHang.Where(x => x.IDTaiKhoan == idtk).AsNoTracking().FirstOrDefaultAsync();
            var hangs = await context.ChiTietGioHang.Where(x => x.IDGioHang == idGiohang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
            ViewBag.productCarts = hangs;
            KhachHang kh = await context.KhachHang.Where(x => x.IDTaiKhoan == idtk).AsNoTracking().FirstOrDefaultAsync();
            ViewBag.khachhang = kh;
            return View();
        }
        public async Task<IActionResult> Success()
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
            foreach(var item in hangs)
            {
                ChiTietHoaDon ct = new ChiTietHoaDon() { IDHoaDon = hoadon.Id, tenGao = item.gao.tenGao, soLuong = item.soLuong, gia = item.gao.giaBan, hinhAnh = item.gao.HinhAnh, khoiLuong = item.khoiLuong};
                context.Add(ct);
                await context.SaveChangesAsync();
            }
            foreach (var item in hangs)
            {
                ChiTietGioHang x = new ChiTietGioHang() { Id = item.Id};
                context.Remove(x);
                await context.SaveChangesAsync();
            }
            var hang = await context.ChiTietGioHang.Where(x => x.IDGioHang == idGiohang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
            HttpContext.Session.SetString("soluong", hang.Count.ToString());
            toast.AddSuccessToastMessage("Đặt hàng thành công. Quý khách vui lòng kiểm tra thông tin chi tiết đơn hàng tại mục 'Đơn mua'", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "paid")
            {
                return RedirectToAction("Success");
            }
            return RedirectToAction("Cart");
        }
        [HttpPost]
        public async Task<IActionResult> Payment(HoaDon hoadon, int payment)
        {
            string hoadonjson = JsonConvert.SerializeObject(hoadon);
            HttpContext.Session.SetString("MyObjectKey", hoadonjson);
            if(payment == 2)
            {
                return RedirectToAction("CreatePaymentUrl", "CheckOut", hoadon);
            }
            int idtk = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
            var idGiohang = await context.GioHang.Where(x => x.IDTaiKhoan == idtk).AsNoTracking().FirstOrDefaultAsync();
            var hangs = await context.ChiTietGioHang.Where(x => x.IDGioHang == idGiohang.Id).Include(x => x.gao).AsNoTracking().ToListAsync();
            ViewBag.productCarts = hangs;

            var domain = "https://localhost:7205/user/Home/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"OrderConfirmation",
                CancelUrl = domain + $"Cart",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = "chau033033@gmail.com",
            };
            foreach (var item in hangs)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.khoiLuong * item.gao.giaBan),
                        Currency = "vnd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{item.gao.tenGao} loại {item.khoiLuong} kg",
                        }
                    },
                    Quantity = item.soLuong
                };
                options.LineItems.Add(sessionListItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        public async Task<IActionResult> Profile()
        {
            int idtk = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
            var khach = await context.KhachHang.Where(h => h.IDTaiKhoan == idtk).Include(x => x.taiKhoan).AsNoTracking().FirstOrDefaultAsync();
            return View(khach);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(KhachHang khach, IFormFile avatar)
        {
            if (avatar != null && avatar.Length > 0)
            {
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload"));
                khach.AnhDaiDien = Path.GetFileName(avatar.FileName);
                using (var filestream = new FileStream(Path.Combine(path, khach.AnhDaiDien), FileMode.Create))
                {
                    await avatar.CopyToAsync(filestream);
                }
                
            }
            if (DateTime.Now.Year - khach.ngaySinh.Year < 18 || DateTime.Now.Year - khach.ngaySinh.Year > 100)
            {
                toast.AddWarningToastMessage("Ngày sinh của bạn không hợp lệ. Vui lòng kiểm tra lại", new ToastrOptions { Title = "Thông báo" });
            }
            else
            {
                HttpContext.Session.Remove("avatar");
                HttpContext.Session.SetString("avatar", khach.AnhDaiDien);
                context.Update(khach);
                await context.SaveChangesAsync();
                toast.AddSuccessToastMessage("Cập nhật thành công hồ sơ", new ToastrOptions { Title = "Thông báo" });
            }
            return RedirectToAction("Profile");
        }
        public async Task<IActionResult> Order()
        {
            int idtk = int.Parse(HttpContext.Session.GetString("iDTaiKhoan"));
            var khachHang = await context.KhachHang.Where(x => x.IDTaiKhoan == idtk).AsNoTracking().FirstOrDefaultAsync();
            var donmua = await context.HoaDon.Where(x => x.IDKhachHang == khachHang.Id).OrderByDescending(x => x.NgayThanhToan).AsNoTracking().ToListAsync();
            for(int i = 0; i < donmua.Count; i++)
            {
                var ct = await context.ChiTietHoaDon.Where(x => x.IDHoaDon == donmua[i].Id).AsNoTracking().ToListAsync();
                int sumTotal = 0;
                for(int x = 0; x < ct.Count; x++)
                {
                    sumTotal += ct[x].soLuong;
                }
                ViewData["sumTotal" + i] = sumTotal;
                sumTotal= 0;
                ViewData["hoadon" + i] = ct;
            }
            return View(donmua);
        }
        public async Task<IActionResult>ChangePassWord()
        {
            int id = (int.Parse(HttpContext.Session.GetString("iDTaiKhoan")));
            var taik = await context.TaiKhoan.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(taik);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassWord(TaiKhoan taiKhoan ,string pass, string newPass, string confirmPass)
        {
            var taik = await context.TaiKhoan.Where(x => x.Id == taiKhoan.Id).AsNoTracking().FirstOrDefaultAsync();
            string encrypPass = Utilities1.Encrypt(pass);
            string encrypNewpass = Utilities1.Encrypt(newPass);
            if (encrypPass == taik.matKhau && newPass != pass && newPass == confirmPass)
            {
                taik.matKhau = encrypNewpass;
                context.Update(taik);
                await context.SaveChangesAsync();
                toast.AddSuccessToastMessage("Thay đổi mật khẩu thành công", new ToastrOptions { Title = "Thông báo" });
                ViewData["pass"] = "";
                ViewData["newpass"] = "";
                ViewData["confirmpass"] = "";
                return View(taik);
            }
            ViewData["pass"] = pass;
            ViewData["newpass"] = newPass;
            ViewData["confirmpass"] = confirmPass;
            toast.AddErrorToastMessage("Thay đổi mật khẩu thất bại. Vui lòng kiểm tra lại mật khẩu và đảm bảo mật khẩu mới không giống mật khẩu cũ", new ToastrOptions { Title="Thông báo"});
            return View(taiKhoan);
        }
        public async Task<IActionResult> LogOut()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("admin"))){
                HttpContext.Session.Remove("admin");
            }
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("avatar");
            HttpContext.Session.Remove("iDTaiKhoan");
            toast.AddSuccessToastMessage("Bạn đã đăng xuất thành công", new ToastrOptions { Title = "Thông báo" });
            return RedirectToAction("Index", "Home", new {area = "user"});
        }
    }
}
