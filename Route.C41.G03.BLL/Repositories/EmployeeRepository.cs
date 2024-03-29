using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.DAL.Data;
using Route.C41.G03.DAL.Models;
using System.Linq;

namespace Route.C41.G03.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)

        {

        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(e => e.Address.ToLower() == address.ToLower());
        }

        public IQueryable<Employee> GetEmployeesByName(string name)
        {
            return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));
        }
    }
}
