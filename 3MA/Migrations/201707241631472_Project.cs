namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Project : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Developer = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        Province = c.String(),
                        Postal = c.String(),
                        Country = c.String(),
                        isPackage = c.Boolean(nullable: false),
                        isProduct = c.Boolean(nullable: false),
                        ClientID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Projects");
        }
    }
}
