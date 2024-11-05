using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DA_QLNhanSu.Models
{
    public partial class TblTtnhanVien
    {
        public TblTtnhanVien()
        {
            TblThongKeLuongs = new HashSet<TblThongKeLuong>();
        }

        public int MaNv { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Cmnd { get; set; }
        public string NoiSinh { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public int? MaTd { get; set; }
        public int? MaBhxh { get; set; }
        public int? MaLuong { get; set; }
        public int? MaDv { get; set; }
        public int? MaThue { get; set; }
        public int? MaCm { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhanQuyen { get; set; }
        public string Anh { get; set; }
        public string GhiChu { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string QuocTich { get; set; }

        public virtual TblBaoHiemXh MaBhxhNavigation { get; set; }
        public virtual TblChuyenMon MaCmNavigation { get; set; }
        public virtual TblDonVi MaDvNavigation { get; set; }
        public virtual TblLuong MaLuongNavigation { get; set; }
        public virtual TblTrinhDo MaTdNavigation { get; set; }
        public virtual TblThueThuNhapCaNhan MaThueNavigation { get; set; }
        public virtual ICollection<TblThongKeLuong> TblThongKeLuongs { get; set; }

        [NotMapped]
        public IFormFile ConvertPhoto { get; set; }
    }
}
