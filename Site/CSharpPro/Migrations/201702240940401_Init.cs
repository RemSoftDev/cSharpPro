namespace CSharpPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Link = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Link = c.String(),
                        ImageUrl = c.String(),
                        Skills = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectClients",
                c => new
                    {
                        Project_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.Client_Id })
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.ProjectTypeProjects",
                c => new
                    {
                        ProjectType_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectType_Id, t.Project_Id })
                .ForeignKey("dbo.ProjectTypes", t => t.ProjectType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.ProjectType_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTypeProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectTypeProjects", "ProjectType_Id", "dbo.ProjectTypes");
            DropForeignKey("dbo.ProjectClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ProjectClients", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ProjectTypeProjects", new[] { "Project_Id" });
            DropIndex("dbo.ProjectTypeProjects", new[] { "ProjectType_Id" });
            DropIndex("dbo.ProjectClients", new[] { "Client_Id" });
            DropIndex("dbo.ProjectClients", new[] { "Project_Id" });
            DropTable("dbo.ProjectTypeProjects");
            DropTable("dbo.ProjectClients");
            DropTable("dbo.Skills");
            DropTable("dbo.ProjectTypes");
            DropTable("dbo.Projects");
            DropTable("dbo.Clients");
        }
    }
}
