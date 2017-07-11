namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Suite_and_Plan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Suitez");
            DropColumn("dbo.AspNetUsers", "Planz");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Planz", c => c.String());
            AddColumn("dbo.AspNetUsers", "Suitez", c => c.Int());
        }
    }
}
