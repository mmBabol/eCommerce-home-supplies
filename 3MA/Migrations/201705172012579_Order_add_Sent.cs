namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_add_Sent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Suite", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Suite", c => c.String());
        }
    }
}
