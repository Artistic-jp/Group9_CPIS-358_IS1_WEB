using System;
using System.Web.Mvc;
using System.Web;
using KAU_Restaurant.Models;

namespace KAU_Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private KauDbContext db = new KauDbContext();

        // GET: /Account/Login
        // Satisfies: Using Cookies - auto-fill from cookie if exists
        public ActionResult Login()
        {
            HttpCookie cookie = Request.Cookies["KAU_RememberMe"];
            if (cookie != null)
            {
                ViewBag.RememberedID = cookie["StudentID"];
                ViewBag.RememberMe = true;
            }
            return View();
        }

        // POST: /Account/Login
        // Satisfies: Forms, Sessions, Cookies
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string studentID, string password, bool rememberMe = false)
        {
            // Satisfies: Forms - Validate 7-digit Student ID
            if (string.IsNullOrEmpty(studentID) || studentID.Length != 7)
            {
                ViewBag.Error = "Student ID must be exactly 7 digits.";
                return View();
            }
            // Check if all digits
            foreach (char c in studentID)
            {
                if (!char.IsDigit(c))
                {
                    ViewBag.Error = "Student ID must contain only numbers.";
                    return View();
                }
            }

            // Satisfies: CRUD Read - RETRIEVE and verify from SQLite
            Student student = db.GetStudentByID(studentID);

            if (student != null && student.Password == password)
            {
                // Satisfies: Using Sessions - store Student ID
                Session["StudentID"] = student.StudentID;
                Session["StudentName"] = student.FullName;

                // Satisfies: Using Cookies - store if Remember Me checked
                if (rememberMe)
                {
                    HttpCookie cookie = new HttpCookie("KAU_RememberMe");
                    cookie["StudentID"] = student.StudentID;
                    cookie.Expires = DateTime.Now.AddDays(7);  // Cookie lasts 7 days
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    if (Request.Cookies["KAU_RememberMe"] != null)
                    {
                        HttpCookie cookie = new HttpCookie("KAU_RememberMe");
                        cookie.Expires = DateTime.Now.AddDays(-1);  // Expire immediately
                        Response.Cookies.Add(cookie);
                    }
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Student ID or Password.";
            return View();
        }

        // GET: /Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            // Also clear the Remember Me cookie on logout
            if (Request.Cookies["KAU_RememberMe"] != null)
            {
                HttpCookie cookie = new HttpCookie("KAU_RememberMe");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Login");
        }
    }
}
