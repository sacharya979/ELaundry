using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class SchedulePickupViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Username Required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number Required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Secondary Address Required")]
        public string SecondaryAddress { get; set; }
        public string PickupDate { get; set; }
        public string DeliveryDate { get; set; }
        [Required(ErrorMessage = "TimeSlot Required")]
        public string TimeSlot { get; set; }
        public Nullable<int> Userid { get; set; }

    }
}