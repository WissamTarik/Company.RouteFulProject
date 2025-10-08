using System.ComponentModel.DataAnnotations;

namespace Company.RouteFulProject.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is required !!")]
        [DataType(DataType.Password)]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",ErrorMessage = "Your password must be between 8 and 32 characters long")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage ="Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
