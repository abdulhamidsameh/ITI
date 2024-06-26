using AutoMapper;
using ITI.BLL.Interfaces;
using ITI.DAL.Models;
using ITI.PL.ViewModels.Topic;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class TopicController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public TopicController(IUnitOfWork unitOfWork,
			IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var topics = _unitOfWork.Repository<Topic>().GetAll();
			var topicsVM = _mapper.Map<IEnumerable<Topic>, IEnumerable<TopicViewModel>>(topics);
			return View(topicsVM);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(TopicViewModel model)
		{
			if (ModelState.IsValid)
			{
				var topic = _mapper.Map<TopicViewModel, Topic>(model);

				_unitOfWork.Repository<Topic>().Add(topic);

				TempData["Message"] = "Topic Added Successfully";

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

			var Topic = _unitOfWork.Repository<Topic>().Get(id.Value);

			if (Topic is null)
				return NotFound();

			var TopicVM = _mapper.Map<Topic, TopicViewModel>(Topic);

			return View(ViewName, TopicVM);

		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id, "Edit");
		}

		[HttpPost]
		public IActionResult Edit(TopicViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var Topic = _mapper.Map<TopicViewModel, Topic>(model);

			_unitOfWork.Repository<Topic>().Update(Topic);

			TempData["Message"] = "Topic Updated Successfully";

			var count = _unitOfWork.Complete();

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
		public IActionResult Delete(TopicViewModel model)
		{
			var topic = _mapper.Map<TopicViewModel, Topic>(model);

			_unitOfWork.Repository<Topic>().Delete(topic);

			TempData["Message"] = "Topic Deleted Successfully";

			var count = _unitOfWork.Complete();

			if (count > 0)
				return RedirectToAction(nameof(Index));


			return View(model);
		}
	}
}
