using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group9_CPIS_358_IS1_WEB.Pages
{
    public class AdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public List<AdminUserRow> Users { get; set; } = new();

        public int TotalUsers => Users.Count;

        public class AdminUserRow
        {
            public string StudentId { get; set; } = "";
            public string FullName { get; set; } = "";
            public string Email { get; set; } = "";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Login");
            }

            foreach (var user in _userManager.Users.ToList())
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var fullName = claims.FirstOrDefault(c => c.Type == "FullName")?.Value ?? "—";
                Users.Add(new AdminUserRow
                {
                    StudentId = user.UserName ?? "—",
                    FullName = fullName,
                    Email = user.Email ?? "—"
                });
            }

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToPage("/Login");
        }
    }
}
