namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelProductAccessories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accessories", "Product_Id", c => c.Int());
            CreateIndex("dbo.Accessories", "Product_Id");
            AddForeignKey("dbo.Accessories", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accessories", "Product_Id", "dbo.Products");
            DropIndex("dbo.Accessories", new[] { "Product_Id" });
            DropColumn("dbo.Accessories", "Product_Id");
        }
    }
}
