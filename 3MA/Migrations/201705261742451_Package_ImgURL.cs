namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Package_ImgURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "ImgURL", c => c.String());
            AddColumn("dbo.Products", "MainCategory", c => c.String(nullable: false));
            AddColumn("dbo.Products", "SubCategory", c => c.String(nullable: false));
            AddColumn("dbo.Products", "MFG_SKU", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Collection", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Finish", c => c.String());
            AddColumn("dbo.Products", "PhotoContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.Products", "Photo", c => c.Binary());
            DropColumn("dbo.Products", "type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "type", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Products", "Photo");
            DropColumn("dbo.Products", "PhotoContentType");
            DropColumn("dbo.Products", "Finish");
            DropColumn("dbo.Products", "Collection");
            DropColumn("dbo.Products", "MFG_SKU");
            DropColumn("dbo.Products", "SubCategory");
            DropColumn("dbo.Products", "MainCategory");
            DropColumn("dbo.Packages", "ImgURL");
        }
    }
}
