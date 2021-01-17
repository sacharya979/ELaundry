using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELaundry.Models.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="Username Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Password Mismatch")]
        public string RetypePassword { get; set; }
        [Required(ErrorMessage = "Fullname Required")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Role Required")]
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
    }
}