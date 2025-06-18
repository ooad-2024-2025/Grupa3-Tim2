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
    public class OcjenaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OcjenaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ocjena
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ocjene.Include(o => o.Korisnik).Include(o => o.Narudzba);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ocjena/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ocjene == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjene
                .Include(o => o.Korisnik)
                .Include(o => o.Narudzba)
                .FirstOrDefaultAsync(m => m.OcjenaID == id);
            if (ocjena == null)
            {
                return NotFound();
            }

            return View(ocjena);
        }

        // GET: Ocjena/Create
        public IActionResult Create()
        {
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id");
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID");
            return View();
        }

        // POST: Ocjena/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OcjenaID,Value,NarudzbaID,KorisnikID")] Ocjena ocjena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocjena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", ocjena.KorisnikID);
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", ocjena.NarudzbaID);
            return View(ocjena);
        }

        // GET: Ocjena/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ocjene == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjene.FindAsync(id);
            if (ocjena == null)
            {
                return NotFound();
            }
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", ocjena.KorisnikID);
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", ocjena.NarudzbaID);
            return View(ocjena);
        }

        // POST: Ocjena/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OcjenaID,Value,NarudzbaID,KorisnikID")] Ocjena ocjena)
        {
            if (id != ocjena.OcjenaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocjena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcjenaExists(ocjena.OcjenaID))
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
            ViewData["KorisnikID"] = new SelectList(_context.AppUsers, "Id", "Id", ocjena.KorisnikID);
            ViewData["NarudzbaID"] = new SelectList(_context.Narudzbe, "NarudzbaID", "KorisnikID", ocjena.NarudzbaID);
            return View(ocjena);
        }

        // GET: Ocjena/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ocjene == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjene
                .Include(o => o.Korisnik)
                .Include(o => o.Narudzba)
                .FirstOrDefaultAsync(m => m.OcjenaID == id);
            if (ocjena == null)
            {
                return NotFound();
            }

            return View(ocjena);
        }

        // POST: Ocjena/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ocjene == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ocjene'  is null.");
            }
            var ocjena = await _context.Ocjene.FindAsync(id);
            if (ocjena != null)
            {
                _context.Ocjene.Remove(ocjena);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcjenaExists(int id)
        {
          return (_context.Ocjene?.Any(e => e.OcjenaID == id)).GetValueOrDefault();
        }
    }
}
