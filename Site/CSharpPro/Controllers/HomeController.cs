using System.Linq;
using System.Web.Mvc;
using CSharpPro.DataModels;
using CSharpPro.ViewModels;
using CSharpPro.Services;
using System;

namespace CSharpPro.Controllers
{
	public class HomeController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly MailService _mailService;

		public HomeController()
		{
			_mailService = new MailService();
			_dataContext = new DataContext();
		}

		public ActionResult Index()
		{
			var model = new HomeViewModel();
			model.Clients = _dataContext.Clients
				.AsNoTracking()
				.Where(x => x.HappyClient && !string.IsNullOrEmpty(x.ImageUrl))
				.OrderBy(o => Guid.NewGuid())
				.Take(6)
				.ToList();
			return View(model);
		}

		public ActionResult About()
		{
			var model = new AboutUsViewModel();
			model.TeamMembers = _dataContext.TeamMembers.AsNoTracking().OrderBy(x => x.Name).ToList();
			return View(model);
		}

		public ActionResult Contact()
		{
			var model = new ContactViewModel();
			model.Clients = _dataContext.Clients
				.AsNoTracking()
				.Where(x => !x.HappyClient && !string.IsNullOrEmpty(x.Testimonial))
				.OrderBy(o => Guid.NewGuid())
				.Take(3)
				.ToList();

			return PartialView(model);
		}

		[HttpPost]
		public ActionResult EmailForm(EmailViewModel model)
		{
			if (ModelState.IsValid)
			{
				_mailService.SendRequestMail(model.Name, model.Email, model.Message);
				_mailService.SendConfirmMail(model.Email);

				ModelState.Clear();
				model = new EmailViewModel
				{
					ShowThankyouMessage = true
				};
			}
			return PartialView("MailForm", model);
		}
		public ActionResult Services()
		{
			var services = _dataContext.Services
				.AsNoTracking()
				//.OrderBy(x => x.Title)
				.ToList();
			return View(services);
		}

		public ActionResult Service(int? id = null)
		{
			if (!id.HasValue)
				return RedirectToAction("Services");

			var service = _dataContext.Services
				.AsNoTracking()
				.FirstOrDefault(x => x.Id == id);

			if (service == null)
			{
				return RedirectToAction("Services");
			}

			return View(service);
		}

		public ActionResult Portfolio()
		{
			var model = new PortfolioViewModel();
			model.Projects = _dataContext.Projects
				.AsNoTracking()
				.Select(x => new
				{
					Project = x,
					Types = x.Types,
				})
				.OrderBy(x => x.Project.Title)
				.ToList()
				.Select(x =>
				{
					x.Project.Types = x.Types.ToList();
					return x.Project;
				})
				.ToList();

			model.ProjectTypes = model.Projects
				.SelectMany(x => x.Types)
				.GroupBy(x => x.Id)
				.Select(x => x.First())
				.OrderBy(x => x.Title)
				.ToList();

			return View(model);
		}

		public ActionResult Project(int? id = null)
		{
			if (!id.HasValue)
				return RedirectToAction("Portfolio");

			var dto = _dataContext.Projects
				.AsNoTracking()
				.Where(x => x.Id == id)
				.Select(x => new
				{
					Project = x,
					Types = x.Types,
					Clients = x.Clients
				})
				.ToList()
				.Select(x =>
				{
					x.Project.Types = x.Types.ToList();
					x.Project.Clients = x.Clients.ToList();
					return x.Project;
				})
				.FirstOrDefault();

			if (dto == null)
				return RedirectToAction("Portfolio");

			var model = new PortfolioProjectViewModel
			{
				Project = dto
			};

			var typeIds = dto.Types.Select(t => t.Id).ToList();

			model.RelatedProjects = _dataContext.Projects
				.AsNoTracking()
				.Where(x => x.Id != dto.Id)
				.Where(x => x.Types.Any(tid => typeIds.Contains(tid.Id)))
				.OrderBy(x => x.Title)
				.ToList();

			return View(model);
		}
	}
}