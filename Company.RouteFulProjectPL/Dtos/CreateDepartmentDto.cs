using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.RouteFullProject.PL.Dtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage ="Code is Required !")]
        [RegularExpression(@"^[0-9a-zA-Z]{2,}$"
                        ,ErrorMessage ="Code must be at least 2 " +
                         "char and not include any special Characters")]
        public string Code { get; set; }
        [Required(ErrorMessage ="Name is Required ! ")]
        public string Name { get; set; }
        [Required (ErrorMessage ="CreatedAt is Required ! ")]
        [DisplayName("Date of creation")]
        public DateTime? CreatedAt { get; set; }
    }
}
