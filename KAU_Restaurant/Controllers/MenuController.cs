using System.Collections.Generic;
using System.Web.Mvc;
using KAU_Restaurant.Models;

namespace KAU_Restaurant.Controllers
{
    public class MenuController : Controller
    {
        private KauDbContext db = new KauDbContext();

        // GET: /Menu/Index
        // Satisfies: CRUD Read - RETRIEVE all meals from SQLite and display dynamically
        public ActionResult Index()
        {
            List<Meal> meals = db.GetAllMeals();
            return View(meals);
        }
    }
}
