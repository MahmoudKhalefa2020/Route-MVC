using System;
using System.Runtime.Serialization;

namespace Route.C41.G03.DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }
    public enum EmpType
    {
        Fulltime = 1,
        Parttime = 2
    }
    public class Employee : ModelBase
    {


        public string Name { get; set; }

        public int? Age { get; set; }


        public string Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }


        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;


        public Gender Gender { get; set; }
        public EmpType EmployeeType { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }


    }
}
