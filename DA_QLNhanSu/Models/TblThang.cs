using System;
using System.Collections.Generic;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblThang
    {
        public TblThang()
        {
            TblThongKeLuongs = new HashSet<TblThongKeLuong>();
        }

        public int MaThang { get; set; }
        public string TenThang { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<TblThongKeLuong> TblThongKeLuongs { get; set; }
    }
}
