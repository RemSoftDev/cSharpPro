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
		public ActionResult Clients()
		{
			var clients = _dataContext.Clients
				.AsNoTracking()
				.OrderBy(x=>x.Name)
				.ToList();
			return View("Clients", clients);
		}

		[HttpGet]
		public ActionResult Client(int? id = null)
		{
			ClientViewModel client = null;
			if (id.HasValue)
			{
				var clientDto = _dataContext.Clients.AsNoTracking().First(x => x.Id == id.Value);
				client = new ClientViewModel
				{
					Id = clientDto.Id,
					Link = clientDto.Link,
					Name = clientDto.Name,
					ImageUrl = clientDto.ImageUrl,
					Testimonial= clientDto.Testimonial,
					HappyClient = clientDto.HappyClient
				};
			}
			return View("Client", client);
		}

		[HttpPost]
		public ActionResult Client(ClientViewModel model)
		{
			if (model.Image != null)
			{
				model.ImageUrl = Server.SaveFile(model.Image);
			}

			if (ModelState.IsValid)
			{
				Client client = null;
				if (model.Id.HasValue)
				{
					client = _dataContext.Clients.First(x => x.Id == model.Id.Value);
					client.Name = model.Name;
					client.Link = model.Link;

					client.Testimonial = model.Testimonial;
					client.HappyClient = model.HappyClient;
					if (!string.IsNullOrEmpty(client.ImageUrl) && client.ImageUrl != model.ImageUrl)
					{
						Server.DeleteFile(client.ImageUrl);
					}

					client.ImageUrl = model.ImageUrl;
				}
				else
				{
					client = new Client
					{
						Name = model.Name,
						Link = model.Link,
						ImageUrl = model.ImageUrl,
						Testimonial = model.Testimonial,
						HappyClient = model.HappyClient
					};
					_dataContext.Clients.Add(client);
				}

				_dataContext.SaveChanges();

				return RedirectToAction("Clients");
			}
			else
			{
				return View("Client", model);
			}
		}

		[HttpDelete]
		public ActionResult Client(int id)
		{
			var client = _dataContext.Clients.FirstOrDefault(x => x.Id == id);
			if (client != null)
			{
				_dataContext.Clients.Remove(client);
				_dataContext.SaveChanges();
			}
			return new EmptyResult();
		}
	}
}