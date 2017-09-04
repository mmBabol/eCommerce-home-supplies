namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accessories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accessories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Specs = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        Collection = c.String(),
                        Size = c.String(),
                        SKU = c.String(),
                        Parent_SKU = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accessories");
        }
    }
}
