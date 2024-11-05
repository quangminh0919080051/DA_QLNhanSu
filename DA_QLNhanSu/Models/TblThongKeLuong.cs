using System;
using System.Collections.Generic;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblThongKeLuong
    {
        public int MaTkluong { get; set; }
        public int MaNv { get; set; }
        public int MaThang { get; set; }
        public int? LuongCoBan { get; set; }
        public int? ThuePhaiDong { get; set; }
        public int? Thuong { get; set; }
        public int? Phat { get; set; }
        public string GhiChu { get; set; }
        public int? TongLuong { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual TblTtnhanVien MaNvNavigation { get; set; }
        public virtual TblThang MaThangNavigation { get; set; }
    }
}
