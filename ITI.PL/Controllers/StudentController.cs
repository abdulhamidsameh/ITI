using AutoMapper;
using ITI.BLL.Interfaces;
using ITI.DAL.Models;
using ITI.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class StudentController : Controller
	{
		private readonly IStudentRepository _studentrepo;
		private readonly IMapper _mapper;

		public StudentController(IStudentRepository StudentRepository,
			IMapper mapper)
        {
			_studentrepo = StudentRepository;
			_mapper = mapper;
		}

		// BaseUrl/Student/Index
		[HttpGet]
        public IActionResult Index()
		{
			var students = _studentrepo.GetAll();
			var studentsVM = _mapper.Map<IEnumerable<Student>,IEnumerable<StudentViewModel>>(students);
			return View(studentsVM);
		}

		// BaseUrl/Student/Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(StudentViewModel studentVM)
		{
			if (ModelState.IsValid)
			{
				var model = _mapper.Map<StudentViewModel,Student>(studentVM);
				_studentrepo.Add(model);
				TempData["Message"] = "Student Added Successfully";
				return RedirectToAction(nameof(Index));
			}
			return View(studentVM);
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
			var studentVM = _mapper.Map<Student,StudentViewModel>(student);
			return View(viewName, studentVM);
		}

		// BaseUrl/Student/Edit/id?
		[HttpGet]
		public IActionResult Edit(int? id)
		{

			return Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(StudentViewModel studentVM)
		{
			if(!ModelState.IsValid)
				return View(studentVM);

			var student = _mapper.Map<StudentViewModel,Student>(studentVM);
			_studentrepo.Update(student);
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
		public IActionResult Delete(StudentViewModel studentVM)
		{
			var student = _mapper.Map<StudentViewModel,Student>(studentVM);
			_studentrepo.Delete(student);
			TempData["Message"] = "Student Deleted Successfully";
			return RedirectToAction(nameof(Index));
		}
	}
}
