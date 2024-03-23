using Route.C41.G03.DAL.Models;
using System.Collections.Generic;

namespace Route.C41.G03.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department Get(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);
    }
}
