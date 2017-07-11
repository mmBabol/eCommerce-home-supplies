namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_package_and_product_objects : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "name");
        }
    }
}
