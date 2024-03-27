using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Route.C41.G03.DAL.Models
{
    public class Department : ModelBase
    {


        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
