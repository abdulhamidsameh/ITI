using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels.Account
{
	public class ResetPasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		[MinLength(5)]
		public string NewPassword { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		[MinLength(5)]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't Match With Password")]
		public string ConfirmPassword { get; set; } = null!;
	}
}
