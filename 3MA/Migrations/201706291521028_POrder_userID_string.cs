namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POrder_userID_string : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POrders", "customerID", c => c.String());
            DropColumn("dbo.POrders", "userID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POrders", "userID", c => c.Int(nullable: false));
            DropColumn("dbo.POrders", "customerID");
        }
    }
}
