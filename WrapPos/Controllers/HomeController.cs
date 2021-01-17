using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    public class HomeController : Controller
    {
        ELaundryDBEntities _db = new ELaundryDBEntities();
        public ActionResult Index()
        {
            ViewBag.Branches = _db.tblBranches.ToList();
            ViewBag.Categories = _db.tblCategories.ToList();
            return View();
        }

        //Pricing Page ActionResult

        public ActionResult Pricing()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }


        /// <summary>FAQs this instance.</summary>
        /// <returns>ActionResult.</returns>
        public ActionResult FAQ()
        {
            return View();
        }
       
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Blog1()
        {
            return View();
        }
        public ActionResult Blog2()
        {
            return View();
        }


        public JsonResult GetOrderList(TempUserViewModel tb)
        {
            tblOrder to = _db.tblOrders.Where(t => t.TokenNo == tb.TokenNo).FirstOrDefault();
            if (to != null)
            {
                if (to.Status == "Active")
                {
                    var lst = _db.tblTempOrders.Where(u => u.BranchId == tb.BranchId && u.TokenNo == tb.TokenNo).Select(x => new { GarmentName = x.tblGarment.GarmentName, Quantity = x.Quantity, ItemState = x.ItemState }).ToList();
                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
               
                return Json(1, JsonRequestBehavior.AllowGet);
            }
           
        }


        }
}