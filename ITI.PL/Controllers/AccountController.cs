using AutoMapper;
using ITI.DAL.Models;
using ITI.PL.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
// P@ssw0rd
namespace ITI.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region Sign Up

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{

				var user = await _userManager.FindByNameAsync(model.UserName);

				if (user is null)
				{
					user = new ApplicationUser()
					{
						UserName = model.UserName,
						Email = model.Email,
						IsAgree = model.IsAgree,
						FirstName = model.FirstName,
						LastName = model.LastName,
					};
					var result = await _userManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

				}
				else
					ModelState.AddModelError(string.Empty, "this username is already in use for anouther account");

			}
			return View(model);
		}

		#endregion

		#region Sign In

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		#endregion

	}
}
