namespace CSharpPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHappyClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Testimonial", c => c.String());
            AddColumn("dbo.Clients", "HappyClient", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "HappyClient");
            DropColumn("dbo.Clients", "Testimonial");
        }
    }
}
