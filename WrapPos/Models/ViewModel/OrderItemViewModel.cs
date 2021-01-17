using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class OrderItemViewModel
    {
        public int? OrderId { get; set; }
        public int GarmentId { get; set; }
        public string GarmentName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<bool> TW { get; set; }
    }
}