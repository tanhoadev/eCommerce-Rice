namespace DoAn.Areas.admin.Models
{
    public class ChiTietGioHang
    {
        public int Id { get; set; }
        public int IDGioHang { get; set; }
        public int IDGao { get; set; }
        public int soLuong { get; set; }
        public int khoiLuong { get; set; }
        public string hinhAnh { get; set; }
        public GioHang gioHang { get; set; }
        public Gao gao { get; set; }
    }
}
