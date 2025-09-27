using Company.RouteFulProject.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.RouteFullProject.DAL.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
        //[DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }
        //[DisplayName("Date of creation")]
        public DateTime CreatedAt { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }


    }
}
