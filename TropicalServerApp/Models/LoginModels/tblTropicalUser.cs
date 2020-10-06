namespace TropicalServerApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    [Table("tblTropicalUser")]
    public partial class tblTropicalUser
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage ="Login ID is required.")]
        [StringLength(100)]
        [DisplayName("Login ID")]
        public string LoginID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50)]
        public string Password { get; set; }

        [NotMapped] // a property or class should be excluded from database mapping.
        public string LoginErrorMessage { get; set; }

        [NotMapped] // a property or class should be excluded from database mapping.
        [DisplayName("Remember my ID")]
        public bool RememberMe { get; set; }

      
        public string ResetPasswordCode { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public int? RoleID { get; set; }

        public int? UserRouteNumber { get; set; }

        public bool? Active { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
