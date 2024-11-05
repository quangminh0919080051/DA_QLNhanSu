using System;
using System.Collections.Generic;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblBaoHiemXh
    {
        public TblBaoHiemXh()
        {
            TblTtnhanViens = new HashSet<TblTtnhanVien>();
        }

        public int MaBhxh { get; set; }
        public DateTime? NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string NoiDkkcb { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<TblTtnhanVien> TblTtnhanViens { get; set; }
    }
}
