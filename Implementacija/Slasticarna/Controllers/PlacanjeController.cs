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
    public class PlacanjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlacanjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Placanje
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Placanja.Include(p => p.Korisnik).Include(p => p.NacinPlacanja).Include(p => p.Narudzba).Include(p => p.Popust);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Placanje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Placanja == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanja
                .Include(p => p.Korisnik)
                .Include(p => p.NacinPlacanja)
                .Include(p => p.Narudzba)
                .Include(p => p.Popust)
                .FirstOrDefaultAsync(m => m.PlacanjeID == id);
            if (placanje == null)
            {
                return NotFound();
            }

            return View(placanje);
        }

        // GET: Placanje/Create
        public IActionResult Create()
        {
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id");
            ViewData["NacinPlacanjaID"] = new SelectList(_context.NaciniPlacanja, "NacinPlacanjaID", "NacinPlacanjaID");
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID");
            ViewData["PopustID"] = new SelectList(_context.Popusti, "PopustID", "PopustID");
            return View();
        }

        // POST: Placanje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlacanjeID,KorisnikID,PopustID,NarudzbaID,NacinPlacanjaID")] Placanje placanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", placanje.KorisnikID);
            ViewData["NacinPlacanjaID"] = new SelectList(_context.NaciniPlacanja, "NacinPlacanjaID", "NacinPlacanjaID", placanje.NacinPlacanjaID);
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", placanje.NarudzbaID);
            ViewData["PopustID"] = new SelectList(_context.Popusti, "PopustID", "PopustID", placanje.PopustID);
            return View(placanje);
        }

        // GET: Placanje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Placanja == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanja.FindAsync(id);
            if (placanje == null)
            {
                return NotFound();
            }
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", placanje.KorisnikID);
            ViewData["NacinPlacanjaID"] = new SelectList(_context.NaciniPlacanja, "NacinPlacanjaID", "NacinPlacanjaID", placanje.NacinPlacanjaID);
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", placanje.NarudzbaID);
            ViewData["PopustID"] = new SelectList(_context.Popusti, "PopustID", "PopustID", placanje.PopustID);
            return View(placanje);
        }

        // POST: Placanje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlacanjeID,KorisnikID,PopustID,NarudzbaID,NacinPlacanjaID")] Placanje placanje)
        {
            if (id != placanje.PlacanjeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacanjeExists(placanje.PlacanjeID))
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
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", placanje.KorisnikID);
            ViewData["NacinPlacanjaID"] = new SelectList(_context.NaciniPlacanja, "NacinPlacanjaID", "NacinPlacanjaID", placanje.NacinPlacanjaID);
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", placanje.NarudzbaID);
            ViewData["PopustID"] = new SelectList(_context.Popusti, "PopustID", "PopustID", placanje.PopustID);
            return View(placanje);
        }

        // GET: Placanje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Placanja == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanja
                .Include(p => p.Korisnik)
                .Include(p => p.NacinPlacanja)
                .Include(p => p.Narudzba)
                .Include(p => p.Popust)
                .FirstOrDefaultAsync(m => m.PlacanjeID == id);
            if (placanje == null)
            {
                return NotFound();
            }

            return View(placanje);
        }

        // POST: Placanje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Placanja == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Placanja'  is null.");
            }
            var placanje = await _context.Placanja.FindAsync(id);
            if (placanje != null)
            {
                _context.Placanja.Remove(placanje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacanjeExists(int id)
        {
          return (_context.Placanja?.Any(e => e.PlacanjeID == id)).GetValueOrDefault();
        }
    }
}
