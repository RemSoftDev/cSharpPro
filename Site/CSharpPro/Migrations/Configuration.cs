using CSharpPro.DataModels;
using System.Data.Entity.Migrations;

namespace CSharpPro.Migrations
{
	public sealed class Configuration : DbMigrationsConfiguration<DataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(DataContext context)
		{
			context.Skills.AddOrUpdate(x=>x.Value, 
				new Skill[]
				{
					new Skill
					{
						Value = "Html"
					},
					new Skill
					{
						Value = "Css"
					},
					new Skill
					{
						Value = "Javascript"
					},
				});

			base.Seed(context);
		}
	}
}
