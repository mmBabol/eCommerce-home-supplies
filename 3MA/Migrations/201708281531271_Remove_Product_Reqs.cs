namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Product_Reqs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PriceCategory", c => c.String());
            AlterColumn("dbo.Products", "Collection", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Collection", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "PriceCategory", c => c.String(nullable: false));
        }
    }
}
