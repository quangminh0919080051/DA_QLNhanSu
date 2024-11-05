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
    public class TrinhDoController : Controller
    {
        private readonly QLNhanSuContext _context;

        public TrinhDoController(QLNhanSuContext context)
        {
            _context = context;
        }

        // GET: TrinhDo
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblTrinhDos.ToListAsync());
        }

        // GET: TrinhDo/Details/:id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTrinhDo = await _context.TblTrinhDos
                .FirstOrDefaultAsync(m => m.MaTd == id);
            if (tblTrinhDo == null)
            {
                return NotFound();
            }

            return View(tblTrinhDo);
        }

        // GET: TrinhDo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrinhDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTd,TenTrinhDo,GhiChu")] TblTrinhDo tblTrinhDo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblTrinhDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblTrinhDo);
        }

        // GET: TrinhDo/Edit/:id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTrinhDo = await _context.TblTrinhDos.FindAsync(id);
            if (tblTrinhDo == null)
            {
                return NotFound();
            }
            return View(tblTrinhDo);
        }

        // POST: TrinhDo/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTd,TenTrinhDo,GhiChu")] TblTrinhDo tblTrinhDo)
        {
            if (id != tblTrinhDo.MaTd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblTrinhDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTrinhDoExists(tblTrinhDo.MaTd))
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
            return View(tblTrinhDo);
        }

        // GET: TrinhDo/Delete/:id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTrinhDo = await _context.TblTrinhDos
                .FirstOrDefaultAsync(m => m.MaTd == id);
            if (tblTrinhDo == null)
            {
                return NotFound();
            }

            return View(tblTrinhDo);
        }

        // POST: TrinhDo/Delete/:id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblTrinhDo = await _context.TblTrinhDos.FindAsync(id);
            _context.TblTrinhDos.Remove(tblTrinhDo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblTrinhDoExists(int id)
        {
            return _context.TblTrinhDos.Any(e => e.MaTd == id);
        }
    }
}
