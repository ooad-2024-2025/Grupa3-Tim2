using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Slasticarna.Models;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Slasticarna.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;

            [Required]
            [Display(Name = "First Name")]
            public string Ime { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Last Name")]
            public string Prezime { get; set; } = string.Empty;

            [Display(Name = "Address")]
            public string? Adresa { get; set; }

            [Display(Name = "Phone Number")]
            public string? BrTelefona { get; set; }

            [Display(Name = "User Type")]
            public string VrstaKorisnika { get; set; } = "Korisnik";
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Ime = Input.Ime,
                    Prezime = Input.Prezime,
                    Adresa = Input.Adresa,
                    BrTelefona = Input.BrTelefona
                };

                System.Diagnostics.Debug.WriteLine("OVO JE USER: "+user);

                // Check if the email already exists
                var existingUser = await _userManager.FindByEmailAsync(Input.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already in use.");
                }

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl ?? "/");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Log errors if the model is invalid
            if (!ModelState.IsValid)
            {
                foreach (var stateError in ModelState.Values)
                {
                    foreach (var error in stateError.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine("ModelState Error: " + error.ErrorMessage);
                    }
                }
            }

            return Page();
        }

    }
}
