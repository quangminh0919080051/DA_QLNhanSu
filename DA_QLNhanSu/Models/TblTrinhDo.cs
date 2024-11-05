using System;
using System.Collections.Generic;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblTrinhDo
    {
        public TblTrinhDo()
        {
            TblLuongs = new HashSet<TblLuong>();
            TblTtnhanViens = new HashSet<TblTtnhanVien>();
        }

        public int MaTd { get; set; }
        public string TenTrinhDo { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<TblLuong> TblLuongs { get; set; }
        public virtual ICollection<TblTtnhanVien> TblTtnhanViens { get; set; }
    }
}
