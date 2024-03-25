using Route.C41.G03.DAL.Models;
using System.Collections.Generic;

namespace Route.C41.G03.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        int Add(Employee entity);
        int Update(Employee entity);
        int Delete(Employee entity);
    }
}
