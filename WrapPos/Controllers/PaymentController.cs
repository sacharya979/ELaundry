using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        [Authorize(Roles = "Collector")]
        public ActionResult MakePayment(int id=0)
        {
            List<TempOrderViewModel> temporder = TempData["mytemp"] as List<TempOrderViewModel>;
            if (temporder != null)
            {

                ViewBag.Orders = temporder;
                decimal totalamount = 0M;
                foreach (var item in ViewBag.Orders)
                {
                    totalamount += Convert.ToDecimal(item.Total);
                }
                tblSetting tbs = _db.tblSettings.OrderByDescending(a => a.SettingId).Take(1).FirstOrDefault();
                decimal discountrate =Convert.ToDecimal( tbs.DiscountRate);
                ViewBag.TotalAmount = totalamount;
                decimal discountamount = discountrate / 100 * totalamount;
                ViewBag.DiscountAmount = discountamount;
                ViewBag.NetAmount = totalamount - discountamount;
                
            }
            else
            {

                if (ViewBag.Orders != null)
                {
                    decimal totalamount = 0M;
                    foreach (var item in ViewBag.Orders)
                    {
                        totalamount += Convert.ToDecimal(item.Total);
                    }
                }
            }
            return View();
        }
        public JsonResult InsertOrder(int id)
        {
            tblOrder tb = new tblOrder();
            tb.TokenNo = id.ToString();
            tb.Status = "Active";
            tb.BranchId= Convert.ToInt32(Session["branchid"]);
            _db.tblOrders.Add(tb);
             _db.SaveChanges();
            ViewBag.OrderId = tb.OrderId;

            return Json(tb.OrderId, JsonRequestBehavior.AllowGet);


        }
        public JsonResult InsertGarment(List<tblTempOrder> lst)
        {

            foreach (var item in lst)
            {
                tblTempOrder tb = new tblTempOrder();
                tb.GarmentId = item.GarmentId;
                tb.UnitPrice = item.UnitPrice;
                tb.Quantity = item.Quantity;
                tb.Total = item.Total;
                tb.TokenNo = item.TokenNo;
                tb.TW = item.TW;
                tb.BranchId = Convert.ToInt32(Session["branchid"]);
                tb.ItemState = "Queued";
                _db.tblTempOrders.Add(tb);
                _db.SaveChanges();

            }

            return Json(1, JsonRequestBehavior.AllowGet);
        }
        ELaundryDBEntities _db = new ELaundryDBEntities();
       
        public JsonResult GetOrderDetailsbyTokenNo(string tokenno)
        {
            //get value from session
            int branchid = Convert.ToInt32(Session["branchid"]);
            _db.Configuration.ProxyCreationEnabled = false;
           
                return Json(_db.tblTempOrders.Where(a => a.TokenNo == tokenno && a.BranchId == branchid).Select(x=>new {GarmentName=x.tblGarment.GarmentName,UnitPrice=x.UnitPrice, Quantity=x.Quantity, Total=x.Total, TW=x.TW}).ToList(), JsonRequestBehavior.AllowGet);
           
        }
        public JsonResult CheckTokenTaken(int id)
        {
            if (id.Equals(null))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                int branchid = Convert.ToInt32(Session["branchid"]);
                tblOrder or = _db.tblOrders.Where(t => t.TokenNo == id.ToString() && t.BranchId == branchid).OrderByDescending(t => t.OrderId).Take(1).FirstOrDefault();
                if (or != null)
                {
                    return Json(or.Status, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
    }
}