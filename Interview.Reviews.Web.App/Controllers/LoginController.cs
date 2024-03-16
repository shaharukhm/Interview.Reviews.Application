using DataModel.Library;
using Interview.Reviews.Web.App.DAL.Db_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Interview.Reviews.Web.App.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        UserDbContext db = new UserDbContext();

        //[ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Abandon(); // Clear session
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        
        public ActionResult Login(UserModel _user)
        {
           var data = db.GetUserList().Where(w => w.Email == _user.Email && w.Password == _user.Password).FirstOrDefault();

            if (data != null)
            {
                Session["UserId"] = data.UserId;
                Session["Role"] = data.UserStatusId;

                //FormsAuthentication.SetAuthCookie(_user.Email, false);
                if (data.UserStatusId == 3) // admin
                {
                    ModelState.Clear();
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else
                {
                    ModelState.Clear();
                    return RedirectToAction("Welcome", "User");
                }
            }
            else
            {
                ViewBag.Message = "Please Enter Valid Email or Password...";
                ModelState.Clear();
                return View();
            }
           
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult ForgotPassword(UserModel _user)
        {
            var data = db.GetUserList().Where(w => w.Email == _user.Email ).FirstOrDefault();
            if (data != null)
            {
                Session["Email"] = _user.Email;
                return RedirectToAction("UpdatePassword", "Login");
            }
            else
            {
                ViewBag.Message = "Please enter a valid email address.";
                ModelState.Clear();
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordModel _user, string newPassword)
        {
            if (_user.NewPassword == _user.ConfirmPassword )
            {
                string email = (string)Session["Email"];
                if (ModelState.IsValid)
                {
                    db.UpdatePassword(email, newPassword);
                    ModelState.Clear();
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                ViewBag.Message = "Password must be matched...";
                ModelState.Clear();
                return View();
            }

           


            return View();
           
        }
    }
}