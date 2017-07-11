namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POrder_Address : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POrders", "Street", c => c.String());
            AddColumn("dbo.POrders", "Prov", c => c.String());
            AddColumn("dbo.POrders", "Country", c => c.String());
            AddColumn("dbo.POrders", "Postal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.POrders", "Postal");
            DropColumn("dbo.POrders", "Country");
            DropColumn("dbo.POrders", "Prov");
            DropColumn("dbo.POrders", "Street");
        }
    }
}
