namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POrder_Billing_Info : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POrders", "BillStreet", c => c.String());
            AddColumn("dbo.POrders", "BillCity", c => c.String());
            AddColumn("dbo.POrders", "BillProv", c => c.String());
            AddColumn("dbo.POrders", "BillCountry", c => c.String());
            AddColumn("dbo.POrders", "BillPostal", c => c.String());
            AddColumn("dbo.POrders", "ProjectName", c => c.String());
            AddColumn("dbo.POrders", "ProjectId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POrders", "ProjectId");
            DropColumn("dbo.POrders", "ProjectName");
            DropColumn("dbo.POrders", "BillPostal");
            DropColumn("dbo.POrders", "BillCountry");
            DropColumn("dbo.POrders", "BillProv");
            DropColumn("dbo.POrders", "BillCity");
            DropColumn("dbo.POrders", "BillStreet");
        }
    }
}
