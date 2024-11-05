using System;
using System.Collections.Generic;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblLuong
    {
        public TblLuong()
        {
            TblThueThuNhapCaNhans = new HashSet<TblThueThuNhapCaNhan>();
            TblTtnhanViens = new HashSet<TblTtnhanVien>();
        }

        public int MaLuong { get; set; }
        public int? MaCm { get; set; }
        public int? MaTd { get; set; }
        public int? MaDv { get; set; }
        public int? LuongCoBan { get; set; }
        public int? LuongCbmoi { get; set; }
        public DateTime? NgayNhap { get; set; }
        public DateTime? NgaySua { get; set; }
        public string GhiChu { get; set; }

        public virtual TblChuyenMon MaCmNavigation { get; set; }
        public virtual TblDonVi MaDvNavigation { get; set; }
        public virtual TblTrinhDo MaTdNavigation { get; set; }
        public virtual ICollection<TblThueThuNhapCaNhan> TblThueThuNhapCaNhans { get; set; }
        public virtual ICollection<TblTtnhanVien> TblTtnhanViens { get; set; }
    }
}
