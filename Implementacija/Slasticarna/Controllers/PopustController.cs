using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slasticarna.Data;
using Slasticarna.Models;
using Slasticarna.Models.ViewModels;

namespace Slasticarna.Controllers
{
    public class PopustController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PopustController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Popust
        public async Task<IActionResult> Index()
        {
            var popusti = await _context.Popusti
                .Include(p => p.PopustProizvodi)
                .ThenInclude(pp => pp.Proizvod)
                .ToListAsync();

            var viewModel = popusti.Select(p => new PopustViewModel
            {
                PopustID = p.PopustID,
                IznosPopusta = p.IznosPopusta,
                KodZaPopust = p.KodZaPopust
            }).ToList();

            return View(viewModel);
        }

        // GET: Popust/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Popust/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Popust popust)
        {
            if (ModelState.IsValid)
            {
                _context.Add(popust);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(popust);
        }
        // GET: Popust/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var popust = await _context.Popusti
                .Include(p => p.PopustProizvodi)   // Include PopustProizvodi to get the related products
                .ThenInclude(pp => pp.Proizvod)   // Include Proizvod details for each PopustProizvod
                .FirstOrDefaultAsync(m => m.PopustID == id);

            if (popust == null)
            {
                return NotFound();
            }

            // Prepare list of all products to show in the checkbox list
            ViewBag.AllProducts = await _context.Proizvodi.ToListAsync();

            // Prepare a list of product IDs that are currently assigned to this Popust
            var selectedProductIds = popust.PopustProizvodi.Select(pp => pp.ProizvodID).ToList();

            // Pass selected product IDs to the view
            ViewBag.SelectedProducts = selectedProductIds;

            return View(popust);
        }

        // POST: Popust/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PopustID, IznosPopusta, KodZaPopust")] Popust popust, int[] selectedProducts)
        {
            if (id != popust.PopustID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Remove old relationships
                    var existingPopustProizvodi = _context.PopustProizvodi.Where(pp => pp.PopustID == id);
                    _context.PopustProizvodi.RemoveRange(existingPopustProizvodi);

                    // Add the new selected products
                    if (selectedProducts != null)
                    {
                        foreach (var selectedProductId in selectedProducts)
                        {
                            _context.PopustProizvodi.Add(new PopustProizvod { PopustID = id, ProizvodID = selectedProductId });
                        }
                    }

                    _context.Update(popust);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PopustExists(popust.PopustID))
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

            return View(popust);
        }


        // GET: Popust/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var popust = await _context.Popusti
                .FirstOrDefaultAsync(m => m.PopustID == id);
            if (popust == null)
            {
                return NotFound();
            }

            return View(popust);
        }

        // POST: Popust/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var popust = await _context.Popusti.FindAsync(id);
            _context.Popusti.Remove(popust);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PopustExists(int id)
        {
            return _context.Popusti.Any(e => e.PopustID == id);
        }
    }
}
