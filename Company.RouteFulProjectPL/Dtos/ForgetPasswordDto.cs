using System.ComponentModel.DataAnnotations;

namespace Company.RouteFulProject.PL.Dtos
{
    public class ForgetPasswordDto
    {

        [Required(ErrorMessage = "Email is required !!")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
