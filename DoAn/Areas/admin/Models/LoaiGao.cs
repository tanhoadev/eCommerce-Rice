namespace DoAn.Areas.admin.Models
{
    public class LoaiGao
    {
        public int Id { get; set; }
        public string tenLoaiGao { get; set; }
        public string hinhAnh { get; set; }
        public ICollection<Gao> gaos { get; set; }
    }
}
