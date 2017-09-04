namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accessories_Price_double : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accessories", "Category", c => c.String());
            AlterColumn("dbo.Accessories", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accessories", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Accessories", "Category");
        }
    }
}
