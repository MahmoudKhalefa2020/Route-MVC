using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.DAL.Models;
using System;

namespace Route.C41.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepo, IHostEnvironment env)
        {
            _employeeRepo = employeeRepo;
            _env = env;
        }
        public IActionResult Index()
        {
            TempData.Keep();
            var Employess = _employeeRepo.GetAll();
            return View(Employess);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                if (count > 0)

                    TempData["Message"] = "Employee created";

                else
                    TempData["Message"] = "An Error Occured :(";
                return RedirectToAction(nameof(Index));

            }
            return View(employee);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = _employeeRepo.Get(id.Value);
            if (Employee is null)
                return NotFound();


            return View(viewName, Employee);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employee);
            try
            {
                var count = _employeeRepo.Update(employee);
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

                return View(employee);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepo.Delete(employee);

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

                return View(employee);
            }
        }
    }
}
