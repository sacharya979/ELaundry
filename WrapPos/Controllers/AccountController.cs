using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        ELaundryDBEntities _db = new ELaundryDBEntities();
        public ActionResult Login()
        {

           

            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginViewModel l, string ReturnUrl = "")
        {
           
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                string password = Utilities.Base64Encode(l.Password);
                var users = db.tblUsers.Where(a => a.Username == l.Username && a.Password == password).FirstOrDefault();
                if (users != null)
                {

                    tblUserBranch ubra = db.tblUserBranches.Where(u => u.UserId == users.UserId).FirstOrDefault();
                    Session.Add("branchid", ubra.BranchId);
                    tblBranch br = db.tblBranches.Where(b => b.BranchId == ubra.BranchId).FirstOrDefault();
                    Session.Add("branchname", br.BranchName);
                    Session.Add("username", users.Username);
                    Session.Add("fullname", users.Fullname);
                    FormsAuthentication.SetAuthCookie(l.Username, true);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session.Add("userid", users.UserId);
                       
                            return RedirectToAction("DashBoard", "Admin");
                        


                    }
                }
                else
                {

                    ModelState.AddModelError("", "Invalid User");

                }
            }
            return View();


        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        //[Authorize(Roles = "Admin")]
        public ActionResult ChangePassword()
        {



            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangePassword(ChangePasswordViewModel ch)
        {

            string username = Session["username"].ToString();
            string password = Utilities.Base64Encode(ch.OldPassword);
            tblUser us = _db.tblUsers.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            if (us != null)
            {
                string newpassword = Utilities.Base64Encode(ch.NewPassword);
                us.Password = newpassword;
                _db.SaveChanges();
                ViewBag.Message = "Change Password Successfully";
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Wrong Old Password";
            }
            return View();
           
        }

        
    }
}