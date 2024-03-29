using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.DAL.Models;
using Route.C41.G03.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route.C41.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepo, IMapper mapper, IHostEnvironment env)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            //_departmentRepo = departmentRepo;
            _env = env;
        }
        public IActionResult Index(string searchInp)
        {
            TempData.Keep();
            var Employess = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInp))
            {
                Employess = _employeeRepo.GetAll();

            }
            else
            {
                Employess = _employeeRepo.GetEmployeesByName(searchInp.ToLower());
            }
            var EmpsMapped = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employess);

            return View(EmpsMapped);
        }

        public IActionResult Create()
        {
            //ViewBag.Departments = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {

            if (ModelState.IsValid)
            {
                var EmpMapped = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                var count = _employeeRepo.Add(EmpMapped);
                if (count > 0)

                    TempData["Message"] = "Employee created";

                else
                    TempData["Message"] = "An Error Occured :(";
                return RedirectToAction(nameof(Index));

            }
            return View(employeeVM);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var Employee = _employeeRepo.Get(id.Value);

            var EmpMapped = _mapper.Map<Employee, EmployeeViewModel>(Employee);

            if (Employee is null)
                return NotFound();


            return View(viewName, EmpMapped);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employeeVM);
            try
            {
                var EmpMapped = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                var count = _employeeRepo.Update(EmpMapped);
                if (count > 0)

                    TempData["Message"] = "Employee Edited";

                else
                    TempData["Message"] = "An Error Occured :(";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating Employee");

                return View(employeeVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var EmpMapped = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _employeeRepo.Delete(EmpMapped);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting Employee");

                return View(employeeVM);
            }
        }
    }
}
