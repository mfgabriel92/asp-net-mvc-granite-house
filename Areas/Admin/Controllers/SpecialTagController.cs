using System;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialTagController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.SpecialTag.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if (!ModelState.IsValid)
            {
                return View(specialTag);
            }

            _context.Add(specialTag);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = await _context.SpecialTag.FindAsync(id);

            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTag specialTag)
        {
            if (id != specialTag.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(specialTag);
            }

            _context.Update(specialTag);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = await _context.SpecialTag.FindAsync(id);

            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var specialTag = await _context.SpecialTag.FindAsync(id);

            if (specialTag == null)
            {
                return NotFound();
            }

            _context.SpecialTag.Remove(specialTag);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}