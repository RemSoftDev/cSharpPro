using System.Linq;
using CSharpPro.DataModels;
using System.Web.Mvc;
using CSharpPro.App_Start;

namespace CSharpPro.Controllers
{
	[BasicAuthorize]
	public partial class AdminController : Controller
	{
		private readonly DataContext _dataContext;
		public AdminController()
		{
			_dataContext = new DataContext();
		}

		public JsonResult Skills(string query = null)
		{
			var skills = _dataContext.Skills
				.AsNoTracking()
				.AsQueryable();

			if (!string.IsNullOrEmpty(query))
			{
				skills = skills.Where(x => x.Value.StartsWith(query));
			}

			var result = skills.Select(x => new { text = x.Value, value = x.Value }).ToList();

			return Json(result, JsonRequestBehavior.AllowGet);
		}
	}
}