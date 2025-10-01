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
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        private readonly CompanyDbContext _companyDbContext;

        public DepartmentRepository(CompanyDbContext companyDbContext):base(companyDbContext)
        {
            _companyDbContext = companyDbContext;
        }

        public async Task<List<Department>>? GetDepartmentsByNameAsync(string name)
        {
            return await _companyDbContext.Departments.Where(d => d.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
        //private readonly CompanyDbContext _Context;
        //public DepartmentRepository(CompanyDbContext context)
        //{
        //    _Context=context;
        //}
        //public IEnumerable<Department> GetAllDepartment()
        //{
        //    var Result = _Context.Departments.ToList();
        //    return Result;

        //}

        //public Department? GetDepartmentById(int id)
        //{
        //    var Department = _Context.Departments.Find(id);
        //    return Department;
        //}

        //public int AddDepartment(Department model)
        //{
        //   _Context.Departments.Add(model);
        //    return _Context.SaveChanges();
        //}

        //public int UpdateDepartment(Department model)
        //{
        //    _Context.Departments.Update(model);
        //    return _Context.SaveChanges();
        //}
        //public int DeleteDepartment(Department model)
        //{
        //    _Context.Departments.Remove(model);
        //    return _Context.SaveChanges();
        //}



    }
}
