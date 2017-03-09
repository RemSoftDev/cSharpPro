using System.Collections.Generic;
using CSharpPro.DataModels;

namespace CSharpPro.ViewModels
{
	public class PortfolioProjectViewModel
	{
		public Project Project{get;set;}
		public List<Project> RelatedProjects{get;set;}
	}
}