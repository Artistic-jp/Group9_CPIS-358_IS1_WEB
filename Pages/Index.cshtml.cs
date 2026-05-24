using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group9_CPIS_358_IS1_WEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var welcomeMessage = HttpContext.Session.GetString("WelcomeMessage");
            if (!string.IsNullOrEmpty(welcomeMessage))
            {
                ViewData["WelcomeMessage"] = welcomeMessage;
                // Clear it so it only shows once
                HttpContext.Session.Remove("WelcomeMessage");
                //welcome message
            }
        }
    }
}
