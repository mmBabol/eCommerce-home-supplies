namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_model_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PriceCategory", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PriceCategory", c => c.String(nullable: false));
        }
    }
}
