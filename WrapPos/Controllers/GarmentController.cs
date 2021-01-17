using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    public class GarmentController : Controller
    {
        // GET: garment
        [Authorize(Roles = "Admin")]
        public ActionResult ManageGarment()
        {
            return View();
        }
       ELaundryDBEntities db = new ELaundryDBEntities();
        public JsonResult GetData()
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<GarmentViewModel> lstitem = new List<GarmentViewModel>();
                var lst = db.tblGarments.Include("tblCategory").ToList();
                foreach (var item in lst)
                {
                    lstitem.Add(new GarmentViewModel() { GarmentId = item.GarmentId, CategoryName = item.tblCategory.CategoryName, GarmentName = item.GarmentName, UnitPrice = item.UnitPrice });
                }
                return Json(new { data = lstitem }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckGarmentById(int id)
        {
            bool isexists = false;
            tblGarment tb = db.tblGarments.Where(c => c.GarmentId == id).FirstOrDefault();
            if (tb != null)
                isexists = true;
            else
                isexists = false;
            return Json(isexists, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckGarment(string GarmentName)
        {
            bool isexists = false;
            tblGarment tb = db.tblGarments.Where(c => c.GarmentName == GarmentName).FirstOrDefault();
            if (tb != null)
                isexists = true;
            else
                isexists = false;
            return Json(isexists, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                using (ELaundryDBEntities db = new ELaundryDBEntities())
                {
                    ViewBag.Categories = db.tblCategories.ToList();

                    return View(new GarmentViewModel());
                }
            }
            else
            {
                using (ELaundryDBEntities db = new ELaundryDBEntities())
                {
                    ViewBag.Categories = db.tblCategories.ToList();
                    GarmentViewModel sub = new GarmentViewModel();
                    var menu = db.tblGarments.Where(x => x.GarmentId == id).FirstOrDefault();
                    sub.GarmentId = menu.GarmentId;
                    sub.GarmentName = menu.GarmentName;
                    sub.UnitPrice = menu.UnitPrice;
                    sub.CategoryId = menu.tblCategory.CategoryId;
                    return View(sub);
                }
            }
        }



        [HttpPost]
        public ActionResult AddOrEdit(GarmentViewModel sm)
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                if (sm.GarmentId == 0)
                {
                    tblGarment tb = new tblGarment();
                    tb.CategoryId = sm.CategoryId;
                    tb.GarmentName = sm.GarmentName;
                    tb.UnitPrice = sm.UnitPrice;
                    db.tblGarments.Add(tb);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tblGarment tbm = db.tblGarments.Where(m => m.GarmentId == sm.GarmentId).FirstOrDefault();
                    tbm.CategoryId = sm.CategoryId;
                    tbm.GarmentName = sm.GarmentName;
                    tbm.UnitPrice = sm.UnitPrice;
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
                tblGarment sm = db.tblGarments.Where(x => x.GarmentId == id).FirstOrDefault();
                db.tblGarments.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}