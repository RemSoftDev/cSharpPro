using System.Web;
using System.Web.Mvc;

namespace CSharpPro.ViewModels
{
	public class ServiceViewModel
	{
		public int? Id { get; set; }

		public string Title { get; set; }

		public string ShortDescription { get; set; }

		[AllowHtml]
		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public HttpPostedFileBase Image { get; set; }
	}
}