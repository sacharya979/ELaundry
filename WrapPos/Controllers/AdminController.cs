using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;


namespace ELaundry.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>The database</summary>
        ELaundryDBEntities _db = new ELaundryDBEntities();
        // GET: Admin

        /// <summary>The CDB</summary>
        ClosingDB cdb = new ClosingDB();
        /// <summary>Dashboards this instance.</summary>
        /// <returns>ActionResult.</returns>
        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.NewOrder = _db.tblTempOrders.Where(o => o.ItemState == "Queued").ToList().Count;
            ViewBag.ActiveToken = _db.tblOrders.Where(o => o.Status == "Active").ToList().Count;

            return View();

        }

        /// <summary>Class ClosingDisplay.</summary>
        public class ClosingDisplay
        {
            public int? UserId { get; set; }
            public string Username { get; set; }
            public int? BranchId { get; set; }
            public string BranchName { get; set; }
            public decimal? TotalAmount { get; set; }
        }
       
        public JsonResult LineChart()
        {
            DateTime dt = Convert.ToDateTime(DateTime.Today);
            List<ClosingDisplay> lstclose = new List<ClosingDisplay>();
            List<tblUserRole> lstur = _db.tblUserRoles.Where(u => u.RoleId == 2).ToList();

            foreach (var ur in lstur)
            {

                tblUser tb = _db.tblUsers.Where(u => u.UserId == ur.UserId).FirstOrDefault();



                tblUserBranch tub = _db.tblUserBranches.Where(u => u.UserId == ur.UserId).FirstOrDefault();
                tblBranch tbbra = _db.tblBranches.Where(b => b.BranchId == tub.BranchId).FirstOrDefault();
                decimal amount = 0M;
                List<tblInvoice> lst = _db.tblInvoices.Where(t => t.InvoiceDate == dt).Where(u => u.UserId == ur.UserId).ToList();
                if (lst.Count > 0)
                {


                    foreach (var item in lst)
                    {

                        amount += Convert.ToDecimal(item.NetAmount);
                    }

                    ClosingDisplay cd = new ClosingDisplay();
                    cd.UserId = ur.UserId;
                    cd.TotalAmount = amount;
                    cd.Username = tb.Username;
                    cd.BranchName = tbbra.BranchName;
                    cd.BranchId = tbbra.BranchId;
                    lstclose.Add(cd);
                }




            }
            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dtt = new DataTable();
            dtt.Columns.Add("Username", System.Type.GetType("System.String"));
            dtt.Columns.Add("TotalAmount", System.Type.GetType("System.Decimal"));

            foreach (var item in lstclose)
            {
                DataRow dr = dtt.NewRow();
                dr["Username"] = item.Username;
                dr["TotalAmount"] = item.TotalAmount;
                dtt.Rows.Add(dr);
            }
            

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dtt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dtt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);

        }
        public JsonResult LineChartBranchWise()
        {
            DateTime dt = Convert.ToDateTime(DateTime.Today);
            List<ClosingDisplay> lstclose = new List<ClosingDisplay>();
            List<tblBranch> lstbranch = _db.tblBranches.ToList();

            foreach (var ur in lstbranch)
            { 
                tblBranch tbbra = _db.tblBranches.Where(b => b.BranchId == ur.BranchId).FirstOrDefault();
                decimal amount = 0M;
                List<tblInvoice> lst = _db.tblInvoices.Where(t => t.InvoiceDate == dt).Where(u => u.BranchId == ur.BranchId).ToList();
                if (lst.Count > 0)
                {
                    foreach (var item in lst)
                    {

                        amount += Convert.ToDecimal(item.NetAmount);
                    }
                    ClosingDisplay cd = new ClosingDisplay();
                   
                    cd.TotalAmount = amount;
                   
                    cd.BranchName = tbbra.BranchName;
                    cd.BranchId = tbbra.BranchId;
                    lstclose.Add(cd);
                }
            }
            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dtt = new DataTable();
            dtt.Columns.Add("BranchName", System.Type.GetType("System.String"));
            dtt.Columns.Add("TotalAmount", System.Type.GetType("System.Decimal"));
            foreach (var item in lstclose)
            {
                DataRow dr = dtt.NewRow();
                dr["BranchName"] = item.BranchName;
                dr["TotalAmount"] = item.TotalAmount;
                dtt.Rows.Add(dr);
            }
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dtt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dtt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
           
        }
        public JsonResult HighestSalesGarment()
        {
            DateTime dt = Convert.ToDateTime(DateTime.Today);
            var lstgarment = _db.GetTotalGarmentOrderCount();
            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dtt = new DataTable();
            dtt.Columns.Add("GarmentName", System.Type.GetType("System.String"));
            dtt.Columns.Add("TotalOrdered", System.Type.GetType("System.Int32"));
            foreach (var item in lstgarment)
            {
                DataRow dr = dtt.NewRow();
                tblGarment tb = _db.tblGarments.Where(p => p.GarmentId == item.GarmentId).FirstOrDefault();

                dr["GarmentName"] = tb.GarmentName;
                dr["TotalOrdered"] = item.TotalOrdered;
                dtt.Rows.Add(dr);
            }
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dtt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dtt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);

        }

    }
}