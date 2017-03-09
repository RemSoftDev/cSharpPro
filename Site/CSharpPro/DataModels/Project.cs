using System.Collections.Generic;

namespace CSharpPro.DataModels
{
	public class Project
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public string ImageUrl { get; set; }
		public string Skills { get; set; }

		public List<Client> Clients { get; set; }
		public List<ProjectType> Types { get; set; }
	}
}