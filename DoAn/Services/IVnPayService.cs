using CodeMegaVNPay.Models;
using DoAn.Areas.admin.Models;

namespace CodeMegaVNPay.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(HoaDon model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}