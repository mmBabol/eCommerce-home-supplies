namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POrder_OrderPurchased1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POrders", "OrderPlaced", c => c.DateTime(nullable: false));
            DropColumn("dbo.POrders", "OrderPlacd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POrders", "OrderPlacd", c => c.DateTime(nullable: false));
            DropColumn("dbo.POrders", "OrderPlaced");
        }
    }
}
