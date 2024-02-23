using Microsoft.AspNetCore.Identity;

namespace GamesLibrary.Areas.Admin.Models.User
{
	public class ChangeRolesModel
	{
		public string Id { get; set; }
		public IList<string> AllRoles { get; set; }
		public IList<string> CheckedRoles { get; set; }	

	}
}
