using Microsoft.EntityFrameworkCore;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.DAL.Data;
using Route.C41.G03.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Route.C41.G03.BLL.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Add(Department entity)
        {
            _context.Add(entity);
            return _context.SaveChanges();
        }
        public int Update(Department entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _context.Remove(entity);
            return _context.SaveChanges();
        }

        public Department Get(int id)
        {
            //return _context.Departments.Find(id);

            return _context.Find<Department>(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.AsNoTracking().ToList();
        }


    }
}
