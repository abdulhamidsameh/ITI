using AutoMapper;
using ITI.DAL.Models;
using ITI.PL.ViewModels.Account;
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

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{

			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);

					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

						// account block
						if (result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Account Is Locked");

						// success
						if (result.Succeeded)
							return RedirectToAction(nameof(HomeController.Index), "Home");

						// not confirm account
						if (result.IsNotAllowed)
							ModelState.AddModelError(string.Empty, "Please Confirm Your Account First");


					}


				}

				ModelState.AddModelError(string.Empty, "Invalid Login");

			}
			return View(model);
		}

		#endregion

	}
}
