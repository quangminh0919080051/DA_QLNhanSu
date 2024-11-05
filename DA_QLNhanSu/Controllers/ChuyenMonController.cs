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
    public class ChuyenMonController : Controller
    {
        private readonly QLNhanSuContext _context;

        public ChuyenMonController(QLNhanSuContext context)
        {
            _context = context;
        }

        // GET: ChuyenMon
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblChuyenMons.ToListAsync());
        }

        // GET: ChuyenMon/Details/:id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChuyenMon = await _context.TblChuyenMons
                .FirstOrDefaultAsync(m => m.MaCm == id);
            if (tblChuyenMon == null)
            {
                return NotFound();
            }

            return View(tblChuyenMon);
        }

        // GET: ChuyenMon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChuyenMon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCm,TenCm,GhiChu")] TblChuyenMon tblChuyenMon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblChuyenMon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblChuyenMon);
        }

        // GET: ChuyenMon/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChuyenMon = await _context.TblChuyenMons.FindAsync(id);
            if (tblChuyenMon == null)
            {
                return NotFound();
            }
            return View(tblChuyenMon);
        }

        // POST: ChuyenMon/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCm,TenCm,GhiChu")] TblChuyenMon tblChuyenMon)
        {
            if (id != tblChuyenMon.MaCm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblChuyenMon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblChuyenMonExists(tblChuyenMon.MaCm))
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
            return View(tblChuyenMon);
        }

        // GET: ChuyenMon/Delete/:id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChuyenMon = await _context.TblChuyenMons
                .FirstOrDefaultAsync(m => m.MaCm == id);
            if (tblChuyenMon == null)
            {
                return NotFound();
            }

            return View(tblChuyenMon);
        }

        // POST: ChuyenMon/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblChuyenMon = await _context.TblChuyenMons.FindAsync(id);
            _context.TblChuyenMons.Remove(tblChuyenMon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblChuyenMonExists(int id)
        {
            return _context.TblChuyenMons.Any(e => e.MaCm == id);
        }
    }
}
