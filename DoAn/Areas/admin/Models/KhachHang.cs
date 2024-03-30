namespace DoAn.Areas.admin.Models
{
    public class KhachHang
    {
        public int Id { get; set; }
        public string hoTen { get; set; }
        public string email { get; set; }
        public string sDT { get; set; }
        public DateTime ngaySinh { get; set; }
        public int IDTaiKhoan { get; set; }
        public string AnhDaiDien { get; set; }
        public TaiKhoan taiKhoan { get; set; }
        public ICollection<HoaDon> hoaDons { get; set; }
    }
}
