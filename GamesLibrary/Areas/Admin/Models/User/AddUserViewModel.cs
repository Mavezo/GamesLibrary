 using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Areas.Admin.Models.User
{
	public class AddUserViewModel
	{
		[Required]
		[Display(Name ="Nickname")]
		public string UserName { get; set; }

		[Required]
		[Display(Name ="Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name ="Password")]
		public string Password { get; set; }	
		
		[Required]
		[DataType(DataType.Password)]
		[Display(Name ="RepeatPassword")]
		[Compare("Password", ErrorMessage = "Password mismatch")]
		public string RepeatPassword { get; set; }

		[Display(Name = "User Roles")]
		public List<string> UserRoles { get; set; } = default!;

		public List<IdentityRole> AllRoles { get; set; } = default!;

	}
}
