using System;
using System.Collections.Generic;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblDonVi
    {
        public TblDonVi()
        {
            TblLuongs = new HashSet<TblLuong>();
            TblTtnhanViens = new HashSet<TblTtnhanVien>();
        }

        public int MaDv { get; set; }
        public string TenDv { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<TblLuong> TblLuongs { get; set; }
        public virtual ICollection<TblTtnhanVien> TblTtnhanViens { get; set; }
    }
}
