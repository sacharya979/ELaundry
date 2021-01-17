using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class ChangePasswordViewModel
    {

        public string Username { get; set; }
        [Required(ErrorMessage = "Old Password Require")]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Password Mismatch")]
        [Display(Name = "Confirm New")]
        public string ConfirmNew { get; set; }

    }
}