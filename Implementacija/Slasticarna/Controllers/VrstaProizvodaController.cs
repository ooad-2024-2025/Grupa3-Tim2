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
    public class VrstaProizvodaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VrstaProizvodaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VrstaProizvoda
        public async Task<IActionResult> Index()
        {
              return _context.VrsteProizvoda != null ? 
                          View(await _context.VrsteProizvoda.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.VrsteProizvoda'  is null.");
        }

        // GET: VrstaProizvoda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VrsteProizvoda == null)
            {
                return NotFound();
            }

            var vrstaProizvoda = await _context.VrsteProizvoda
                .FirstOrDefaultAsync(m => m.VrstaProizvodaID == id);
            if (vrstaProizvoda == null)
            {
                return NotFound();
            }

            return View(vrstaProizvoda);
        }

        // GET: VrstaProizvoda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VrstaProizvoda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VrstaProizvodaID,Naziv")] VrstaProizvoda vrstaProizvoda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vrstaProizvoda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vrstaProizvoda);
        }

        // GET: VrstaProizvoda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VrsteProizvoda == null)
            {
                return NotFound();
            }

            var vrstaProizvoda = await _context.VrsteProizvoda.FindAsync(id);
            if (vrstaProizvoda == null)
            {
                return NotFound();
            }
            return View(vrstaProizvoda);
        }

        // POST: VrstaProizvoda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VrstaProizvodaID,Naziv")] VrstaProizvoda vrstaProizvoda)
        {
            if (id != vrstaProizvoda.VrstaProizvodaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vrstaProizvoda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VrstaProizvodaExists(vrstaProizvoda.VrstaProizvodaID))
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
            return View(vrstaProizvoda);
        }

        // GET: VrstaProizvoda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VrsteProizvoda == null)
            {
                return NotFound();
            }

            var vrstaProizvoda = await _context.VrsteProizvoda
                .FirstOrDefaultAsync(m => m.VrstaProizvodaID == id);
            if (vrstaProizvoda == null)
            {
                return NotFound();
            }

            return View(vrstaProizvoda);
        }

        // POST: VrstaProizvoda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VrsteProizvoda == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VrsteProizvoda'  is null.");
            }
            var vrstaProizvoda = await _context.VrsteProizvoda.FindAsync(id);
            if (vrstaProizvoda != null)
            {
                _context.VrsteProizvoda.Remove(vrstaProizvoda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VrstaProizvodaExists(int id)
        {
          return (_context.VrsteProizvoda?.Any(e => e.VrstaProizvodaID == id)).GetValueOrDefault();
        }
    }
}
