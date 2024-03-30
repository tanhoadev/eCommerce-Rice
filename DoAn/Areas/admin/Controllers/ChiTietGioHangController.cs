using DoAn.Areas.admin.Data;
using Microsoft.AspNetCore.Mvc;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class ChiTietGioHangController : Controller
    {
        private readonly KhoGaoDbContext context;
        public ChiTietGioHangController(KhoGaoDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
    }
}
