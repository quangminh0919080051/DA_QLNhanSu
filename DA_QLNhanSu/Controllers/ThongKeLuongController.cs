using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DA_QLNhanSu.Models;
using OfficeOpenXml;
using System.IO;

namespace DA_QLNhanSu.Controllers
{
    public class ThongKeLuongController : Controller
    {
        private readonly QLNhanSuContext _context;

        public ThongKeLuongController(QLNhanSuContext context)
        {
            _context = context;
        }

        // GET: ThongKeLuong
        public async Task<IActionResult> Index()
        {
            var qLNhanSuContext = _context.TblThongKeLuongs.Include(t => t.MaNvNavigation).Include(t => t.MaThangNavigation);
            return View(await qLNhanSuContext.ToListAsync());
        }

        // GET: ThongKeLuong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblThongKeLuong = await _context.TblThongKeLuongs
                .Include(t => t.MaNvNavigation)
                .Include(t => t.MaThangNavigation)
                .FirstOrDefaultAsync(m => m.MaTkluong == id);
            if (tblThongKeLuong == null)
            {
                return NotFound();
            }

            return View(tblThongKeLuong);
        }

        // GET: ThongKeLuong/Create
        public IActionResult Create()
        {
            ViewData["MaNv"] = new SelectList(_context.TblTtnhanViens, "MaNv", "MaNv");
            ViewData["MaThang"] = new SelectList(_context.TblThangs, "MaThang", "TenThang");
            return View();
        }

        // POST: ThongKeLuong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblThongKeLuong tblThongKeLuong)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.TblTtnhanViens
                .Include(t => t.MaThueNavigation)
                .Include(t => t.MaLuongNavigation)
                .FirstOrDefaultAsync(m => m.MaNv == tblThongKeLuong.MaNv);
                if(user != null)
                {
                    if (tblThongKeLuong.Phat == null)
                    {
                        tblThongKeLuong.Phat = 0;
                    }
                    if (tblThongKeLuong.Thuong == null)
                    {
                        tblThongKeLuong.Thuong = 0;
                    }
                    tblThongKeLuong.LuongCoBan = user.MaLuongNavigation.LuongCoBan;
                    tblThongKeLuong.ThuePhaiDong = user.MaThueNavigation.SoTien;
                    tblThongKeLuong.TongLuong = ((tblThongKeLuong.LuongCoBan - tblThongKeLuong.ThuePhaiDong) + tblThongKeLuong.Thuong) - tblThongKeLuong.Phat;
                    tblThongKeLuong.NgayTao = DateTime.Now;
                    _context.Add(tblThongKeLuong);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewData["MaNv"] = new SelectList(_context.TblTtnhanViens, "MaNv", "MaNv", tblThongKeLuong.MaNv);
            ViewData["MaThang"] = new SelectList(_context.TblThangs, "MaThang", "TenThang", tblThongKeLuong.MaThang);
            return View(tblThongKeLuong);
        }

        // GET: ThongKeLuong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblThongKeLuong = await _context.TblThongKeLuongs.FindAsync(id);
            if (tblThongKeLuong == null)
            {
                return NotFound();
            }
            ViewData["MaNv"] = new SelectList(_context.TblTtnhanViens, "MaNv", "MaNv", tblThongKeLuong.MaNv);
            ViewData["MaThang"] = new SelectList(_context.TblThangs, "MaThang", "TenThang", tblThongKeLuong.MaThang);
            return View(tblThongKeLuong);
        }

        // POST: ThongKeLuong/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TblThongKeLuong tblThongKeLuong)
        {
            if (id != tblThongKeLuong.MaTkluong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     var user = await _context.TblTtnhanViens
                                .Include(t => t.MaThueNavigation)
                                .Include(t => t.MaLuongNavigation)
                                .FirstOrDefaultAsync(m => m.MaNv == tblThongKeLuong.MaNv);
                    if (tblThongKeLuong.Phat == null)
                    {
                        tblThongKeLuong.Phat = 0;
                    }
                    if (tblThongKeLuong.Thuong == null)
                    {
                        tblThongKeLuong.Thuong = 0;
                    }
                    tblThongKeLuong.LuongCoBan = user.MaLuongNavigation.LuongCoBan;
                    tblThongKeLuong.ThuePhaiDong = user.MaThueNavigation.SoTien;
                    tblThongKeLuong.TongLuong = ((tblThongKeLuong.LuongCoBan - tblThongKeLuong.ThuePhaiDong) + tblThongKeLuong.Thuong) - tblThongKeLuong.Phat;
                    _context.Update(tblThongKeLuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblThongKeLuongExists(tblThongKeLuong.MaTkluong))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNv"] = new SelectList(_context.TblTtnhanViens, "MaNv", "MaNv", tblThongKeLuong.MaNv);
            ViewData["MaThang"] = new SelectList(_context.TblThangs, "MaThang", "TenThang", tblThongKeLuong.MaThang);
            return View(tblThongKeLuong);
        }

        // GET: ThongKeLuong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblThongKeLuong = await _context.TblThongKeLuongs
                .Include(t => t.MaNvNavigation)
                .Include(t => t.MaThangNavigation)
                .FirstOrDefaultAsync(m => m.MaTkluong == id);
            if (tblThongKeLuong == null)
            {
                return NotFound();
            }

            return View(tblThongKeLuong);
        }

        // POST: ThongKeLuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblThongKeLuong = await _context.TblThongKeLuongs.FindAsync(id);
            _context.TblThongKeLuongs.Remove(tblThongKeLuong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExportToExcel(int MaThang)
        {
            var data = new List<TblThongKeLuong>();
            if(MaThang == 0)
            {
                // Lấy dữ liệu từ Entity Framework
               data = await _context.TblThongKeLuongs
                          .Include(t => t.MaNvNavigation)
                          .Include(t => t.MaThangNavigation)
                          .ToListAsync();
            }
            else if(MaThang != 0)
            {
                // Lấy dữ liệu từ Entity Framework
                data = await _context.TblThongKeLuongs
                           .Include(t => t.MaNvNavigation)
                           .Include(t => t.MaThangNavigation)
                           .Where(t => t.MaThang == MaThang)
                           .ToListAsync();
            }

            // Tạo một file Excel mới
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            // Tạo một worksheet mới
            var worksheet = package.Workbook.Worksheets.Add("Customers");

            // Thêm tiêu đề cho các cột
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Tháng";
            worksheet.Cells[1, 3].Value = "Mã NV";
            worksheet.Cells[1, 4].Value = "Tên NV";
            worksheet.Cells[1, 5].Value = "Email";
            worksheet.Cells[1, 6].Value = "Lương Cơ Bản";
            worksheet.Cells[1, 7].Value = "Thuế Phải Đóng";
            worksheet.Cells[1, 8].Value = "Thưởng";
            worksheet.Cells[1, 9].Value = "Thực Nhận";
            worksheet.Cells[1, 10].Value = "Ghi Chú";

            // Thêm dữ liệu vào các cột
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = i+1;
                worksheet.Cells[i + 2, 2].Value = data[i].MaThangNavigation.TenThang;
                worksheet.Cells[i + 2, 3].Value = data[i].MaNv;
                worksheet.Cells[i + 2, 4].Value = data[i].MaNvNavigation.HoTen;
                worksheet.Cells[i + 2, 5].Value = data[i].MaNvNavigation.Email;
                worksheet.Cells[i + 2, 6].Value = data[i].LuongCoBan;
                worksheet.Cells[i + 2, 7].Value = data[i].ThuePhaiDong;
                worksheet.Cells[i + 2, 8].Value = data[i].Thuong;
                worksheet.Cells[i + 2, 9].Value = data[i].TongLuong;
                worksheet.Cells[i + 2, 10].Value = data[i].GhiChu;
            }

            // Save file Excel
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Trả về file Excel như một phản hồi HTTP
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ThongKeLuong_All.xlsx");
        }

        private bool TblThongKeLuongExists(int id)
        {
            return _context.TblThongKeLuongs.Any(e => e.MaTkluong == id);
        }
    }
}
