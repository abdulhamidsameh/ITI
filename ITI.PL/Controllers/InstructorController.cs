using AutoMapper;
using ITI.BLL.Interfaces;
using ITI.BLL.Specifications;
using ITI.DAL.Models;
using ITI.PL.ViewModels.Instructor;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class InstructorController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public InstructorController(IUnitOfWork unitOfWork,
			IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var spec = new BaseSpecifications<Instructor>();
			spec.Includes.Add(I => I.Department!);
			spec.Includes.Add(I => I.Courses);

			var Instructors = _unitOfWork.Repository<Instructor>().GetWithSpecAll(spec);

			var InstructorsVM = _mapper.Map<IEnumerable<Instructor>, IEnumerable<InstructorViewModel>>(Instructors);

			return View(InstructorsVM);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(InstructorViewModel model)
		{
			if (ModelState.IsValid)
			{
				var instructor = _mapper.Map<InstructorViewModel, Instructor>(model);

				_unitOfWork.Repository<Instructor>().Add(instructor);

				TempData["Message"] = "Instructor Added Successfully";

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

			var spec = new BaseSpecifications<Instructor>(I => I.Id == id.Value);
			spec.Includes.Add(I => I.Department!);
			spec.Includes.Add(I => I.Courses);

			var instructor = _unitOfWork.Repository<Instructor>().GetWithSpec(spec);

			if (instructor is null)
				return NotFound();

			var instructorVM = _mapper.Map<Instructor, InstructorViewModel>(instructor);

			return View(ViewName, instructorVM);
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id,"Edit");
		}

		[HttpPost]
		public IActionResult Edit(InstructorViewModel model)
		{
			if(!ModelState.IsValid)
				return View(model);

			var instructor = _mapper.Map<InstructorViewModel,Instructor>(model);

			_unitOfWork.Repository<Instructor>().Update(instructor);

			TempData["Message"] = "Instructor Updated Successfully";

			var count = _unitOfWork.Complete();

			if(count > 0)
				return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public IActionResult Delete(int? id) 
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(InstructorViewModel model)
		{
			var instructor = _mapper.Map<InstructorViewModel, Instructor>(model);

			_unitOfWork.Repository<Instructor>().Delete(instructor);

			TempData["Message"] = "Instructor Deleted Successfully";

			var count = _unitOfWork.Complete();	

			if( count > 0)
				return RedirectToAction(nameof(Index));

			return View(model);

		}


	}
}
