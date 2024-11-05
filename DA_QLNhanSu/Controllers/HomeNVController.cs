using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DA_QLNhanSu.Models;
using Microsoft.AspNetCore.Http;

namespace DA_QLNhanSu.Controllers
{
    public class HomeNVController : Controller
    {
        private readonly QLNhanSuContext _context;

        public HomeNVController(QLNhanSuContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ThueCaNhan()
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

        public async Task<IActionResult> ThongKeLuongChiTiet(int? id)
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

        public async Task<IActionResult> ThongTinBaoHiem()
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
                .FirstOrDefaultAsync(m => m.MaDv == id);
            return View(user);
        }

        public async Task<IActionResult> ThongKeLuong()
        {
            var SessionUserId = HttpContext.Session.GetString("MaNV");
            var id = int.Parse(SessionUserId);
            var thongKeLuongs = await _context.TblThongKeLuongs.Where(tkl => tkl.MaNv == id)
                                .Include(t => t.MaNvNavigation)
                                .Include(t => t.MaThangNavigation)
                                .ToListAsync();
            return View(thongKeLuongs);
        }
    }
}
