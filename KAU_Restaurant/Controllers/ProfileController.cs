using System;
using System.Web.Mvc;
using System.Web;
using KAU_Restaurant.Models;

namespace KAU_Restaurant.Controllers
{
    public class ProfileController : Controller
    {
        private KauDbContext db = new KauDbContext();

        // GET: /Profile/Index
        // Satisfies: Using Sessions - check Session, redirect if empty
        public ActionResult Index()
        {
            if (Session["StudentID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Student student = db.GetStudentByID(Session["StudentID"].ToString());
            return View(student);
        }

        // POST: /Profile/UpdatePassword
        // Satisfies: Update - UPDATE password in SQLite
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(string newPassword, string confirmPassword)
        {
            if (Session["StudentID"] == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 4)
            {
                ViewBag.Error = "Password must be at least 4 characters.";
                return View("Index", db.GetStudentByID(Session["StudentID"].ToString()));
            }
            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View("Index", db.GetStudentByID(Session["StudentID"].ToString()));
            }

            if (db.UpdatePassword(Session["StudentID"].ToString(), newPassword))
                ViewBag.Message = "Password updated successfully!";
            else
                ViewBag.Error = "Failed to update password.";

            return View("Index", db.GetStudentByID(Session["StudentID"].ToString()));
        }

        // POST: /Profile/DeleteAccount
        // Satisfies: Delete - DELETE account from SQLite
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount()
        {
            if (Session["StudentID"] == null)
                return RedirectToAction("Login", "Account");

            db.DeleteStudent(Session["StudentID"].ToString());

            Session.Clear();
            Session.Abandon();

            // Remove Remember Me cookie on account deletion
            if (Request.Cookies["KAU_RememberMe"] != null)
            {
                HttpCookie cookie = new HttpCookie("KAU_RememberMe");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            TempData["Message"] = "Your account has been deleted.";
            return RedirectToAction("Login", "Account");
        }
    }
}
