using DataModel.Library;
using Interview.Reviews.Web.App.DAL.Db_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interview.Reviews.Web.App.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        UserDbContext db = new UserDbContext();
      

        public ActionResult GetAdminDetailById()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int userId = Convert.ToInt32(Session["UserId"]);

            // Use the user ID to fetch user details
            UserModel user = db.GetUserDetails(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new List<UserModel> { user });
        }
        public ActionResult AdminDashboard()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int TotalUsers = db.GetUserCount("tblUser");
                int TotalGraduates = db.GetGraduatesCount("tblUser");
                int TotalAdmins = db.GetAdminCount("tblUser");
                int TotalReviews = db.GetReviewCount("tblReview");
                ViewBag.TotalUsers = TotalUsers;
                ViewBag.TotalGraduates = TotalGraduates;
                ViewBag.TotalAdmins = TotalAdmins;
                ViewBag.TotalReviews = TotalReviews;
                return View();
            }
          
        }

        /// 
        /// controller for Partial views 
        ///
        public ActionResult GetGraduateList()
        {
               var Grad = db.GetGraduatesFromDatabase();// Get students data from database using ADO.NET
                return PartialView("_Graduates", Grad);
        }

        public ActionResult GetAdminList()
        {
            var Grad = db.GetAdminsFromDatabase();// Get students data from database using ADO.NET
            return PartialView("_Admins", Grad);
        }

        public ActionResult GetUserList()
        {
            var users = db.GetUsersFromDatabase();// Get students data from database using ADO.NET
            return PartialView("_Users", users);
        }
        public ActionResult GetReviewList()
        {
            var review = db.GetReviewsFromDatabase();// Get students data from database using ADO.NET
            return PartialView("_Reviews", review);
        }
    }
}