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
    public class DonViController : Controller
    {
        private readonly QLNhanSuContext _context;

        public DonViController(QLNhanSuContext context)
        {
            _context = context;
        }

        // GET: DonVi
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblDonVis.ToListAsync());
        }

        // GET: DonVi/Details/:id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDonVi = await _context.TblDonVis
                .FirstOrDefaultAsync(m => m.MaDv == id);
            if (tblDonVi == null)
            {
                return NotFound();
            }

            return View(tblDonVi);
        }

        // GET: DonVi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonVi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDv,TenDv,GhiChu")] TblDonVi tblDonVi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblDonVi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblDonVi);
        }

        // GET: DonVi/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDonVi = await _context.TblDonVis.FindAsync(id);
            if (tblDonVi == null)
            {
                return NotFound();
            }
            return View(tblDonVi);
        }

        // POST: DonVi/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDv,TenDv,GhiChu")] TblDonVi tblDonVi)
        {
            if (id != tblDonVi.MaDv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblDonVi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblDonViExists(tblDonVi.MaDv))
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
            return View(tblDonVi);
        }

        // GET: DonVi/Delete/:id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDonVi = await _context.TblDonVis
                .FirstOrDefaultAsync(m => m.MaDv == id);
            if (tblDonVi == null)
            {
                return NotFound();
            }

            return View(tblDonVi);
        }

        // POST: DonVi/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblDonVi = await _context.TblDonVis.FindAsync(id);
            _context.TblDonVis.Remove(tblDonVi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblDonViExists(int id)
        {
            return _context.TblDonVis.Any(e => e.MaDv == id);
        }
    }
}
