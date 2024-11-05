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
    public class BaoHiemXhController : Controller
    {
        private readonly QLNhanSuContext _context;

        public BaoHiemXhController(QLNhanSuContext context)
        {
            _context = context;
        }

        // GET: BaoHiemXh
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblBaoHiemXhs.ToListAsync());
        }

        // GET: BaoHiemXh/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBaoHiemXh = await _context.TblBaoHiemXhs
                .FirstOrDefaultAsync(m => m.MaBhxh == id);
            if (tblBaoHiemXh == null)
            {
                return NotFound();
            }

            return View(tblBaoHiemXh);
        }

        // GET: BaoHiemXh/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BaoHiemXh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBhxh,NgayCap,NoiCap,NoiDkkcb,GhiChu")] TblBaoHiemXh tblBaoHiemXh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblBaoHiemXh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblBaoHiemXh);
        }

        // GET: BaoHiemXh/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBaoHiemXh = await _context.TblBaoHiemXhs.FindAsync(id);
            if (tblBaoHiemXh == null)
            {
                return NotFound();
            }
            return View(tblBaoHiemXh);
        }

        // POST: BaoHiemXh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBhxh,NgayCap,NoiCap,NoiDkkcb,GhiChu")] TblBaoHiemXh tblBaoHiemXh)
        {
            if (id != tblBaoHiemXh.MaBhxh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblBaoHiemXh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBaoHiemXhExists(tblBaoHiemXh.MaBhxh))
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
            return View(tblBaoHiemXh);
        }

        // GET: BaoHiemXh/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBaoHiemXh = await _context.TblBaoHiemXhs
                .FirstOrDefaultAsync(m => m.MaBhxh == id);
            if (tblBaoHiemXh == null)
            {
                return NotFound();
            }

            return View(tblBaoHiemXh);
        }

        // POST: BaoHiemXh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblBaoHiemXh = await _context.TblBaoHiemXhs.FindAsync(id);
            _context.TblBaoHiemXhs.Remove(tblBaoHiemXh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblBaoHiemXhExists(int id)
        {
            return _context.TblBaoHiemXhs.Any(e => e.MaBhxh == id);
        }
    }
}
