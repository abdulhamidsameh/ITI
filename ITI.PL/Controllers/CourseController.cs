using AutoMapper;
using ITI.BLL.Interfaces;
using ITI.BLL.Specifications;
using ITI.DAL.Models;
using ITI.PL.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ITI.PL.Controllers
{
	public class CourseController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CourseController(IUnitOfWork unitOfWork,
			IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var spec = new BaseSpecifications<Course>();
			spec.Includes.Add(C => C.Instructors);
			spec.Includes.Add(C => C.Topic!);
			spec.Includes.Add(C => C.Students);
			var courses = _unitOfWork.Repository<Course>().GetWithSpecAll(spec);
			var coursesVM = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseViewModel>>(courses);
			return View(coursesVM);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CourseViewModel model)
		{
			if (ModelState.IsValid)
			{
				var course = _mapper.Map<CourseViewModel, Course>(model);

				_unitOfWork.Repository<Course>().Add(course);

				TempData["Message"] = "Course Added Successfully";

				var count = _unitOfWork.Complete();

				if (count > 0)
					return RedirectToAction(nameof(Index));



			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Details(int? id, string ViewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest();

			var spec = new BaseSpecifications<Course>(C => C.Id == id.Value);
			spec.Includes.Add(C => C.Instructors);
			spec.Includes.Add(C => C.Topic!);
			spec.Includes.Add(C => C.Students);
			var course = _unitOfWork.Repository<Course>().GetWithSpec(spec);

			if (course is null)
				return NotFound();

			var courseVM = _mapper.Map<Course, CourseViewModel>(course);


			return View(ViewName, courseVM);
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id, "Edit");
		}

		[HttpPost]
		public IActionResult Edit(CourseViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var course = _mapper.Map<CourseViewModel, Course>(model);

			_unitOfWork.Repository<Course>().Update(course);

			var count = _unitOfWork.Complete();

			TempData["Message"] = "Course Updated Successfully";

			if (count > 0)
				return RedirectToAction(nameof(Index));


			return View(model);
		}


		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(CourseViewModel model)
		{
			if (ModelState.IsValid)
			{
				var course = _mapper.Map<CourseViewModel, Course>(model);

				_unitOfWork.Repository<Course>().Delete(course);

				TempData["Message"] = "Course Deleted Successfully";

				var count = _unitOfWork.Complete();

				if (count > 0)
					return RedirectToAction(nameof(Index));

			}
			return View(model);
		}
	}
}
