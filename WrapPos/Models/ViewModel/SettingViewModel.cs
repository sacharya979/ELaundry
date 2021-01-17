using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class SettingViewModel
    {
        public int SettingId { get; set; }
        [Required(ErrorMessage ="Discount Rate Required")]
        public Nullable<decimal> DiscountRate { get; set; }
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}