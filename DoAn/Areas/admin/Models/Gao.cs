namespace DoAn.Areas.admin.Models
{
    public class Gao
    {
        public int Id { get; set; }
        public string tenGao { get; set; }
        public string xuatXu { get; set; }
        public decimal giaBan { get; set; }
        public string dacTinh { get; set; }
        public int tinhTrang { get; set; }
        public string moTa { get; set; }
        public string HinhAnh { get; set; }
        public int IDLoaiGao { get; set; }
        public int DangBanChay { get; set; }
        public int conLai { get; set; }
        public int soSao { get; set; }
        public LoaiGao loaiGao { get; set; }
        public ICollection<ChiTietGioHang> chiTietGioHangs { get; set; }
        public ICollection<DanhGia> danhGias { get; set; }
        public ICollection<DongGoi> dongGois { get; set; }
        public ICollection<NhapHang> nhapHangs { get; set; }

    }
}
