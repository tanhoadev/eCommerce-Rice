namespace DoAn.Areas.admin.Models
{
    public class HoaDon
    {
        public int Id { get; set; }
        public decimal Gia { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string hoTen { get; set; }
        public string eMail { get; set; }
        public string sDt { get; set; }
        public int IDKhachHang { get; set; }
        public int TrangThai { get; set; }
        public string TinhThanh { get; set; }
        public string QuanHuyen { get; set; }
        public string PhuongXa { get; set; }
        public string SoNha { get; set; }
        public ICollection<ChiTietHoaDon> chiTietHoaDons { get; set; }
        public KhachHang khachHang { get; set; }
    }
}
