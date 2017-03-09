using System.Web;

namespace CSharpPro.ViewModels
{
	public class ClientViewModel
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public string Link { get; set; }

		public string Testimonial { get; set; }

		public bool HappyClient { get; set; }

		public string ImageUrl { get; set; }

		public HttpPostedFileBase Image { get; set; }
	}
}