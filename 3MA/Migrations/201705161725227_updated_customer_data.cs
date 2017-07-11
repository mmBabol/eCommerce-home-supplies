namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_customer_data : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomFloors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        walls = c.Double(nullable: false),
                        floor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        type = c.String(nullable: false, maxLength: 100),
                        description = c.String(nullable: false, maxLength: 200),
                        price = c.Double(nullable: false),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Package_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Package_Id", "dbo.Packages");
            DropIndex("dbo.Products", new[] { "Package_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.Packages");
            DropTable("dbo.RoomFloors");
        }
    }
}
