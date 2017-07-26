using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRModel.Login
{
    public class UserModel
    {

        public int UserId { get; set; }
        
        public string IsActive { get; set; }
        public string IsLocked { get; set; }
        public DateTime LastLoginAttemptDate { get; set; }
        public DateTime LastUserLockedTime { get; set; }
        public int FailedPasswordCount { get; set; }
        public string IsStaff { get; set; }
        public LoginEnum status { get; set; }

        [DisplayName("Login Name")]
        [Required(ErrorMessage = "Login Name can not be blank")]
        [StringLength(100, ErrorMessage = "Login should be equal or less than 100 character")]
        public string LoginName { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name can not be blank")]
        [StringLength(50, ErrorMessage = "First Name should be equal or less than 50 character")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
       
        
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name can not be blank")]
        [StringLength(50, ErrorMessage = "Last Name should be equal or less than 50 character")]
        public string LastName { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
    }
    public enum LoginEnum
    {
        Pass,
        FailByCount,
        FailByTime,
        UserPasswordWrong

    }
    public class ResetPasswordViewModel
    {
        [Required]
        
        [Display(Name = "LoginName")]
        public string LoginName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        
    }
}
