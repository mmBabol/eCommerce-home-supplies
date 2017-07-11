namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product_dimensions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DimW", c => c.String());
            AddColumn("dbo.Products", "DimTH", c => c.String());
            AddColumn("dbo.Products", "DimL", c => c.String());
            AddColumn("dbo.Products", "PricePerSF", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PricePerSF");
            DropColumn("dbo.Products", "DimL");
            DropColumn("dbo.Products", "DimTH");
            DropColumn("dbo.Products", "DimW");
        }
    }
}
