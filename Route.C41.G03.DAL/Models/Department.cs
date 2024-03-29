using System;
using System.Collections.Generic;

namespace Route.C41.G03.DAL.Models
{
    public class Department : ModelBase
    {



        public string Code { get; set; }


        public string Name { get; set; }


        public DateTime DateOfCreation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
