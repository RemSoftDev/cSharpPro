using System.Linq;
using CSharpPro.DataModels;
using CSharpPro.ViewModels;
using System.Web.Mvc;
using CSharpPro.Extensions;

namespace CSharpPro.Controllers
{
	public partial class AdminController
	{
		[HttpGet]
		public ActionResult ProjectTypes()
		{
			var types = _dataContext.ProjectTypes.AsNoTracking().ToList();
			return View("ProjectTypes", types);
		}

		[HttpGet]
		public ActionResult ProjectType(int? id = null)
		{
			ProjectTypeViewModel type = null;
			if (id.HasValue)
			{
				var typeDto = _dataContext.ProjectTypes.AsNoTracking().First(x => x.Id == id.Value);
				type = new ProjectTypeViewModel
				{
					Id = typeDto.Id,
					Title = typeDto.Title
				};
			}
			return View("ProjectType", type);
		}

		[HttpPost]
		public ActionResult ProjectType(ProjectTypeViewModel model)
		{
			if (ModelState.IsValid)
			{
				ProjectType type = null;
				if (model.Id.HasValue)
				{
					type = _dataContext.ProjectTypes.First(x => x.Id == model.Id.Value);
					type.Title = model.Title;
				}
				else
				{
					type = new ProjectType{
						Title = model.Title
					};
					_dataContext.ProjectTypes.Add(type);
				}

				_dataContext.SaveChanges();

				return RedirectToAction("ProjectTypes");
			}
			else
			{
				return View("ProjectType", model);
			}
		}

		[HttpDelete]
		public ActionResult ProjectType(int id)
		{
			var type = _dataContext.ProjectTypes.FirstOrDefault(x => x.Id == id);
			if (type != null)
			{
				_dataContext.ProjectTypes.Remove(type);
				_dataContext.SaveChanges();
			}
			return new EmptyResult();
		}
	}
}