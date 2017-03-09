using System.Collections.Generic;

namespace CSharpPro.DataModels
{
	public class ProjectType
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<Project> Projects { get; set; }
	}
}