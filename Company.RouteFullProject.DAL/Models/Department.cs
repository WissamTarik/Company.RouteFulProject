using Company.RouteFulProject.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.RouteFullProject.DAL.Models
{
    public class Department:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        [DisplayName("Date of creation")]
        public DateTime? CreatedAt { get; set; }

    }
}
