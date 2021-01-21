﻿using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.ViewModel
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage ="UserName Requeired")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName Requeired")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Requeired")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email Requeired")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Email address does not format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Requeired")]
        [Display(Name = "Password Name")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "IsAdmin Requeired")]
        [Display(Name = "IsAdmin")]
        public bool IsAdmin { get; set; }
        public bool IsSupplier { get; set; }
    }
}