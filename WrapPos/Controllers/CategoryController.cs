using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult ManageCategory()
        {
            return View();
        }

        public static object MockItemManager()
        {
            throw new NotImplementedException();
        }

        ELaundryDBEntities db = new ELaundryDBEntities();
        public JsonResult GetData()
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<CategoryViewModel> lst = new List<CategoryViewModel>();
                var empList = db.tblCategories.Select(x => new { CategoryId = x.CategoryId, CategoryName = x.CategoryName }).ToList();
                foreach (var item in empList)
                {
                    lst.Add(new CategoryViewModel() { CategoryId = item.CategoryId, CategoryName = item.CategoryName });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckCategory(string categoryname)
        {
            bool isexists = false;
                tblCategory tb = db.tblCategories.Where(c => c.CategoryName == categoryname).FirstOrDefault();
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

                    return View(new CategoryViewModel());
                }
            }
            else
            {
                using (ELaundryDBEntities db = new ELaundryDBEntities())
                {
                    CategoryViewModel sub = new CategoryViewModel();
                    var menu = db.tblCategories.Where(x => x.CategoryId == id).FirstOrDefault();
                    sub.CategoryId = menu.CategoryId;
                    sub.CategoryName = menu.CategoryName;
                    return View(sub);
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(CategoryViewModel sm)
        {
            using (ELaundryDBEntities db = new ELaundryDBEntities())
            {
                if (sm.CategoryId == 0)
                {
                    tblCategory tb = new tblCategory();
                    tb.CategoryName = sm.CategoryName;
                    db.tblCategories.Add(tb);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tblCategory tbm = db.tblCategories.Where(m => m.CategoryId == sm.CategoryId).FirstOrDefault();
                    tbm.CategoryName = sm.CategoryName;
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
                tblCategory sm = db.tblCategories.Where(x => x.CategoryId == id).FirstOrDefault();
                db.tblCategories.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}