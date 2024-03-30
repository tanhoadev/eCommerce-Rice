namespace DoAn.Areas.admin.Models
{
    public class GioHang
    {
        public int Id { get; set; }
        public int IDTaiKhoan { get; set; }
        public TaiKhoan taiKhoan { get; set; }
        public ICollection<ChiTietGioHang> chiTietGioHangs { get; set; }
    }
}
