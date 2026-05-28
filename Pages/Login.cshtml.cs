using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Group9_CPIS_358_IS1_WEB.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string StudentId { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Hardcoded admin shortcut: 0000 / 0000 opens the admin dashboard.
                if (Input.StudentId == "0000" && Input.Password == "0000")
                {
                    HttpContext.Session.SetString("IsAdmin", "true");
                    return RedirectToPage("/Admin");
                }

                var result = await _signInManager.PasswordSignInAsync(Input.StudentId, Input.Password, isPersistent: true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Set a session value for demonstration
                    HttpContext.Session.SetString("WelcomeMessage", "Welcome back, " + Input.StudentId + "!");
                    return LocalRedirect("~/");
                }
                else
                {
                    ErrorMessage = "Invalid login attempt.";
                    return Page();
                }
            }

            return Page();
        }
    }
}
