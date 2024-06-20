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
			return View();
		}
	}
}
