using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Group9_CPIS_358_IS1_WEB.Pages
{
    public class SignupModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public SignupModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, MinimumLength = 7, ErrorMessage = "Student ID must be exactly 7 digits.")]
            public string StudentId { get; set; }

            [Required]
            public string FullName { get; set; }

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
                var user = new IdentityUser { UserName = Input.StudentId, Email = Input.StudentId + "@kau.edu.sa" };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Store full name as a claim so it shows in the header
                    await _userManager.AddClaimAsync(user, new Claim("FullName", Input.FullName));
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    HttpContext.Session.SetString("WelcomeMessage", "Welcome, " + Input.FullName + "! Your account was successfully created.");
                    return LocalRedirect("~/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    ErrorMessage += error.Description + " ";
                }
            }

            return Page();
        }
    }
}
