namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_model : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PriceCategory", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Collection", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Products", "Collection", c => c.String());
            AlterColumn("dbo.Products", "PriceCategory", c => c.String());
        }
    }
}
