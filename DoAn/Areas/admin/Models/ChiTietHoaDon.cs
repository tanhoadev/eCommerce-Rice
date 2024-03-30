namespace DoAn.Areas.admin.Models
{
    public class ChiTietHoaDon
    {
        public int Id { get; set; }
        public int IDHoaDon { get; set; }
        public string tenGao { get; set; }
        public int soLuong { get; set; }
        public int khoiLuong { get; set; }
        public decimal gia { get; set; }
        public string hinhAnh { get; set; }
        public HoaDon hoaDon { get; set; }
    }
}
