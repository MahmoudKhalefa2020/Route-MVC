using Route.C41.G03.DAL.Models;
using System.Linq;

namespace Route.C41.G03.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeesByAddress(string address);
        IQueryable<Employee> GetEmployeesByName(string name);

    }
}
