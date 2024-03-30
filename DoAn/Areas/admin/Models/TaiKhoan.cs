namespace DoAn.Areas.admin.Models
{
    public class TaiKhoan
    {
        public int Id { get; set; }
        public string taiKhoan { get; set; }
        public string matKhau { get; set; }
        public int loaiTaiKhoan { get; set; }
        public ICollection<GioHang> gioHangs { get; set; }
        public ICollection<KhachHang> khachHangs { get; set; }
        public ICollection<DanhGia> danhGias { get; set; }

    }
}
