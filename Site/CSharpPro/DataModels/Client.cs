using System.Collections.Generic;

namespace CSharpPro.DataModels
{
	public class Client
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }
		public string ImageUrl { get; set; }
		public string Testimonial { get; set; }
		public bool HappyClient { get; set; }
		public List<Project> Projects { get; set; }
	}
}