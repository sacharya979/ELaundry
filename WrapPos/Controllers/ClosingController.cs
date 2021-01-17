using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELaundry.Models;

namespace ELaundry.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClosingController : Controller
    {
        // GET: Closing
        ELaundryDBEntities db = new ELaundryDBEntities();
        public class ClosingDisplay
        {
            public int? UserId { get; set; }
            public string Username { get; set; }
            public int? BranchId { get; set; }
            public string BranchName { get; set; }
            public decimal? TotalAmount { get; set; }
        }
        ClosingDB cdb = new ClosingDB();
        public ActionResult MakeClosing()
        {


            DateTime dt = Convert.ToDateTime(DateTime.Today);


            List<ClosingDisplay> lstclose = new List<ClosingDisplay>();

            List<tblUserRole> lstur = db.tblUserRoles.Where(u => u.RoleId == 2).ToList();

            foreach (var ur in lstur)
            {
                tblUser tb = db.tblUsers.Where(u => u.UserId == ur.UserId).FirstOrDefault();

                bool i = cdb.CheckClosingDate(tb.UserId);
                if (!i)
                {

                    tblUserBranch tub = db.tblUserBranches.Where(u => u.UserId == ur.UserId).FirstOrDefault();
                    tblBranch tbbra = db.tblBranches.Where(b => b.BranchId == tub.BranchId).FirstOrDefault();
                    decimal amount = 0M;
                    List<tblInvoice> lst = db.tblInvoices.Where(t => t.InvoiceDate == dt).Where(u => u.UserId == ur.UserId).ToList();
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
                    ViewBag.Records = lstclose;
                }
            }


            return View();
        }

        public JsonResult InsertClosing(tblClosing tb)
        {


            tb.ClosingDateTime = DateTime.Now;

            db.tblClosings.Add(tb);
            db.SaveChanges();



            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}