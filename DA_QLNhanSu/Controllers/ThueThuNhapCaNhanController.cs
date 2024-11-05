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
    public class ThueThuNhapCaNhanController : Controller
    {
        private readonly QLNhanSuContext _context;

        public ThueThuNhapCaNhanController(QLNhanSuContext context)
        {
            _context = context;
        }

        // GET: ThueThuNhapCaNhan
        public async Task<IActionResult> Index()
        {
            var qLNhanSuContext = _context.TblThueThuNhapCaNhans.Include(t => t.MaLuongNavigation);
            return View(await qLNhanSuContext.ToListAsync());
        }

        // GET: ThueThuNhapCaNhan/Details/:id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblThueThuNhapCaNhan = await _context.TblThueThuNhapCaNhans
                .Include(t => t.MaLuongNavigation)
                .FirstOrDefaultAsync(m => m.MaThue == id);
            if (tblThueThuNhapCaNhan == null)
            {
                return NotFound();
            }

            return View(tblThueThuNhapCaNhan);
        }

        // GET: ThueThuNhapCaNhan/Create
        public IActionResult Create()
        {
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "LuongCoBan");
            return View();
        }

        // POST: ThueThuNhapCaNhan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThue,CoQuanQuanLyThue,MaLuong,SoTien,NgayDangKi,GhiChu")] TblThueThuNhapCaNhan tblThueThuNhapCaNhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblThueThuNhapCaNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "MaLuong", tblThueThuNhapCaNhan.MaLuong);
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "MaLuong", tblThueThuNhapCaNhan.MaLuong);
            return View(tblThueThuNhapCaNhan);
        }

        // GET: ThueThuNhapCaNhan/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblThueThuNhapCaNhan = await _context.TblThueThuNhapCaNhans.FindAsync(id);
            if (tblThueThuNhapCaNhan == null)
            {
                return NotFound();
            }
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "MaLuong", tblThueThuNhapCaNhan.MaLuong);
            return View(tblThueThuNhapCaNhan);
        }

        // POST: ThueThuNhapCaNhan/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThue,CoQuanQuanLyThue,MaLuong,SoTien,NgayDangKi,GhiChu")] TblThueThuNhapCaNhan tblThueThuNhapCaNhan)
        {
            if (id != tblThueThuNhapCaNhan.MaThue)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblThueThuNhapCaNhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblThueThuNhapCaNhanExists(tblThueThuNhapCaNhan.MaThue))
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
            ViewData["MaLuong"] = new SelectList(_context.TblLuongs, "MaLuong", "MaLuong", tblThueThuNhapCaNhan.MaLuong);
            return View(tblThueThuNhapCaNhan);
        }

        // GET: ThueThuNhapCaNhan/Delete/:id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblThueThuNhapCaNhan = await _context.TblThueThuNhapCaNhans
                .Include(t => t.MaLuongNavigation)
                .FirstOrDefaultAsync(m => m.MaThue == id);
            if (tblThueThuNhapCaNhan == null)
            {
                return NotFound();
            }

            return View(tblThueThuNhapCaNhan);
        }

        // POST: ThueThuNhapCaNhan/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblThueThuNhapCaNhan = await _context.TblThueThuNhapCaNhans.FindAsync(id);
            _context.TblThueThuNhapCaNhans.Remove(tblThueThuNhapCaNhan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblThueThuNhapCaNhanExists(int id)
        {
            return _context.TblThueThuNhapCaNhans.Any(e => e.MaThue == id);
        }
    }
}
