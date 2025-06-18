using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Slasticarna.Data;
using Slasticarna.Models;

namespace Slasticarna.Controllers
{
    public class StanjeNarudzbeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StanjeNarudzbeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StanjeNarudzbe
        public async Task<IActionResult> Index()
        {
              return _context.StanjaNarudzbi != null ? 
                          View(await _context.StanjaNarudzbi.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.StanjaNarudzbi'  is null.");
        }

        // GET: StanjeNarudzbe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StanjaNarudzbi == null)
            {
                return NotFound();
            }

            var stanjeNarudzbe = await _context.StanjaNarudzbi
                .FirstOrDefaultAsync(m => m.StanjeNarudzbeID == id);
            if (stanjeNarudzbe == null)
            {
                return NotFound();
            }

            return View(stanjeNarudzbe);
        }

        // GET: StanjeNarudzbe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StanjeNarudzbe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StanjeNarudzbeID,Naziv")] StanjeNarudzbe stanjeNarudzbe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stanjeNarudzbe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stanjeNarudzbe);
        }

        // GET: StanjeNarudzbe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StanjaNarudzbi == null)
            {
                return NotFound();
            }

            var stanjeNarudzbe = await _context.StanjaNarudzbi.FindAsync(id);
            if (stanjeNarudzbe == null)
            {
                return NotFound();
            }
            return View(stanjeNarudzbe);
        }

        // POST: StanjeNarudzbe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StanjeNarudzbeID,Naziv")] StanjeNarudzbe stanjeNarudzbe)
        {
            if (id != stanjeNarudzbe.StanjeNarudzbeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stanjeNarudzbe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StanjeNarudzbeExists(stanjeNarudzbe.StanjeNarudzbeID))
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
            return View(stanjeNarudzbe);
        }

        // GET: StanjeNarudzbe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StanjaNarudzbi == null)
            {
                return NotFound();
            }

            var stanjeNarudzbe = await _context.StanjaNarudzbi
                .FirstOrDefaultAsync(m => m.StanjeNarudzbeID == id);
            if (stanjeNarudzbe == null)
            {
                return NotFound();
            }

            return View(stanjeNarudzbe);
        }

        // POST: StanjeNarudzbe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StanjaNarudzbi == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StanjaNarudzbi'  is null.");
            }
            var stanjeNarudzbe = await _context.StanjaNarudzbi.FindAsync(id);
            if (stanjeNarudzbe != null)
            {
                _context.StanjaNarudzbi.Remove(stanjeNarudzbe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StanjeNarudzbeExists(int id)
        {
          return (_context.StanjaNarudzbi?.Any(e => e.StanjeNarudzbeID == id)).GetValueOrDefault();
        }
    }
}
