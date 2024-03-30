using DoAn.Areas.admin.Models;
using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using CodeMegaVNPay.Models;
using CodeMegaVNPay.Services;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public HomeController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePaymentUrl(HoaDon model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }
       
    }
}