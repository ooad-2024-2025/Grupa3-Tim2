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
    public class NarudzbaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NarudzbaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Narudzba
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Narudzbe.Include(n => n.Korisnik).Include(n => n.Stanje);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Narudzba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Narudzbe == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe
                .Include(n => n.Korisnik)
                .Include(n => n.Stanje)
                .FirstOrDefaultAsync(m => m.NarudzbaID == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // GET: Narudzba/Create
        public IActionResult Create()
        {
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id");
            ViewData["StanjeNarudzbe"] = new SelectList(_context.StanjaNarudzbi, "StanjeNarudzbeID", "StanjeNarudzbeID");
            return View();
        }

        // POST: Narudzba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NarudzbaID,UkupnaCijena,Datum,Vrijeme,KorisnikID,StanjeNarudzbe")] Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(narudzba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", narudzba.KorisnikID);
            ViewData["StanjeNarudzbe"] = new SelectList(_context.StanjaNarudzbi, "StanjeNarudzbeID", "StanjeNarudzbeID", narudzba.StanjeNarudzbe);
            return View(narudzba);
        }

        // GET: Narudzba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Narudzbe == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe.FindAsync(id);
            if (narudzba == null)
            {
                return NotFound();
            }
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", narudzba.KorisnikID);
            ViewData["StanjeNarudzbe"] = new SelectList(_context.StanjaNarudzbi, "StanjeNarudzbeID", "StanjeNarudzbeID", narudzba.StanjeNarudzbe);
            return View(narudzba);
        }

        // POST: Narudzba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NarudzbaID,UkupnaCijena,Datum,Vrijeme,KorisnikID,StanjeNarudzbe")] Narudzba narudzba)
        {
            if (id != narudzba.NarudzbaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.NarudzbaID))
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
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", narudzba.KorisnikID);
            ViewData["StanjeNarudzbe"] = new SelectList(_context.StanjaNarudzbi, "StanjeNarudzbeID", "StanjeNarudzbeID", narudzba.StanjeNarudzbe);
            return View(narudzba);
        }

        // GET: Narudzba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Narudzbe == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzbe
                .Include(n => n.Korisnik)
                .Include(n => n.Stanje)
                .FirstOrDefaultAsync(m => m.NarudzbaID == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // POST: Narudzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Narudzbe == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Narudzbe'  is null.");
            }
            var narudzba = await _context.Narudzbe.FindAsync(id);
            if (narudzba != null)
            {
                _context.Narudzbe.Remove(narudzba);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudzbaExists(int id)
        {
          return (_context.Narudzbe?.Any(e => e.NarudzbaID == id)).GetValueOrDefault();
        }
    }
}
