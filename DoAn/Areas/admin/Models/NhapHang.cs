namespace DoAn.Areas.admin.Models
{
    public class NhapHang
    {
        public int Id { get; set; }
        public int IDGao { get; set; }
        public int soLuongNhap { get; set; }
        public DateTime ngayNhap { get; set; }
        public Gao gao { get; set; }
    }
}
