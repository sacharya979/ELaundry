using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    public class UserController : Controller
    {
        // GET: garment
        [Authorize(Roles = "Admin")]
        public ActionResult ManageUser()
        {
            return View();
        }

        public JsonResult GetData()
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                MyRoleProvider mr = new MyRoleProvider();
                db.Configuration.LazyLoadingEnabled = false;
                List<UserViewModel> lstuser = new List<UserViewModel>();
                var lst = db.tblUsers.ToList();
                string[] str = new string[] { };
               
                foreach (var item in lst)
                {
                    string roles = "";
                    str = mr.GetRolesForUser(item.Username);
                   if(str.Length>0)
                    {
                        foreach (var r in str)
                        {
                            roles += r.ToString()+",";
                        }
                        roles = roles.Remove(roles.IndexOf(','));
                    }

                    tblUserBranch tbub = db.tblUserBranches.Where(u => u.UserId == item.UserId).FirstOrDefault();
                    tblBranch br = db.tblBranches.Where(u => u.BranchId == tbub.BranchId).FirstOrDefault();

                    lstuser.Add(new UserViewModel() { UserId = item.UserId, Username = item.Username, Fullname=item.Fullname, RoleName=roles, BranchName=br.BranchName });
                }
                return Json(new { data = lstuser }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            
                using (ELaundryDBEntities db = new ELaundryDBEntities())
                {
                    ViewBag.Roles = db.tblRoles.ToList();
                ViewBag.Branches = db.tblBranches.ToList();


                return View();
                }
           
        }


        [HttpPost]
        public ActionResult AddOrEdit(UserViewModel sm)
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                
                    tblUser tb = new tblUser();
                string password = Utilities.Base64Encode(sm.Password);
                    tb.Username = sm.Username;
                    tb.Password = password;
                    tb.Fullname = sm.Fullname;

                    db.tblUsers.Add(tb);
                    db.SaveChanges();

                    tblUserRole tu = new tblUserRole();
                    tu.RoleId = sm.RoleId;
                    tu.UserId = tb.UserId;
                    db.tblUserRoles.Add(tu);
                    db.SaveChanges();

                tblUserBranch br = new tblUserBranch();
                br.UserId = tb.UserId;
                br.BranchId = sm.BranchId;
                db.tblUserBranches.Add(br);
                db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                
               
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                tblUser sm = db.tblUsers.Where(x => x.UserId == id).FirstOrDefault();
                db.tblUsers.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}