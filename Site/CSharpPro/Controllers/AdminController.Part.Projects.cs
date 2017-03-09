using System.Linq;
using CSharpPro.DataModels;
using CSharpPro.ViewModels;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System;
using Newtonsoft.Json;
using CSharpPro.Extensions;

namespace CSharpPro.Controllers
{
	public partial class AdminController
	{
		[HttpGet]
		public ActionResult Projects()
		{
			var projects = _dataContext.Projects
				.AsNoTracking()
				.OrderBy(x=>x.Title)
				.ToList();
			return View("Projects", projects);
		}

		[HttpGet]
		public ActionResult Project(int? id = null)
		{
			ProjectViewModel project = new ProjectViewModel();
			project.TypeList = _dataContext.ProjectTypes.AsNoTracking().OrderBy(x => x.Title).ToList();
			project.ClientList = _dataContext.Clients.AsNoTracking().OrderBy(x => x.Name).ToList();

			if (id.HasValue)
			{
				var projectDto = _dataContext.Projects
					.AsNoTracking()
					.Select(x => new
					{
						Project = x,
						Types = x.Types.Select(t => t.Id),
						Clients = x.Clients.Select(c => c.Id)
					})
					.FirstOrDefault(x => x.Project.Id == id.Value);

				if(projectDto == null)
					return RedirectToAction("Projects");

				project.Id = projectDto.Project.Id;
				project.Skills = projectDto.Project.Skills;
				if(!string.IsNullOrEmpty(projectDto.Project.Skills)){
					var initialString = projectDto.Project.Skills.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
						.Select(x => new { text = x, value = x })
						.ToList();
					project.InitialSkills = JsonConvert.SerializeObject(initialString, Formatting.None);
				}

				project.Link = projectDto.Project.Link;
				project.ImageUrl = projectDto.Project.ImageUrl;
				project.Title = projectDto.Project.Title;
				project.Description = projectDto.Project.Description;
				project.SelectedTypeList = projectDto.Types.ToList();
				project.SelectedClientList = projectDto.Clients.ToList();
			}
			return View("Project", project);
		}

		[HttpPost]
		public ActionResult Project(ProjectViewModel model)
		{
			if (model.Image != null)
			{
				model.ImageUrl = Server.SaveFile(model.Image);
			}

			if (ModelState.IsValid)
			{
				Project project = null;
				List<ProjectType> types = null;
				List<Client> clients = null;

				if (model.SelectedTypeList != null && model.SelectedTypeList.Any())
				{
					types = _dataContext.ProjectTypes.Where(x => model.SelectedTypeList.Any(s => s == x.Id)).ToList();
				}
				else
				{
					types = new List<ProjectType>();
				}

				if (model.SelectedClientList != null && model.SelectedClientList.Any())
				{
					clients = _dataContext.Clients.Where(x => model.SelectedClientList.Any(s => s == x.Id)).ToList();
				}
				else
				{
					clients = new List<Client>();
				}

				if (!string.IsNullOrEmpty(model.Skills))
				{
					var skills = model.Skills.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
						.Select(x => new Skill { Value = x }).ToArray();
					_dataContext.Skills.AddOrUpdate(x => x.Value, skills);
				}

				if (model.Id.HasValue)
				{
					project = _dataContext.Projects
						.Include(x => x.Clients)
						.Include(x => x.Types)
						.First(x => x.Id == model.Id.Value);

					project.Title = model.Title;
					project.Description = model.Description;
					project.Link = model.Link;
					project.Types = types;
					project.Clients = clients;
					project.Skills = model.Skills;

					if(!string.IsNullOrEmpty(project.ImageUrl) && project.ImageUrl != model.ImageUrl)
					{
						Server.DeleteFile(project.ImageUrl);
					}

					project.ImageUrl = model.ImageUrl;
				}
				else
				{
					project = new Project
					{
						Title = model.Title,
						Description = model.Description,
						Link = model.Link,
						Types = types,
						Clients = clients,
						Skills = model.Skills,
						ImageUrl = model.ImageUrl
					};
					_dataContext.Projects.Add(project);
				}

				_dataContext.SaveChanges();

				return RedirectToAction("Projects");
			}
			else
			{
				model.TypeList = _dataContext.ProjectTypes.AsNoTracking().OrderBy(x=>x.Title).ToList();
				model.ClientList = _dataContext.Clients.AsNoTracking().OrderBy(x => x.Name).ToList();
				return View("Project", model);
			}
		}

		[HttpDelete]
		public ActionResult Project(int id)
		{
			var project = _dataContext.Projects.FirstOrDefault(x => x.Id == id);
			if (project != null)
			{
				_dataContext.Projects.Remove(project);
				_dataContext.SaveChanges();
			}
			return new EmptyResult();
		}
	}
}