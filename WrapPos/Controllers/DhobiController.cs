using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;

namespace ELaundry.Controllers
{
    public class DhobiController : Controller
    {
        // GET: Dhobi
        ELaundryDBEntities _db = new ELaundryDBEntities();
        [Authorize(Roles = "Dhobi")]
        public ActionResult ViewDhobiOrder()
        {
            int branchid = Convert.ToInt32(Session["branchid"]);
            ViewBag.Tokens = _db.tblOrders.Where(s => s.Status == "Active" && s.BranchId==branchid).ToList();
            return View();
        }
        /// <summary>Gets the temporary order by temporary identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>JsonResult.</returns>
        [Authorize(Roles = "Dhobi")]
        public JsonResult GetTempOrderByTempId(int id)
        {
            _db.Configuration.ProxyCreationEnabled = false;
            int branchid = Convert.ToInt32(Session["branchid"]);
            var temporder = _db.tblTempOrders.Where(x => x.TempOrderId == id && x.BranchId == branchid).FirstOrDefault();
            return Json(temporder, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Dhobi")]
        public JsonResult UpdateGarmentStatus(int id)
        {
            tblTempOrder tb = _db.tblTempOrders.Where(k => k.TempOrderId == id).FirstOrDefault();
            if (tb != null)
            {
                tb.ItemState = "Completed";
                _db.SaveChanges();
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Dhobi")]
        public JsonResult UpdateTokenStatus(int id)
        {
            int branchid = Convert.ToInt32(Session["branchid"]);
            tblOrder tb = _db.tblOrders.Where(k => k.OrderId == id && k.BranchId==branchid).FirstOrDefault();
            tb.Status = "Inactive";
            _db.SaveChanges();
            return Json(tb.TokenNo, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Dhobi")]
        public JsonResult DeleteTempOrder(int id)
        {
            int branchid = Convert.ToInt32(Session["branchid"]);
            string tokenno = id.ToString();
            List<tblTempOrder> lst = _db.tblTempOrders.Where(k => k.TokenNo == tokenno && k.BranchId==branchid).ToList();
            foreach (tblTempOrder item in lst)
            {
                tblOrderItem or = new tblOrderItem();
                or.GarmentId = item.GarmentId;
                or.Quantity = item.Quantity;
                or.TakeAway = item.TW.ToString();
                or.BranchId = branchid;
                or.TotalPrice = item.Total;
                tblOrder tbor = _db.tblOrders.Where(t => t.TokenNo == tokenno).OrderByDescending(a=>a.OrderId).Take(1).FirstOrDefault();
                or.OrderId = tbor.OrderId;
                _db.tblOrderItems.Add(or);
                _db.SaveChanges();
               

                tblTempOrder tb = _db.tblTempOrders.Where(t => t.TokenNo == item.TokenNo && t.BranchId == branchid).FirstOrDefault();
                _db.tblTempOrders.Remove(tb);
                _db.SaveChanges();
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}