namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POrder_City : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POrders", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.POrders", "City");
        }
    }
}
