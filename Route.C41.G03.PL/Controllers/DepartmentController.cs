using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.DAL.Models;
using Route.C41.G03.PL.ViewModels;
using System;
using System.Collections.Generic;

namespace Route.C41.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _env;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment env)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var deptsmapped = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(deptsmapped);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var deptmapped = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(deptmapped);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(departmentVM);
        }


        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            var deptmapped = _mapper.Map<Department, DepartmentViewModel>(department);


            if (department is null)

                return NotFound();

            return View(viewName, deptmapped);
        }

        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //    return BadRequest();
            //var department = _departmentRepo.Get(id.Value);
            //if (Department is null)
            //    return NotFound();
            //return View(department);

            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var deptmapped = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.DepartmentRepository.Update(deptmapped);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating Department");

                return View(departmentVM);
            }


        }


        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var deptmapped = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.DepartmentRepository.Delete(deptmapped);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting Department");

                return View(departmentVM);
            }
        }
    }
}
