using Route.C41.G03.DAL.Models;
using System;

namespace Route.C41.G03.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : ModelBase;

        int Complete();

    }
}
