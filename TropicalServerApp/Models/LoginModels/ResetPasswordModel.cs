using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TropicalServerApp.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }
        [Required]
        public string ResetCode { get; set; }
    }
}