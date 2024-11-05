using System;
using System.Collections.Generic;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblChuyenMon
    {
        public TblChuyenMon()
        {
            TblLuongs = new HashSet<TblLuong>();
            TblTtnhanViens = new HashSet<TblTtnhanVien>();
        }

        public int MaCm { get; set; }
        public string TenCm { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<TblLuong> TblLuongs { get; set; }
        public virtual ICollection<TblTtnhanVien> TblTtnhanViens { get; set; }
    }
}
