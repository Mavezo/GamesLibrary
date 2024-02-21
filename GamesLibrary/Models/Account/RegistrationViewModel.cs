using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Models.Account
{
	public class RegistrationViewModel
	{
		[Required]
		[Display(Name = "Nickname")]
		public string Nickname { get; set; }

		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords mismatch")]
		[Display(Name = "Confirm password")]
		public string ConfirmPassword { get; set; }


	}
}
