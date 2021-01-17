using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;

namespace ELaundry.Controllers
{
    public class OrderItemController : Controller
    {
        // GET: OrderItem
        public ActionResult OrderSearch()
        {
            return View();
        }
        ELaundryDBEntities _db = new ELaundryDBEntities();
      
        public JsonResult OrderSearchbyId(int id)
        {
            
                var items = _db.tblOrderItems.Where(a => a.OrderId == id).Select(x => new { GarmentName = x.tblGarment.GarmentName, UnitPrice = x.tblGarment.UnitPrice, Quantity = x.Quantity, Total = x.TotalPrice, TW = x.TakeAway }).ToList();
                return Json(items, JsonRequestBehavior.AllowGet);
           
            
        }
    }
}