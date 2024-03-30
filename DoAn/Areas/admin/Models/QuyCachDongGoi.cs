namespace DoAn.Areas.admin.Models
{
    public class QuyCachDongGoi
    {
        public int Id { get; set; }
        public int khoiLuong { get; set; }
        public ICollection<DongGoi> dongGois { get; set; }
    }
}
