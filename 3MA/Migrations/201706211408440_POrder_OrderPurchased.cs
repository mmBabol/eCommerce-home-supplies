namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POrder_OrderPurchased : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POrders", "OrderPlacd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POrders", "OrderPlacd");
        }
    }
}
