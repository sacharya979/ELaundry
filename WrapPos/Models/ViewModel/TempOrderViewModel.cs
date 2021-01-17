using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class TempOrderViewModel
    {
        public int TempOrderId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string TokenNo { get; set; }
        public Nullable<int> GarmentId { get; set; }
        public string GarmentName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<bool> TW { get; set; }
        public string ItemState { get; set; }
    }
}