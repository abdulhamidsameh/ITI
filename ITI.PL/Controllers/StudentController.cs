using ITI.BLL.Interfaces;
using ITI.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class StudentController : Controller
	{
		private readonly IStudentRepository _repository;

		public StudentController(IStudentRepository repository)
        {
			_repository = repository;
		}

		// BaseUrl/Student/Index
		[HttpGet]
        public IActionResult Index()
		{
			var students = _repository.GetAll();
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
				_repository.Add(model);
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
			var student = _repository.Get(id.Value);

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

			_repository.Update(model);
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
			_repository.Delete(model);
			return RedirectToAction(nameof(Index));
		}
	}
}
