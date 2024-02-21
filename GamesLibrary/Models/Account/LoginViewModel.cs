using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Models.Account
{
	public class LoginViewModel
	{
		[Required]
		[Display(Name = "Nickname")]
		public string Nickname { get; set; }

		[Required]
		[Display(Name ="Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name ="Remember me?")]
		public bool RememberMe { get; set; }

		public string? ReturnURL { get; set; }
	}
}
