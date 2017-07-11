namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_add_Product_list : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderWithPackage_Id", c => c.Int());
            CreateIndex("dbo.Orders", "OrderWithPackage_Id");
            AddForeignKey("dbo.Orders", "OrderWithPackage_Id", "dbo.Packages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderWithPackage_Id", "dbo.Packages");
            DropIndex("dbo.Orders", new[] { "OrderWithPackage_Id" });
            DropColumn("dbo.Orders", "OrderWithPackage_Id");
        }
    }
}
