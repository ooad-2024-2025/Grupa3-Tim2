using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slasticarna.Data;
using Slasticarna.Models;
using System.Security.Claims;
using System.Text.Json;

namespace Slasticarna.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppUser
        public async Task<IActionResult> Index()
        {
            var users = await _context.AppUsers.Include(u => u.Popust).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> AccountSettings()
        {
            var userJson = HttpContext.Session.GetString("AppUser");

            if (string.IsNullOrEmpty(userJson))
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            var sessionUser = JsonSerializer.Deserialize<ApplicationUser>(userJson);
            var user = await _context.AppUsers.FindAsync(sessionUser.Id);

            if (user == null)
                return NotFound();

            ViewBag.User = user;
            return View();
        }


        // GET: AppUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.AppUsers
                .Include(u => u.Popust)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var userJson = HttpContext.Session.GetString("AppUser");
            if (string.IsNullOrEmpty(userJson))
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            var sessionUser = JsonSerializer.Deserialize<ApplicationUser>(userJson);
            var user = await _context.AppUsers.FindAsync(sessionUser.Id);

            if (user != null)
            {
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfile(ApplicationUser model, IFormFile AvatarImage)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) return NotFound();

            // Update osnovnih podataka
            user.Ime = model.Ime;
            user.Prezime = model.Prezime;
            user.Email = model.Email;
            user.PhoneNumber = model.BrTelefona;
            user.Adresa = model.Adresa;

            // Spremanje slike
            if (AvatarImage != null && AvatarImage.Length > 0)
            {
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(AvatarImage.FileName);
                var savePath = Path.Combine("img", "avatars", fileName);
                var fullPath = Path.Combine(wwwRootPath, savePath);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await AvatarImage.CopyToAsync(stream);
                }

                user.AvatarPath = "/" + savePath.Replace("\\", "/");
            }

            await _context.SaveChangesAsync();

            // Updateaj sesiju ako koristiš Session za usera
            HttpContext.Session.SetString("AppUser", JsonSerializer.Serialize(user));

            return RedirectToAction("AccountSettings");
        }



    }
}


