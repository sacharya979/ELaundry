using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;
using ELaundry.Models.ViewModel;

namespace ELaundry.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult SalesReport()
        {
            return View();
        }
        // GET: Invoice
        ELaundryDBEntities _db = new ELaundryDBEntities();
        [Authorize(Roles = "Collector")]
        public JsonResult InsertInvoice(tblInvoice tb)
        {
            tblInvoice tbl = _db.tblInvoices.OrderByDescending(ti => ti.InvoiceId).Take(1).FirstOrDefault();
            tblSetting tbs = _db.tblSettings.OrderByDescending(s => s.SettingId).Take(1).FirstOrDefault();

            tblInvoice t = new tblInvoice();
            if (tbl != null)
            {
                t.InvoiceId = tbl.InvoiceId;
            }
            else
            {
                t.InvoiceId = 1;
            }
            t.OrderId = tb.OrderId;

            decimal taxamount =Convert.ToDecimal( tb.GrossAmount * 15 / 100);
           
            decimal grossamount =Convert.ToDecimal( tb.GrossAmount + taxamount);
            decimal discountamount = Convert.ToDecimal(tbs.DiscountRate / 100 * grossamount);
            t.DiscountAmount = discountamount;
            t.TaxAmount = taxamount;
            t.GrossAmount = tb.GrossAmount;
            t.NetAmount = tb.GrossAmount+taxamount-discountamount;

            t.InvoiceDate = DateTime.Today;
            t.UserId = Convert.ToInt32(Session["userid"]);
            t.BranchId = Convert.ToInt32(Session["branchid"]);
            _db.tblInvoices.Add(t);


            _db.SaveChanges();

            

            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShowReport(string FromDate, string ToDate)
        {
            DateTime fromdate = Convert.ToDateTime(FromDate);
            DateTime todate = Convert.ToDateTime(ToDate);
            List<InvoiceReportViewModel> lst = new List<InvoiceReportViewModel>();
            var reports = _db.getInvoiceByUserId_Date().Where(a=>a.InvoiceDate>= fromdate && a.InvoiceDate<= todate).ToList();
            foreach (var item in reports)
            {
                DateTime dtt =Convert.ToDateTime( item.InvoiceDate);

                tblUser tb = _db.tblUsers.Where(u => u.UserId == item.UserId).FirstOrDefault();
                lst.Add(new InvoiceReportViewModel() {Username=tb.Username, TotalAmount= item.TotalNetAmount, InvoiceDate= dtt.ToString("d") });
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
       
    }
}