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
    public class NacinPlacanjaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NacinPlacanjaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NacinPlacanja
        public async Task<IActionResult> Index()
        {
              return _context.NaciniPlacanja != null ? 
                          View(await _context.NaciniPlacanja.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.NaciniPlacanja'  is null.");
        }

        // GET: NacinPlacanja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NaciniPlacanja == null)
            {
                return NotFound();
            }

            var nacinPlacanja = await _context.NaciniPlacanja
                .FirstOrDefaultAsync(m => m.NacinPlacanjaID == id);
            if (nacinPlacanja == null)
            {
                return NotFound();
            }

            return View(nacinPlacanja);
        }

        // GET: NacinPlacanja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NacinPlacanja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NacinPlacanjaID,Naziv")] NacinPlacanja nacinPlacanja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nacinPlacanja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nacinPlacanja);
        }

        // GET: NacinPlacanja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NaciniPlacanja == null)
            {
                return NotFound();
            }

            var nacinPlacanja = await _context.NaciniPlacanja.FindAsync(id);
            if (nacinPlacanja == null)
            {
                return NotFound();
            }
            return View(nacinPlacanja);
        }

        // POST: NacinPlacanja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NacinPlacanjaID,Naziv")] NacinPlacanja nacinPlacanja)
        {
            if (id != nacinPlacanja.NacinPlacanjaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nacinPlacanja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NacinPlacanjaExists(nacinPlacanja.NacinPlacanjaID))
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
            return View(nacinPlacanja);
        }

        // GET: NacinPlacanja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NaciniPlacanja == null)
            {
                return NotFound();
            }

            var nacinPlacanja = await _context.NaciniPlacanja
                .FirstOrDefaultAsync(m => m.NacinPlacanjaID == id);
            if (nacinPlacanja == null)
            {
                return NotFound();
            }

            return View(nacinPlacanja);
        }

        // POST: NacinPlacanja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NaciniPlacanja == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NaciniPlacanja'  is null.");
            }
            var nacinPlacanja = await _context.NaciniPlacanja.FindAsync(id);
            if (nacinPlacanja != null)
            {
                _context.NaciniPlacanja.Remove(nacinPlacanja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NacinPlacanjaExists(int id)
        {
          return (_context.NaciniPlacanja?.Any(e => e.NacinPlacanjaID == id)).GetValueOrDefault();
        }
    }
}
