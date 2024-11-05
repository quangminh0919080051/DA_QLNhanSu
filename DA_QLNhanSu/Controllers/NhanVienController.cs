using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DA_QLNhanSu.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;

namespace DA_QLNhanSu.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly QLNhanSuContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NhanVienController(QLNhanSuContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: NhanVien
        public async Task<IActionResult> Index()
        {
            var qLNhanSuContext = _context.TblTtnhanViens.Include(t => t.MaBhxhNavigation).Include(t => t.MaCmNavigation).Include(t => t.MaDvNavigation).Include(t => t.MaLuongNavigation).Include(t => t.MaTdNavigation).Include(t => t.MaThueNavigation);
            return View(await qLNhanSuContext.ToListAsync());
        }

        // GET: NhanVien
        public async Task<IActionResult> PhanQuyen()
        {
            var qLNhanSuContext = _context.TblTtnhanViens.Include(t => t.MaBhxhNavigation).Include(t => t.MaCmNavigation).Include(t => t.MaDvNavigation).Include(t => t.MaLuongNavigation).Include(t => t.MaTdNavigation).Include(t => t.MaThueNavigation);
            return View(await qLNhanSuContext.ToListAsync());
        }


        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTtnhanVien = await _context.TblTtnhanViens
                .Include(t => t.MaBhxhNavigation)
                .Include(t => t.MaCmNavigation)
                .Include(t => t.MaDvNavigation)
                .Include(t => t.MaLuongNavigation)
                .Include(t => t.MaTdNavigation)
                .Include(t => t.MaThueNavigation)
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (tblTtnhanVien == null)
            {
                return NotFound();
            }

            return View(tblTtnhanVien);
        }

        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ Entity Framework
            var data = await _context.TblTtnhanViens
                    .Include(t => t.MaBhxhNavigation)
                    .Include(t => t.MaCmNavigation)
                    .Include(t => t.MaDvNavigation)
                    .Include(t => t.MaLuongNavigation)
                    .Include(t => t.MaTdNavigation)
                    .Include(t => t.MaThueNavigation)
                    .ToListAsync();

            // Tạo một file Excel mới
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            // Tạo một worksheet mới
            var worksheet = package.Workbook.Worksheets.Add("Customers");

            // Thêm tiêu đề cho các cột
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Mã NV";
            worksheet.Cells[1, 3].Value = "Họ Tên";
            worksheet.Cells[1, 4].Value = "Email";
            worksheet.Cells[1, 5].Value = "Phone";
            worksheet.Cells[1, 6].Value = "Ngày Sinh";
            worksheet.Cells[1, 7].Value = "Giới Tính";
            worksheet.Cells[1, 8].Value = "CMND";
            worksheet.Cells[1, 9].Value = "Nơi Sinh";
            worksheet.Cells[1, 10].Value = "Địa Chỉ";
            worksheet.Cells[1, 11].Value = "Dân Tộc";
            worksheet.Cells[1, 12].Value = "Tôn Giáo";
            worksheet.Cells[1, 13].Value = "Quốc Tịch";
            worksheet.Cells[1, 14].Value = "Trình Độ";
            worksheet.Cells[1, 15].Value = "Chuyên Môn";
            worksheet.Cells[1, 16].Value = "Đơn Vị";
            worksheet.Cells[1, 17].Value = "Nơi Đăng Ký BH";
            worksheet.Cells[1, 18].Value = "Cơ Quan QL Thuế";
            worksheet.Cells[1, 19].Value = "Lương Cơ Bản";
            worksheet.Cells[1, 20].Value = "Ghi Chú";

            // Thêm dữ liệu vào các cột
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = i + 1;
                worksheet.Cells[i + 2, 2].Value = data[i].MaNv;
                worksheet.Cells[i + 2, 3].Value = data[i].HoTen;
                worksheet.Cells[i + 2, 4].Value = data[i].Email;
                worksheet.Cells[i + 2, 5].Value = data[i].Sdt;
                worksheet.Cells[i + 2, 6].Value = data[i].NgaySinh;
                worksheet.Cells[i + 2, 7].Value = data[i].GioiTinh;
                worksheet.Cells[i + 2, 8].Value = data[i].Cmnd;
                worksheet.Cells[i + 2, 9].Value = data[i].NoiSinh;
                worksheet.Cells[i + 2, 10].Value = data[i].DiaChi;
                worksheet.Cells[i + 2, 11].Value = data[i].DanToc;
                worksheet.Cells[i + 2, 12].Value = data[i].TonGiao;
                worksheet.Cells[i + 2, 13].Value = data[i].QuocTich;
                worksheet.Cells[i + 2, 14].Value = data[i].MaTdNavigation.TenTrinhDo;
                worksheet.Cells[i + 2, 15].Value = data[i].MaCmNavigation.TenCm;
                worksheet.Cells[i + 2, 16].Value = data[i].MaDvNavigation.TenDv;
                worksheet.Cells[i + 2, 17].Value = data[i].MaBhxhNavigation.NoiDkkcb;
                worksheet.Cells[i + 2, 18].Value = data[i].MaThueNavigation.CoQuanQuanLyThue;
                worksheet.Cells[i + 2, 19].Value = data[i].MaLuongNavigation.LuongCoBan;
                worksheet.Cells[i + 2, 20].Value = data[i].GhiChu;
            }

            // Save file Excel
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Trả về file Excel như một phản hồi HTTP
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NhanVien_All.xlsx");
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            ViewData["MaBhxh"] = new SelectList(_context.TblBaoHiemXhs, "MaBhxh", "NoiDkkcb");
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm");
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "TenDv");
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "LuongCoBan");
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo");
            ViewData["MaThue"] = new SelectList(_context.TblThueThuNhapCaNhans, "MaThue", "CoQuanQuanLyThue");
            return View();
        }

        // POST: NhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblTtnhanVien tblTtnhanVien)
        {
            if (ModelState.IsValid)
            {
                if (tblTtnhanVien.ConvertPhoto != null)
                {
                    string folder = "images/avatar";
                    folder += Guid.NewGuid().ToString() + "_" + tblTtnhanVien.ConvertPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await tblTtnhanVien.ConvertPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    tblTtnhanVien.Anh = $"/{folder}";
                }
                _context.Add(tblTtnhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaBhxh"] = new SelectList(_context.TblBaoHiemXhs, "MaBhxh", "MaBhxh", tblTtnhanVien.MaBhxh);
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm", tblTtnhanVien.MaCm);
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "MaDv", tblTtnhanVien.MaDv);
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "MaLuong", tblTtnhanVien.MaLuong);
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo", tblTtnhanVien.MaTd);
            ViewData["MaThue"] = new SelectList(_context.TblThueThuNhapCaNhans, "MaThue", "MaThue", tblTtnhanVien.MaThue);
            return View(tblTtnhanVien);
        }

        // GET: NhanVien/Edit/5
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
            ViewData["MaBhxh"] = new SelectList(_context.TblBaoHiemXhs, "MaBhxh", "NoiDkkcb", tblTtnhanVien.MaBhxh);
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm", tblTtnhanVien.MaCm);
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "TenDv", tblTtnhanVien.MaDv);
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "LuongCoBan", tblTtnhanVien.MaLuong);
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo", tblTtnhanVien.MaTd);
            ViewData["MaThue"] = new SelectList(_context.TblThueThuNhapCaNhans, "MaThue", "CoQuanQuanLyThue", tblTtnhanVien.MaThue);
            return View(tblTtnhanVien);
        }

        // POST: NhanVien/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  TblTtnhanVien tblTtnhanVien)
        {
            if (id != tblTtnhanVien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (tblTtnhanVien.ConvertPhoto != null)
                {
                    string folder = "images/avatar";
                    folder += Guid.NewGuid().ToString() + "_" + tblTtnhanVien.ConvertPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await tblTtnhanVien.ConvertPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaBhxh"] = new SelectList(_context.TblBaoHiemXhs, "MaBhxh", "MaBhxh", tblTtnhanVien.MaBhxh);
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm", tblTtnhanVien.MaCm);
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "MaDv", tblTtnhanVien.MaDv);
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "MaLuong", tblTtnhanVien.MaLuong);
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo", tblTtnhanVien.MaTd);
            ViewData["MaThue"] = new SelectList(_context.TblThueThuNhapCaNhans, "MaThue", "MaThue", tblTtnhanVien.MaThue);
            return View(tblTtnhanVien);
        }

        // GET: NhanVien/Edit/5
        


        // GET: NhanVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTtnhanVien = await _context.TblTtnhanViens
                .Include(t => t.MaBhxhNavigation)
                .Include(t => t.MaCmNavigation)
                .Include(t => t.MaDvNavigation)
                .Include(t => t.MaLuongNavigation)
                .Include(t => t.MaTdNavigation)
                .Include(t => t.MaThueNavigation)
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (tblTtnhanVien == null)
            {
                return NotFound();
            }

            return View(tblTtnhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblTtnhanVien = await _context.TblTtnhanViens.FindAsync(id);
            _context.TblTtnhanViens.Remove(tblTtnhanVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool TblTtnhanVienExists(int id)
        {
            return _context.TblTtnhanViens.Any(e => e.MaNv == id);
        }
    }
}
