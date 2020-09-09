﻿using System.ComponentModel.DataAnnotations;
namespace Vehlution_Everything_.Models
{
    public class ForgetPassword
    {
        [Display(Name = "User Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Email Id Required")]
        public string EmailId { get; set; }
    }
}