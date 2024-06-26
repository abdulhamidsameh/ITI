using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels.Account
{
	public class SignUpViewModel
	{
		[Required]
		public string UserName { get; set; } = null!;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[Display(Name ="First Name")]
		public string FirstName { get; set; } = null!;

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		[MinLength(5)]
		public string Password { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		[MinLength(5)]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password doesn't Match With Password")]
		public string ConfirmPassword { get; set; } = null!;

		public bool IsAgree { get; set; }
	}
}
