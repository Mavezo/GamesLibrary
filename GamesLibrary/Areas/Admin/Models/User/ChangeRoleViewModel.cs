using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Areas.Admin.Models.User
{
	public class ChangeRoleViewModel
	{
		public string Id { get; set; }
		//[BindNever]
		public IList<string>? AllRoles { get; set; } 
		public IList<string> UserRoles { get; set; }
	}
}
