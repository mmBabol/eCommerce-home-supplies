namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Model_POrder_Accessories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accessories", "POrder_Id", c => c.Int());
            CreateIndex("dbo.Accessories", "POrder_Id");
            AddForeignKey("dbo.Accessories", "POrder_Id", "dbo.POrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accessories", "POrder_Id", "dbo.POrders");
            DropIndex("dbo.Accessories", new[] { "POrder_Id" });
            DropColumn("dbo.Accessories", "POrder_Id");
        }
    }
}
