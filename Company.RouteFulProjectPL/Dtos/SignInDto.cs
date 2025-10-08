using System.ComponentModel.DataAnnotations;

namespace Company.RouteFulProject.PL.Dtos
{
    public class SignInDto
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required !!")]
        [DataType(DataType.Password)]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",ErrorMessage = "Your password must be between 8 and 32 characters long")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
