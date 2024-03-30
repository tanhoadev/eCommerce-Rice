namespace DoAn.Areas.admin.Models
{
    public class DanhGia
    {
        public int Id { get; set; }
        public int IDGao { get; set; }
        public int IDTaiKhoan { get; set; }
        public string noiDung { get; set; }
        public int soSao { get; set; }
        public DateTime thoiGian { get; set; }
        public Gao gao { get; set; }
        public TaiKhoan taiKhoan { get; set; }
    }
}
