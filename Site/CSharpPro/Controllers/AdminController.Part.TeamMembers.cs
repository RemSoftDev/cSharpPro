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
		public ActionResult TeamMembers()
		{
			var clients = _dataContext.TeamMembers
				.AsNoTracking()
				.OrderBy(x=>x.Name)
				.ToList();
			return View("TeamMembers", clients);
		}

		[HttpGet]
		public ActionResult TeamMember(int? id = null)
		{
			TeamMemberViewModel client = null;
			if (id.HasValue)
			{
				var clientDto = _dataContext.TeamMembers.AsNoTracking().First(x => x.Id == id.Value);
				client = new TeamMemberViewModel
				{
					Id = clientDto.Id,
					Position = clientDto.Position,
					Name = clientDto.Name,
					ImageUrl = clientDto.ImageUrl
				};
			}
			return View("TeamMember", client);
		}

		[HttpPost]
		public ActionResult TeamMember(TeamMemberViewModel model)
		{
			if (model.Image != null)
			{
				model.ImageUrl = Server.SaveFile(model.Image);
			}

			if (ModelState.IsValid)
			{
				TeamMember client = null;
				if (model.Id.HasValue)
				{
					client = _dataContext.TeamMembers.First(x => x.Id == model.Id.Value);
					client.Name = model.Name;
					client.Position = model.Position;

					if (!string.IsNullOrEmpty(client.ImageUrl) && client.ImageUrl != model.ImageUrl)
					{
						Server.DeleteFile(client.ImageUrl);
					}

					client.ImageUrl = model.ImageUrl;
				}
				else
				{
					client = new TeamMember
					{
						Name = model.Name,
						Position = model.Position,
						ImageUrl = model.ImageUrl
					};
					_dataContext.TeamMembers.Add(client);
				}

				_dataContext.SaveChanges();

				return RedirectToAction("TeamMembers");
			}
			else
			{
				return View("TeamMember", model);
			}
		}

		[HttpDelete]
		public ActionResult TeamMember(int id)
		{
			var client = _dataContext.TeamMembers.FirstOrDefault(x => x.Id == id);
			if (client != null)
			{
				_dataContext.TeamMembers.Remove(client);
				_dataContext.SaveChanges();
			}
			return new EmptyResult();
		}
	}
}