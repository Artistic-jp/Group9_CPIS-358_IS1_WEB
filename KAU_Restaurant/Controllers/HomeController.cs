using System.Web.Mvc;

namespace KAU_Restaurant.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        // Satisfies: Using Sessions - personalized welcome message
        public ActionResult Index()
        {
            if (Session["StudentID"] != null)
            {
                ViewBag.WelcomeMessage = "Welcome back, Student " + Session["StudentID"].ToString() + "!";
            }
            else
            {
                ViewBag.WelcomeMessage = "Welcome to KAU Restaurant";
            }
            return View();
        }
    }
}
