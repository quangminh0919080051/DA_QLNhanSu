using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DA_QLNhanSu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DA_QLNhanSu.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly QLNhanSuContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserDashboardController(QLNhanSuContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: UserDashboard/Details
        public async Task<IActionResult> Details()
        {
            var SessionUserId = HttpContext.Session.GetString("MaNV");
            var id = int.Parse(SessionUserId);
            var user = await _context.TblTtnhanViens
                .Include(t => t.MaCmNavigation)
                .Include(t => t.MaDvNavigation)
                .Include(t => t.MaTdNavigation)
                .Include(t => t.MaBhxhNavigation)
                .Include(t => t.MaThueNavigation)
                .Include(t => t.MaLuongNavigation)
                .FirstOrDefaultAsync(m => m.MaNv == id);
            return View(user);
        }

        // GET: UserDashboard/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTtnhanVien = await _context.TblTtnhanViens.FindAsync(id);
            if (tblTtnhanVien == null)
            {
                return NotFound();
            }
            ViewData["MaBhxh"] = new SelectList(_context.TblBaoHiemXhs, "MaBhxh", "MaBhxh", tblTtnhanVien.MaBhxh);
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm", tblTtnhanVien.MaCm);
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "MaDv", tblTtnhanVien.MaDv);
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "MaLuong", tblTtnhanVien.MaLuong);
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo", tblTtnhanVien.MaTd);
            ViewData["MaThue"] = new SelectList(_context.TblThueThuNhapCaNhans, "MaThue", "MaThue", tblTtnhanVien.MaThue);
            return View(tblTtnhanVien);
        }

        // POST: UserDashboard/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TblTtnhanVien tblTtnhanVien)
        {
            if (id != tblTtnhanVien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(tblTtnhanVien.ConvertPhoto != null)
                {
                    string folder = "images/avatar";
                    folder += Guid.NewGuid().ToString()+"_"+ tblTtnhanVien.ConvertPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await tblTtnhanVien.ConvertPhoto.CopyToAsync(new FileStream(serverFolder,FileMode.Create));
                    tblTtnhanVien.Anh = $"/{folder}";
                }
                else // giữ nguyên giá trị của thuộc tính Anh
                {
                    tblTtnhanVien.Anh = _context.TblTtnhanViens.AsNoTracking().FirstOrDefault(x => x.MaNv == tblTtnhanVien.MaNv)?.Anh;
                }
                try
                {
                    _context.Update(tblTtnhanVien);
                    await _context.SaveChangesAsync();

                    HttpContext.Session.Remove("TenNV");
                    HttpContext.Session.SetString("TenNV", tblTtnhanVien.HoTen.ToString());
                    if (tblTtnhanVien.Anh != null)
                    {
                        HttpContext.Session.Remove("Image");
                        HttpContext.Session.SetString("Image", tblTtnhanVien.Anh.ToString());
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTtnhanVienExists(tblTtnhanVien.MaNv))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details");
            }
            ViewData["MaBhxh"] = new SelectList(_context.TblBaoHiemXhs, "MaBhxh", "MaBhxh", tblTtnhanVien.MaBhxh);
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm", tblTtnhanVien.MaCm);
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "MaDv", tblTtnhanVien.MaDv);
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "MaLuong", tblTtnhanVien.MaLuong);
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo", tblTtnhanVien.MaTd);
            ViewData["MaThue"] = new SelectList(_context.TblThueThuNhapCaNhans, "MaThue", "MaThue", tblTtnhanVien.MaThue);
            return View(tblTtnhanVien);
        }

        public IActionResult ChangPassWord()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangPassWord(string oldPassword, string newPassword, string cfPassword)
        {
            var SessionUserId = HttpContext.Session.GetString("MaNV");
            var id = int.Parse(SessionUserId);
            var nhanVien = await _context.TblTtnhanViens.FindAsync(id);
            if (nhanVien != null) { 
            if (oldPassword == null)
            {
                ModelState.AddModelError("", "Mật khẩu không được để trống");
                return View();
            }
            if (newPassword == null)
            {
                ModelState.AddModelError("", "Mật khẩu không được để trống");
                return View();
            }
            if (cfPassword == null)
            {
                ModelState.AddModelError("", "Mật khẩu không được để trống");
                return View();
            }
            if(nhanVien.Password == oldPassword)
                {
                    if (newPassword == cfPassword)
                    {
                        nhanVien.Password = newPassword;
                        _context.Update(nhanVien);
                        var check = _context.SaveChanges();
                        if (check > 0)
                        {
                            return RedirectToAction("Details");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lỗi lưu dữ liệu");
                            return View();
                        }

                    } else
                    {
                        ModelState.AddModelError("", "Mật khẩu nhập lại không khớp");
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không chính xác");
                    return View();
                }
            } else return RedirectToAction("Login", "Home");
        }

        private bool TblTtnhanVienExists(int id)
        {
            return _context.TblTtnhanViens.Any(e => e.MaNv == id);
        }
    }
}
