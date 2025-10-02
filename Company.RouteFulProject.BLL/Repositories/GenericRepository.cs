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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof (T)==typeof(Employee))
                return (IEnumerable<T>)  await _context.Employees.Include(e=>e.Department).ToListAsync();
            
           
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T>? GetByIdAsync(int id)

        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id) as T;
            }
            else if (typeof(T) == typeof(Department))
                return await _context.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == id) as T;
            return  await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
        }
        public void Update(T model)
        {
            _context.Set<T>().Update(model);
        }
        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
        }

    
       
    }
}
