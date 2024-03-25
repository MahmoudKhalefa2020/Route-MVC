using System;
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
    }
}
