namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_account_new_data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Suite", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Plan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Plan");
            DropColumn("dbo.AspNetUsers", "Suite");
        }
    }
}
