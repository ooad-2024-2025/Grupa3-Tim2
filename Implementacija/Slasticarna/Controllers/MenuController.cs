using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slasticarna.Data;
using Slasticarna.Models;

namespace Slasticarna.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MenuController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MenuPage
        public async Task<IActionResult> Index()
        {
            var products = await _context.Proizvodi.ToListAsync();
            ViewBag.Products = products;

            // Check if the user is logged in
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewBag.Username = user.UserName;
            }

            return View(products);
        }

        // POST: Submit Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitOrder(List<ProductOrder> quantities)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");  // Redirect to Login if user is not authenticated
            }

            if (quantities != null && quantities.Any())
            {
                // Create the new order (Narudzba)
                var order = new Narudzba
                {
                    KorisnikID = user.Id,  // Set the logged-in user's ID
                    Datum = DateTime.Now.Date,
                    Vrijeme = DateTime.Now.TimeOfDay,
                    UkupnaCijena = 0, // Will calculate the total price later
                    StanjeNarudzbe = 1, // Example: Status 1 for "Pending" (adjust as needed)
                };

                _context.Narudzbe.Add(order);
                await _context.SaveChangesAsync(); // Save to get the order ID

                double totalPrice = 0;

                // Add order items (StavkaNarudzbe)
                foreach (var quantity in quantities)
                {
                    var product = await _context.Proizvodi.FindAsync(quantity.ProductId);
                    if (product != null && quantity.Quantity > 0)
                    {
                        var orderItem = new StavkaNarudzbe
                        {
                            NarudzbaID = order.NarudzbaID,
                            ProizvodID = quantity.ProductId,
                            Kolicina = quantity.Quantity,  // Store the quantity
                        };

                        totalPrice += product.Cijena * quantity.Quantity;

                        _context.StavkeNarudzbe.Add(orderItem);
                    }
                }

                // Update total price of the order
                order.UkupnaCijena = totalPrice;

                _context.Update(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(OrderConfirmation)); // Redirect to a confirmation page after successful order
            }

            return View("Index");
        }


        public IActionResult OrderConfirmation()
        {
            return View(); // A simple confirmation view can be created
        }
    }

    public class ProductOrder
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
