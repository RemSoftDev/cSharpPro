using System.Collections.Generic;
using CSharpPro.DataModels;

namespace CSharpPro.ViewModels
{
	public class PortfolioViewModel
	{
		public List<Project> Projects{get;set;}
		public List<ProjectType> ProjectTypes{get;set;}
	}
}