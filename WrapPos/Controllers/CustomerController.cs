using ELaundry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELaundry.Controllers
{
    public class CustomerController : Controller
    {
        ELaundryDBEntities _db = new ELaundryDBEntities();
        // GET: Customer
        public ActionResult SchedulePickup()
        {
            ViewBag.TimeSlot = _db.tblTimeSlots.ToList();
            
            return View();
        }
       
        [HttpPost]
        public ActionResult SchedulePickUp(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                ELaundryDBEntities _db = new ELaundryDBEntities();
                //Creating variable to get the value

                string TimeSlot = collection["TimeSlot"];
                string FullName = collection["FullName"];
                string Phone = collection["Phone"];
                string Email = collection["Email"];
                string Address = collection["Address"];
                string SecondaryAddress = collection["SecondaryAddress"];
                string PickupDate = collection["PickupDate"];
                string DeliveryDate = collection["DeliveryDate"];
                tblSchedulePickup obj = new tblSchedulePickup();
                obj.FullName = FullName;
                obj.Phone = Phone;
                obj.Email = Email;
                obj.Address = Address;
                obj.SecondaryAddress = SecondaryAddress;
                obj.PickupDate = PickupDate;
                obj.DeliveryDate = DeliveryDate;
                obj.TimeSlot = TimeSlot;
                obj.Userid = 5;
                _db.tblSchedulePickups.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Thankyou");
            }
            catch
            {
                return View();
            }
        
    }

        // Return the user to thank you page. 
        public ActionResult Thankyou()
        {
            return View();
        }

    }
}