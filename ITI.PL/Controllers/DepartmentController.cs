using AutoMapper;
using ITI.BLL.Interfaces;
using ITI.BLL.Specifications;
using ITI.DAL.Models;
using ITI.PL.ViewModels;
using ITI.PL.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public DepartmentController(
			IUnitOfWork unitOfWork,
			IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		// baseUrl/Department/Index
		[HttpGet]
		public IActionResult Index()
		{
			var spec = new BaseSpecifications<Department>();
			spec.Includes.Add(D => D.Instructors);
			spec.Includes.Add(D => D.Students);
			spec.Includes.Add(D => D.Instructor!);
			var departments = _unitOfWork.Repository<Department>().GetWithSpecAll(spec);
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
				_unitOfWork.Repository<Department>().Add(department);
				TempData["Message"] = "Department Created Successfully";
				var count = _unitOfWork.Complete();
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
			return View(departmentVM);

		}

		// baseUrl/Department/Details/id?
		[HttpGet]
		public IActionResult Details(int? id, string viewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest();

			var spec = new BaseSpecifications<Department>(D => D.Id == id.Value);
			spec.Includes.Add(D => D.Instructors);
			spec.Includes.Add(D => D.Students);
			spec.Includes.Add(D => D.Instructor!);
			
			var department = _unitOfWork.Repository<Department>().GetWithSpec(spec);

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

			_unitOfWork.Repository<Department>().Update(department);
			TempData["Message"] = "Department Updated Successfully";
			
			var count = _unitOfWork.Complete();

			if(count > 0)
				return RedirectToAction(nameof(Index));


			return View(departmentVM);

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
			_unitOfWork.Repository<Department>().Delete(department);
			TempData["Message"] = "Department Deleted Successfully";
		
			var count =_unitOfWork.Complete();
			if (count > 0)
				return RedirectToAction(nameof(Index));
			
			return View(departmentVM);
		}
	}
}
