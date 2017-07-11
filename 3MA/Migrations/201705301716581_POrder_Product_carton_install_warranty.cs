namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POrder_Product_carton_install_warranty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SqFt", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Lbs", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Type", c => c.String());
            AddColumn("dbo.Products", "RHC", c => c.String());
            AddColumn("dbo.Products", "Core", c => c.String());
            AddColumn("dbo.Products", "Strctr", c => c.String());
            AddColumn("dbo.Products", "Fnsh", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Fnsh");
            DropColumn("dbo.Products", "Strctr");
            DropColumn("dbo.Products", "Core");
            DropColumn("dbo.Products", "RHC");
            DropColumn("dbo.Products", "Type");
            DropColumn("dbo.Products", "Lbs");
            DropColumn("dbo.Products", "SqFt");
        }
    }
}
