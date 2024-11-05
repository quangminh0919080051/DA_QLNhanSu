using System;
using System.Collections.Generic;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblThueThuNhapCaNhan
    {
        public TblThueThuNhapCaNhan()
        {
            TblTtnhanViens = new HashSet<TblTtnhanVien>();
        }

        public int MaThue { get; set; }
        public string CoQuanQuanLyThue { get; set; }
        public int? MaLuong { get; set; }
        public int? SoTien { get; set; }
        public DateTime? NgayDangKi { get; set; }
        public string GhiChu { get; set; }

        public virtual TblLuong MaLuongNavigation { get; set; }
        public virtual ICollection<TblTtnhanVien> TblTtnhanViens { get; set; }
    }
}
