using System.Web;

namespace CSharpPro.ViewModels
{
	public class TeamMemberViewModel
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public string Position { get; set; }

		public string ImageUrl { get; set; }

		public HttpPostedFileBase Image { get; set; }
	}
}