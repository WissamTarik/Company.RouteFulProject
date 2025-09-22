using System.ComponentModel.DataAnnotations;

namespace Company.RouteFullProject.PL.Dtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage ="Code is Required !")]
        public string Code { get; set; }
        [Required(ErrorMessage ="Name is Required ! ")]
        public string Name { get; set; }
        [Required (ErrorMessage ="CreatedAt is Required ! ")]
        public DateTime? CreatedAt { get; set; }
    }
}
