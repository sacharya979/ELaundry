using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class InvoiceReportViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public decimal? TotalAmount { get; set; }
        public string InvoiceDate { get; set; }
    }
}