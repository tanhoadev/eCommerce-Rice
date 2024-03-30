using Microsoft.AspNetCore.Mvc;

namespace DoAn.Areas.admin.Controllers
{
    [Area("admin")]
    public class NhapHangController : Controller
    {
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
