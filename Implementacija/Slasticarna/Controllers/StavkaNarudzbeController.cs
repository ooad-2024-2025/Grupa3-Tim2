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
    public class StavkaNarudzbeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StavkaNarudzbeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StavkaNarudzbe
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StavkeNarudzbe.Include(s => s.Narudzba).Include(s => s.Proizvod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StavkaNarudzbe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StavkeNarudzbe == null)
            {
                return NotFound();
            }

            var stavkaNarudzbe = await _context.StavkeNarudzbe
                .Include(s => s.Narudzba)
                .Include(s => s.Proizvod)
                .FirstOrDefaultAsync(m => m.StavkaID == id);
            if (stavkaNarudzbe == null)
            {
                return NotFound();
            }

            return View(stavkaNarudzbe);
        }

        // GET: StavkaNarudzbe/Create
        public IActionResult Create()
        {
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID");
            ViewData["ProizvodID"] = new SelectList(_context.Proizvodi, "ProizvodID", "Naziv");
            return View();
        }

        // POST: StavkaNarudzbe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StavkaID,Kolicina,NarudzbaID,ProizvodID")] StavkaNarudzbe stavkaNarudzbe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stavkaNarudzbe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", stavkaNarudzbe.NarudzbaID);
            ViewData["ProizvodID"] = new SelectList(_context.Proizvodi, "ProizvodID", "Naziv", stavkaNarudzbe.ProizvodID);
            return View(stavkaNarudzbe);
        }

        // GET: StavkaNarudzbe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StavkeNarudzbe == null)
            {
                return NotFound();
            }

            var stavkaNarudzbe = await _context.StavkeNarudzbe.FindAsync(id);
            if (stavkaNarudzbe == null)
            {
                return NotFound();
            }
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", stavkaNarudzbe.NarudzbaID);
            ViewData["ProizvodID"] = new SelectList(_context.Proizvodi, "ProizvodID", "Naziv", stavkaNarudzbe.ProizvodID);
            return View(stavkaNarudzbe);
        }

        // POST: StavkaNarudzbe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StavkaID,Kolicina,NarudzbaID,ProizvodID")] StavkaNarudzbe stavkaNarudzbe)
        {
            if (id != stavkaNarudzbe.StavkaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stavkaNarudzbe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StavkaNarudzbeExists(stavkaNarudzbe.StavkaID))
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
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", stavkaNarudzbe.NarudzbaID);
            ViewData["ProizvodID"] = new SelectList(_context.Proizvodi, "ProizvodID", "Naziv", stavkaNarudzbe.ProizvodID);
            return View(stavkaNarudzbe);
        }

        // GET: StavkaNarudzbe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StavkeNarudzbe == null)
            {
                return NotFound();
            }

            var stavkaNarudzbe = await _context.StavkeNarudzbe
                .Include(s => s.Narudzba)
                .Include(s => s.Proizvod)
                .FirstOrDefaultAsync(m => m.StavkaID == id);
            if (stavkaNarudzbe == null)
            {
                return NotFound();
            }

            return View(stavkaNarudzbe);
        }

        // POST: StavkaNarudzbe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StavkeNarudzbe == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StavkeNarudzbe'  is null.");
            }
            var stavkaNarudzbe = await _context.StavkeNarudzbe.FindAsync(id);
            if (stavkaNarudzbe != null)
            {
                _context.StavkeNarudzbe.Remove(stavkaNarudzbe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StavkaNarudzbeExists(int id)
        {
          return (_context.StavkeNarudzbe?.Any(e => e.StavkaID == id)).GetValueOrDefault();
        }
    }
}
