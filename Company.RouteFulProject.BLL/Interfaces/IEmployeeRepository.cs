using Company.RouteFullProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.RouteFulProject.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        //IEnumerable<Employee> GetAllEmployees();
        //Employee? GetEmployeeById(int id);
        //int AddEmployee(Employee model);
        //int UpdateEmployee(Employee model);
        //int DeleteEmployee(Employee model);

        List<Employee> ?GetByName(string  name);   
    }
}
