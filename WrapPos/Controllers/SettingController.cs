using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    public class SettingController : Controller
    {
        // GET: Setting
        [Authorize(Roles = "Admin")]
        public ActionResult ManageSetting()
        {
            return View();
        }
        ELaundryDBEntities db = new ELaundryDBEntities();
        /// <summary>Gets the data.</summary>
        /// <returns>JsonResult.</returns>
        public JsonResult GetData()
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<SettingViewModel> lst = new List<SettingViewModel>();
                var empList = db.tblSettings.ToList();
                foreach (var x in empList)
                {
                    lst.Add(new SettingViewModel() { SettingId = x.SettingId, DiscountRate = x.DiscountRate, Description = x.Description, IsActive = x.IsActive });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>Adds the or edit.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                using (ELaundryDBEntities db = new ELaundryDBEntities())
                {

                    return View(new SettingViewModel());
                }
            }
            else
            {
                using (ELaundryDBEntities db = new ELaundryDBEntities())
                {
                    SettingViewModel sub = new SettingViewModel();
                    var menu = db.tblSettings.Where(x => x.SettingId == id).FirstOrDefault();
                    sub.SettingId = menu.SettingId;
                    sub.DiscountRate = menu.DiscountRate;
                    sub.Description = menu.Description;
                    sub.IsActive = menu.IsActive;
                    return View(sub);
                }
            }
        }

        /// <summary>Adds the or edit.</summary>
        /// <param name="sm">The sm.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult AddOrEdit(SettingViewModel sm)
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                if (sm.SettingId == 0)
                {
                    tblSetting tb = new tblSetting();
                    tb.DiscountRate = sm.DiscountRate;
                    tb.Description = sm.Description;
                    tb.IsActive = sm.IsActive;
                    db.tblSettings.Add(tb);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tblSetting tbm = db.tblSettings.Where(m => m.SettingId == sm.SettingId).FirstOrDefault();
                    tbm.DiscountRate = sm.DiscountRate;
                    tbm.Description = sm.Description;
                    tbm.IsActive = sm.IsActive;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                tblSetting sm = db.tblSettings.Where(x => x.SettingId == id).FirstOrDefault();
                db.tblSettings.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}