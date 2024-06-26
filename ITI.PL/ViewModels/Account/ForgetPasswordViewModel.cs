using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels.Account
{
	public class ForgetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;
	}
}
