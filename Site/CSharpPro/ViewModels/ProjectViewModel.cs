using CSharpPro.DataModels;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CSharpPro.ViewModels
{
	public class ProjectViewModel
	{
		public int? Id { get; set; }

		public string Title { get; set; }

		[AllowHtml]
		public string Description { get; set; }

		public string Link { get; set; }
		public string InitialSkills { get; set; }
		public string Skills { get; set; }

		public string ImageUrl { get; set; }
		public HttpPostedFileBase Image { get; set; }

		public List<int> SelectedTypeList { get; set; }
		public List<ProjectType> TypeList { get; set; }

		public List<int> SelectedClientList { get; set; }
		public List<Client> ClientList { get; set; }
	}
}