using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELaundry.Models
{
    public  class DhobiDB
    {
        private  ELaundryDBEntities _db = new ELaundryDBEntities();
        public  List<tblTempOrder> getActiveTokenTempOrderItem(string tokenno, int branchid)
        {
           
            List<tblTempOrder> lst = _db.tblTempOrders.Where(t => t.TokenNo == tokenno && t.BranchId==branchid).ToList();
            return lst;
        }
    }
}