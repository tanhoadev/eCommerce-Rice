using MessagePack;

namespace DoAn.Areas.admin.Models
{
    public class DongGoi
    {
        public int IDQuyCach { get; set; }
        public int IDGao { get; set; }
        public Gao gao { get; set; }
        public QuyCachDongGoi quyCachDongGoi { get; set; }
    }
}
