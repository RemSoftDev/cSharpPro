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
		public ActionResult Services()
		{
			var clients = _dataContext.Services
				.AsNoTracking()
				.OrderBy(x=>x.Title)
				.ToList();
			return View("Services", clients);
		}

		[HttpGet]
		public ActionResult Service(int? id = null)
		{
			ServiceViewModel client = null;
			if (id.HasValue)
			{
				var clientDto = _dataContext.Services.AsNoTracking().First(x => x.Id == id.Value);
				client = new ServiceViewModel
				{
					Id = clientDto.Id,
					Title = clientDto.Title,
					Description = clientDto.Description,
					ShortDescription = clientDto.ShortDescription,
					ImageUrl = clientDto.ImageUrl
				};
			}
			return View("Service", client);
		}

		[HttpPost]
		public ActionResult Service(ServiceViewModel model)
		{
			if (model.Image != null)
			{
				model.ImageUrl = Server.SaveFile(model.Image);
			}

			if (ModelState.IsValid)
			{
				Service client = null;
				if (model.Id.HasValue)
				{
					client = _dataContext.Services.First(x => x.Id == model.Id.Value);
					client.Title = model.Title;
					client.Description = model.Description;
					client.ShortDescription = model.ShortDescription;

					if (!string.IsNullOrEmpty(client.ImageUrl) && client.ImageUrl != model.ImageUrl)
					{
						Server.DeleteFile(client.ImageUrl);
					}

					client.ImageUrl = model.ImageUrl;
				}
				else
				{
					client = new Service
					{
						Title = model.Title,
						ShortDescription = model.ShortDescription,
						Description = model.Description,
						ImageUrl = model.ImageUrl
					};
					_dataContext.Services.Add(client);
				}

				_dataContext.SaveChanges();

				return RedirectToAction("Services");
			}
			else
			{
				return View("Service", model);
			}
		}

		[HttpDelete]
		public ActionResult Service(int id)
		{
			var client = _dataContext.Services.FirstOrDefault(x => x.Id == id);
			if (client != null)
			{
				_dataContext.Services.Remove(client);
				_dataContext.SaveChanges();
			}
			return new EmptyResult();
		}
	}
}