using ITI.BLL.Interfaces;
using ITI.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IGenericRepository<Department> _departmentRepo;

		public DepartmentController(
			IGenericRepository<Department> departmentRepo)
		{
			_departmentRepo = departmentRepo;
		}

		// baseUrl/Department/Index
		[HttpGet]
		public IActionResult Index()
		{
			var departments = _departmentRepo.GetAll();
			return View(departments);
		}

		// baseUrl/Department/Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Department model)
		{
			if (ModelState.IsValid)
			{
				var Count = _departmentRepo.Add(model);
				if (Count > 0)
					return RedirectToAction(nameof(Index));
			}
			return View(model);

		}

		// baseUrl/Department/Details/id?
		[HttpGet]
		public IActionResult Details(int? id, string viewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest();

			var department = _departmentRepo.Get(id.Value);

			if (department is null)
				return NotFound();

			return View(viewName, department);

		}

		// BaseUrl/Department/Edit/id?
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Department model)
		{
			if (!ModelState.IsValid)
				return View(model);

			_departmentRepo.Update(model);
			return RedirectToAction(nameof(Index));

		}

		// BaseUrl/Department/Delete/id?
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(Department model)
		{
			_departmentRepo.Delete(model);
			return RedirectToAction(nameof(Index));
		}
	}
}
