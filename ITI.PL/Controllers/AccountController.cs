using AutoMapper;
using ITI.DAL.Models;
using ITI.PL.Services.EmailSender;
using ITI.PL.Services.SmsMessage;
using ITI.PL.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
// P@ssw0rd
namespace ITI.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;
		private readonly ISmsServices _smsServices;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IEmailSender emailSender,
			IConfiguration configuration,
			ISmsServices smsServices
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_configuration = configuration;
			_smsServices = smsServices;
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

		#region Sign Out

		[HttpGet]
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}

		#endregion

		#region Forget Password

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{

					var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

					var resetBssswordURL = Url.Action("ResetPassword", "Account", new { email = model.Email, token = resetPasswordToken }, "https", "localhost:7103");

					await _emailSender.SendAsync(
						from: _configuration["EmailSettings:SenderEmail"]!,
						recipients: model.Email,
						subject: "reset your password",
						body: resetBssswordURL!
						);

					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Not Have an Account");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["Email"] = email;
			TempData["Token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{

			if (ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["Token"] as string;
				var user = await _userManager.FindByEmailAsync(email!);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token!, model.NewPassword);
					RedirectToAction(nameof(SignIn));
				}
				else
					ModelState.AddModelError(string.Empty, "URL is not Valid");
			}
			return View(model);
		}

		#endregion

		#region Send Sms



		[HttpPost]
		public async Task<IActionResult> SendSms(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{

					var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

					var resetBssswordURL = Url.Action("ResetPassword", "Account", new { email = model.Email, token = resetPasswordToken }, "https", "localhost:7103");

					var sms = new SmsMessage()
					{
						Body = resetBssswordURL,
						PhoneNumber = user.PhoneNumber

					};

					 _smsServices.Send(
						sms
						);

					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Not Have an Account");
			}
			return View(model);
		}



		#endregion

	}
}
