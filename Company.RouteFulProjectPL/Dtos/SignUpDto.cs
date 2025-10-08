using System.ComponentModel.DataAnnotations;

namespace Company.RouteFulProject.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="UserName is required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="FirstName is required !!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="LastName is required !!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required !!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required !!")]
        [DataType(DataType.Password)]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",ErrorMessage = "Your password must be between 8 and 32 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage="Password and confirm password aren't match")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
