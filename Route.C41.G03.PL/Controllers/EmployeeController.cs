using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;
using Route.C41.G03.DAL.Models;
using Route.C41.G03.PL.Helpers;
using Route.C41.G03.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route.C41.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IHostEnvironment _env;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_departmentRepo = departmentRepo;
            _env = env;
        }
        public IActionResult Index(string searchInp)
        {
            TempData.Keep();
            var Employess = Enumerable.Empty<Employee>();
            var empRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;
            if (string.IsNullOrEmpty(searchInp))
            {
                Employess = empRepo.GetAll();

            }
            else
            {
                Employess = empRepo.GetEmployeesByName(searchInp.ToLower());
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

            employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

            if (ModelState.IsValid)
            {
                var EmpMapped = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.Repository<Employee>().Add(EmpMapped);
                var count = _unitOfWork.Complete();

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

            var Employee = _unitOfWork.Repository<Employee>().Get(id.Value);

            var EmpMapped = _mapper.Map<Employee, EmployeeViewModel>(Employee);

            if (Employee is null)
                return NotFound();

            if (viewName.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = Employee.ImageName;


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

                _unitOfWork.Repository<Employee>().Update(EmpMapped);
                var count = _unitOfWork.Complete();

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
                employeeVM.ImageName = TempData["ImageName"] as string;
                var EmpMapped = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.Repository<Employee>().Delete(EmpMapped);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
                return View(employeeVM);
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
