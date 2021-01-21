using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is Required")]
        [MinLength(4,ErrorMessage ="Password min 4 must be character")]
        public string Password { get; set; }
    }
}
