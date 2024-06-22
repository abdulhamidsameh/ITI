using AutoMapper;
using ITI.BLL.Interfaces;
using ITI.DAL.Models;
using ITI.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IGenericRepository<Department> _departmentRepo;
		private readonly IMapper _mapper;

		public DepartmentController(
			IGenericRepository<Department> departmentRepo,
			IMapper mapper)
		{
			_departmentRepo = departmentRepo;
			_mapper = mapper;
		}

		// baseUrl/Department/Index
		[HttpGet]
		public IActionResult Index()
		{
			var departments = _departmentRepo.GetAll();
			var departmentsVM = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
			return View(departmentsVM);
		}

		// baseUrl/Department/Create
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
				var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
				var Count = _departmentRepo.Add(department);
				if (Count > 0)
				{
					TempData["Message"] = "Department Created Successfully";
					return RedirectToAction(nameof(Index));
				}
			}
			return View(departmentVM);

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

			var departmentVM = _mapper.Map<Department, DepartmentViewModel>(department);

			return View(viewName, departmentVM);

		}

		// BaseUrl/Department/Edit/id?
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(DepartmentViewModel departmentVM)
		{
			if (!ModelState.IsValid)
				return View(departmentVM);

			var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

			_departmentRepo.Update(department);
			TempData["Message"] = "Department Updated Successfully";

			return RedirectToAction(nameof(Index));

		}

		// BaseUrl/Department/Delete/id?
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(DepartmentViewModel departmentVM)
		{
			var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
			_departmentRepo.Delete(department);
			TempData["Message"] = "Department Deleted Successfully";

			return RedirectToAction(nameof(Index));
		}
	}
}
