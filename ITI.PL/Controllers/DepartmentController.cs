using ITI.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITI.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _departmentRepo;

		public DepartmentController(IDepartmentRepository departmentRepo)
        {
			_departmentRepo = departmentRepo;
		}

		// baseUrl/Department/Index
		[HttpGet]
        public IActionResult Index()
		{
			var departments = _departmentRepo.GetAll();
			return View(departments);
		}
	}
}
