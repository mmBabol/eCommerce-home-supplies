namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product_Image_Specs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PriceCategory", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Image", c => c.String());
            AddColumn("dbo.Products", "Specs", c => c.String());
            DropColumn("dbo.Products", "PhotoContentType");
            DropColumn("dbo.Products", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Photo", c => c.Binary());
            AddColumn("dbo.Products", "PhotoContentType", c => c.String(maxLength: 200));
            DropColumn("dbo.Products", "Specs");
            DropColumn("dbo.Products", "Image");
            DropColumn("dbo.Products", "PriceCategory");
        }
    }
}
