using Company.RouteFullProject.DAL.Data.Contexts;
using Company.RouteFulProject.BLL.Interfaces;
using Company.RouteFulProject.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.RouteFulProject.BLL
{
    public class UnitOfWork : IUnitOfWork,IAsyncDisposable
    {
        private readonly CompanyDbContext _context;
        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public async  Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

      
        public async ValueTask DisposeAsync()
        {
             await _context.DisposeAsync();
        }
    }
}
