using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DA_QLNhanSu.Models;

namespace DA_QLNhanSu.Controllers
{
    public class LuongController : Controller
    {
        private readonly QLNhanSuContext _context;

        public LuongController(QLNhanSuContext context)
        {
            _context = context;
        }

        // GET: Luong
        public async Task<IActionResult> Index()
        {
            var qLNhanSuContext = _context.TblLuongs.Include(t => t.MaCmNavigation).Include(t => t.MaDvNavigation).Include(t => t.MaTdNavigation);
            return View(await qLNhanSuContext.ToListAsync());
        }

        // GET: Luong/Details/:id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLuong = await _context.TblLuongs
                .Include(t => t.MaCmNavigation)
                .Include(t => t.MaDvNavigation)
                .Include(t => t.MaTdNavigation)
                .FirstOrDefaultAsync(m => m.MaLuong == id);
            if (tblLuong == null)
            {
                return NotFound();
            }

            return View(tblLuong);
        }

        // GET: Luong/Create
        public IActionResult Create()
        {
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm");
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "TenDv");
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo");
            return View();
        }

        // POST: Luong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLuong,MaCm,MaTd,MaDv,LuongCoBan,LuongCbmoi,NgayNhap,NgaySua,GhiChu")] TblLuong tblLuong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblLuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm", tblLuong.MaCm);
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "TenDv", tblLuong.MaDv);
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo", tblLuong.MaTd);
            return View(tblLuong);
        }

        // GET: Luong/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLuong = await _context.TblLuongs.FindAsync(id);
            if (tblLuong == null)
            {
                return NotFound();
            }
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm", tblLuong.MaCm);
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "TenDv", tblLuong.MaDv);
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo", tblLuong.MaTd);
            return View(tblLuong);
        }

        // POST: Luong/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLuong,MaCm,MaTd,MaDv,LuongCoBan,LuongCbmoi,NgayNhap,NgaySua,GhiChu")] TblLuong tblLuong)
        {
            if (id != tblLuong.MaLuong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblLuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblLuongExists(tblLuong.MaLuong))
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
            ViewData["MaCm"] = new SelectList(_context.TblChuyenMons, "MaCm", "TenCm", tblLuong.MaCm);
            ViewData["MaDv"] = new SelectList(_context.TblDonVis, "MaDv", "TenDv", tblLuong.MaDv);
            ViewData["MaTd"] = new SelectList(_context.TblTrinhDos, "MaTd", "TenTrinhDo", tblLuong.MaTd);
            return View(tblLuong);
        }

        // GET: Luong/Delete/:id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLuong = await _context.TblLuongs
                .Include(t => t.MaCmNavigation)
                .Include(t => t.MaDvNavigation)
                .Include(t => t.MaTdNavigation)
                .FirstOrDefaultAsync(m => m.MaLuong == id);
            if (tblLuong == null)
            {
                return NotFound();
            }

            return View(tblLuong);
        }

        // POST: Luong/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblLuong = await _context.TblLuongs.FindAsync(id);
            _context.TblLuongs.Remove(tblLuong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblLuongExists(int id)
        {
            return _context.TblLuongs.Any(e => e.MaLuong == id);
        }
    }
}
