using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class ClosingViewModel
    {
        public int ClosingId { get; set; }
        public Nullable<decimal> SalesAmount { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<System.DateTime> ClosingDateTime { get; set; }
        public string Username { get; set; }
        public Nullable<int> UserId { get; set; }
        public string BranchName { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}