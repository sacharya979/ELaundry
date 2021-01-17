using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        ELaundryDBEntities _db = new ELaundryDBEntities();
        [Authorize(Roles = "Collector")]
        public ActionResult ManageOrder()
        {
            ViewBag.Categories = _db.tblCategories.ToList();
            ViewBag.garments = _db.tblGarments.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ManageOrder(List<tblTempOrder> lst)
        {
            if (lst != null)
            {
                //List<tblTempOrder> neworder = new List<tblTempOrder>();
                List<TempOrderViewModel> neworder = new List<TempOrderViewModel>();
                foreach (var item in lst)
                {
                    TempOrderViewModel tb = new TempOrderViewModel();
                    tblGarment tbpro = _db.tblGarments.Where(p => p.GarmentId == item.GarmentId).FirstOrDefault();

                    tb.GarmentId = item.GarmentId;
                    tb.GarmentName = tbpro.GarmentName;
                    tb.UnitPrice = item.UnitPrice;
                    tb.Quantity = item.Quantity;
                    tb.Total = item.Total;
                    tb.TokenNo = item.TokenNo;
                    tb.TW = item.TW;
                    tb.BranchId = Convert.ToInt32(Session["branchid"]);
                    tb.ItemState = "Queued";
                    neworder.Add(tb);

                }
                TempData["mytemp"] = neworder;
                //List<tblTempOrder> temporder = TempData["mytemp"] as List<tblTempOrder>;
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
        }
       
        //user order report
        public ActionResult ViewOrderReport(int id)
        {
            List<tblInvoice> lst = _db.tblInvoices.Where(u => u.UserId == id).ToList();
            return View();
        }
        
        public List<tblGarment> GetGarmentByCatId(int catid)
        {
            return _db.tblGarments.Where(p => p.CategoryId == catid).ToList();
        }
        public JsonResult GetGarmentByCategoryId(int id)
        {
            _db.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
            {
                return Json(_db.tblGarments.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var garments = _db.tblGarments.Where(e => e.CategoryId == id).ToList();
                
                    return Json(garments, JsonRequestBehavior.AllowGet);
               
            }
        }
        public JsonResult GetGarmentByGarmentName(string search)
        {
            _db.Configuration.ProxyCreationEnabled = false;
            var garments =_db.tblGarments.Where(p => p.GarmentName.Contains(search)).ToList();
                return Json(garments, JsonRequestBehavior.AllowGet);
           
        }
        public JsonResult GetbyID(int ID)
        {
            _db.Configuration.ProxyCreationEnabled = false;
            var garment = _db.tblGarments.Where(x => x.GarmentId == ID).FirstOrDefault();
            return Json(garment, JsonRequestBehavior.AllowGet);
        }
        
       


    }
}