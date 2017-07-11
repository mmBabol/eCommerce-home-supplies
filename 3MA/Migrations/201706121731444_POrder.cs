namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Completed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Completed");
        }
    }
}
