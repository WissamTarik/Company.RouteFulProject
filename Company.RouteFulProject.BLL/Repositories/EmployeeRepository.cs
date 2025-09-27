using Company.RouteFullProject.DAL.Data.Contexts;
using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.RouteFulProject.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext companyDbContext):base(companyDbContext)
        {
            _context = companyDbContext;
        }


        //private readonly CompanyDbContext _context;
        //public EmployeeRepository(CompanyDbContext companyDbContext)
        //{
        //    _context = companyDbContext;

        //}
        //public IEnumerable<Employee> GetAllEmployees()
        //{
        //    return _context.Employees.ToList();
        //}

        //public Employee? GetEmployeeById(int id)
        //{
        //    return _context.Employees.Find(id);
        //}

        //public int AddEmployee(Employee model)
        //{
        //    _context.Employees.Add(model);
        //    return _context.SaveChanges();
        //}

        //public int UpdateEmployee(Employee model)
        //{
        //    _context.Employees.Update(model);
        //    return _context.SaveChanges();
        //}
        //public int DeleteEmployee(Employee model)
        //{
        //    _context.Employees.Remove(model);
        //    return _context.SaveChanges();
        //}

        public List<Employee>? GetByName(string name)
        {
           return  _context.Employees.Include(e=>e.Department)
                   .Where(e=>e.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
