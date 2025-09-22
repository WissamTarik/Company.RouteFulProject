using Company.RouteFullProject.DAL.Data.Contexts;
using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.RouteFulProject.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _Context;
        public DepartmentRepository(CompanyDbContext context)
        {
            _Context=context;
        }
        public IEnumerable<Department> GetAllDepartment()
        {
            var Result = _Context.Departments.ToList();
            return Result;
            
        }

        public Department? GetDepartmentById(int id)
        {
            var Department = _Context.Departments.Find(id);
            return Department;
        }

        public int AddDepartment(Department model)
        {
           _Context.Departments.Add(model);
            return _Context.SaveChanges();
        }

        public int UpdateDepartment(Department model)
        {
            _Context.Departments.Update(model);
            return _Context.SaveChanges();
        }
        public int DeleteDepartment(Department model)
        {
            _Context.Departments.Remove(model);
            return _Context.SaveChanges();
        }

      
       
    }
}
