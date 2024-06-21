using ITI.BLL.Interfaces;
using ITI.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class StudentController : Controller
	{
		private readonly IStudentRepository _studentrepo;

		public StudentController(IStudentRepository StudentRepository)
        {
			_studentrepo = StudentRepository;
		}

		// BaseUrl/Student/Index
		[HttpGet]
        public IActionResult Index()
		{
			var students = _studentrepo.GetAll();
			return View(students);
		}

		// BaseUrl/Student/Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Student model) 
		{
			if (ModelState.IsValid)
			{
				_studentrepo.Add(model);
				TempData["Message"] = "Student Added Successfully";
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		// BaseUrl/Student/Details/id?
		[HttpGet]
		public IActionResult Details(int? id,string viewName="Details")
		{
			if (!id.HasValue)
				return BadRequest();
			var student = _studentrepo.Get(id.Value);

			if(student is null)
				return NotFound();
			return View(viewName,student);
		}

		// BaseUrl/Student/Edit/id?
		[HttpGet]
		public IActionResult Edit(int? id)
		{

			return Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Student model)
		{
			if(!ModelState.IsValid)
				return View(model);

			_studentrepo.Update(model);
			TempData["Message"] = "Student Updated Successfully";
			return RedirectToAction(nameof(Index));

		}

		// BaseUrl/Student/Delete/id?
		[HttpGet]
		public IActionResult Delete(int? id) 
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(Student model)
		{
			_studentrepo.Delete(model);
			TempData["Message"] = "Student Deleted Successfully";
			return RedirectToAction(nameof(Index));
		}
	}
}
