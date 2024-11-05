using DA_QLNhanSu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DA_QLNhanSu.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLNhanSuContext _context;

        public HomeController(QLNhanSuContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) == true)
            {
                ModelState.AddModelError("", "Email không được để trống");
                return View(email);
            }
            if (string.IsNullOrEmpty(password) == true)
            {
                ModelState.AddModelError("", "Mật khẩu không được để trống");
                return View(password);
            }
            var user = _context.TblTtnhanViens.SingleOrDefault(x => x.Email.Trim().ToLower() == email.Trim().ToLower() && x.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("MaNV", user.MaNv.ToString());
                HttpContext.Session.SetString("TenNV", user.HoTen.ToString());
                if (user.Anh != null)
                {
                    HttpContext.Session.SetString("Image", user.Anh.ToString());
                }
                HttpContext.Session.SetString("Email", user.Email.Trim().ToLower());
                HttpContext.Session.SetInt32("Role", user.PhanQuyen);
                if(user.PhanQuyen == 1 || user.PhanQuyen == 2)
                {
                return RedirectToAction("Index");
                } else return RedirectToAction("ThueCaNhan", "HomeNV");
            }
            else
            {
                ModelState.AddModelError("", "Đăng Nhập Thất Bại! Kiểm Lại Thông Tin Đăng Nhập");
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("MaNv,HoTen,NgaySinh,GioiTinh,Cmnd,NoiSinh,DiaChi,Sdt,MaTd,MaBhxh,MaLuong,MaDv,MaThue,MaCm,Email,Password,PhanQuyen,Anh,GhiChu,DanToc,TonGiao,QuocTich")] TblTtnhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(nhanVien.HoTen) == true || string.IsNullOrEmpty(nhanVien.GioiTinh) == true || string.IsNullOrEmpty(nhanVien.Email) == true || nhanVien.Sdt == null || nhanVien.NgaySinh == null || nhanVien.Password == null)
                {
                    ModelState.AddModelError("", "Thông tin không được để trống");
                    return View(nhanVien);
                }
                var checkEmail = _context.TblTtnhanViens.SingleOrDefault(x => x.Email.Trim().ToLower() == nhanVien.Email.Trim().ToLower());
                if (checkEmail != null)
                {
                    ModelState.AddModelError("", "Địa chỉ Email đã tồn tại");
                    return View(nhanVien);
                }
                var checkPhone = _context.TblTtnhanViens.SingleOrDefault(x => x.Sdt == nhanVien.Sdt);
                if (checkPhone != null)
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại");
                    return View(nhanVien);
                }
                nhanVien.PhanQuyen = 3;
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View();
        }


        //Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Login");
        }

    }
}
