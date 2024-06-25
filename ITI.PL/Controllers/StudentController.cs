using AutoMapper;
using ITI.BLL.Interfaces;
using ITI.DAL.Models;
using ITI.PL.Helpers;
using ITI.PL.ViewModels.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace ITI.PL.Controllers
{
	[Authorize]
	public class StudentController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public StudentController(
			IUnitOfWork unitOfWork,
			IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		// BaseUrl/Student/Index
		[HttpGet]
		public IActionResult Index()
		{
			var students = _unitOfWork.Repository<Student>().GetAll();
			var studentsVM = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(students);
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
			if (studentVM.Image is not null)
			{
				var imageName = DocumentSetting.UploadFile(studentVM.Image, "Images");

				studentVM.ImageName = imageName;
			}


			if (ModelState.IsValid)
			{
				var model = _mapper.Map<StudentViewModel, Student>(studentVM);
				_unitOfWork.Repository<Student>().Add(model);
				TempData["Message"] = "Student Added Successfully";
				var count = _unitOfWork.Complete();
				if (count > 0)
					return RedirectToAction(nameof(Index));


			}
			return View(studentVM);
		}

		// BaseUrl/Student/Details/id?
		[HttpGet]
		public IActionResult Details(int? id, string viewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest();
			var student = _unitOfWork.Repository<Student>().Get(id.Value);

			if (student is null)
				return NotFound();
			var studentVM = _mapper.Map<Student, StudentViewModel>(student);
			return View(viewName, studentVM);
		}

		// BaseUrl/Student/Edit/id?
		[HttpGet]
		public IActionResult Edit(int? id)
		{

			return Details(id, "Edit");
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public IActionResult Edit(StudentViewModel studentVM)
		{
			studentVM.ImageName = TempData["ImageName"] as string;
			if (!ModelState.IsValid)
				return View(studentVM);

			var student = _mapper.Map<StudentViewModel, Student>(studentVM);
			_unitOfWork.Repository<Student>().Update(student);
			TempData["Message"] = "Student Updated Successfully";

			var count = _unitOfWork.Complete();
			if (count > 0)
				return RedirectToAction(nameof(Index));

			return View(studentVM);

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
			studentVM.ImageName = TempData["ImageName"] as string;
			var student = _mapper.Map<StudentViewModel, Student>(studentVM);
			_unitOfWork.Repository<Student>().Delete(student);
			TempData["Message"] = "Student Deleted Successfully";

			var count = _unitOfWork.Complete();
			if (count > 0)
			{
				if (studentVM.ImageName is not null)
					DocumentSetting.DeleteFile(studentVM.ImageName!, "Images");

				return RedirectToAction(nameof(Index));
			}

			return View(studentVM);
		}
	}
}
