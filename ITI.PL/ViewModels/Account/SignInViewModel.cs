using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels.Account
{
	public class SignInViewModel
	{

		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		public bool RememberMe { get; set; }
    }
}
