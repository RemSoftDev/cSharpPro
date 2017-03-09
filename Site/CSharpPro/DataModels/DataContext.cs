using CSharpPro.Migrations;
using System.Data.Entity;

namespace CSharpPro.DataModels
{

	public class DataContext : DbContext
	{
		// Your context has been configured to use a 'DataContext' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'CSharpPro.DataModels.DataContext' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'DataContext' 
		// connection string in the application configuration file.
		public DataContext()
			: base("name=CSharpPro")
		{
			Database.SetInitializer(new DataContextInitializer());
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		public virtual DbSet<Skill> Skills { get; set; }
		public virtual DbSet<ProjectType> ProjectTypes { get; set; }
		public virtual DbSet<Project> Projects { get; set; }
		public virtual DbSet<Client> Clients { get; set; }
		public virtual DbSet<TeamMember> TeamMembers { get; set; }
		public virtual DbSet<Service> Services { get; set; }
	}
	public class DataContextInitializer : MigrateDatabaseToLatestVersion<DataContext, Configuration>
	{
	}
}

