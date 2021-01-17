using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class GarmentViewModel
    {
        public int GarmentId { get; set; }
        [Required(ErrorMessage ="Category Required")]
        public int? CategoryId { get; set; }
       
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Garment Name Required")]
        public string GarmentName { get; set; }
        [Required(ErrorMessage = "Unitprice Required")]
        public decimal? UnitPrice { get; set; }
        
    }
}