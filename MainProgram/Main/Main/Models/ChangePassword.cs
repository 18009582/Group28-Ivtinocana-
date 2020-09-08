using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Main.Models
{
    public class ChangePassword
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "OTP is required")]
        public string OTP { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum of 6 characters required")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password should match with Password")]
        public string ConfirmPassword { get; set; }
    }
}