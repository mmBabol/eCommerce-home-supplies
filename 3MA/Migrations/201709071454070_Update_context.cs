namespace _3MA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_context : DbMigration
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
                        Price = c.Double(nullable: false),
                        Collection = c.String(),
                        Category = c.String(),
                        Size = c.String(),
                        SKU = c.String(),
                        Parent_SKU = c.String(),
                        Product_Id = c.Int(),
                        POrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.POrders", t => t.POrder_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.POrder_Id);
            
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
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Suite = c.Int(nullable: false),
                        LightUpdate = c.Boolean(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        OrderWithPackage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.OrderWithPackage_Id)
                .Index(t => t.OrderWithPackage_Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ImgURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainCategory = c.String(nullable: false),
                        SubCategory = c.String(nullable: false),
                        PriceCategory = c.String(),
                        MFG_SKU = c.String(nullable: false),
                        Collection = c.String(nullable: false),
                        Finish = c.String(),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 300),
                        Price = c.Double(nullable: false),
                        Image = c.String(),
                        Specs = c.String(),
                        DimW = c.String(),
                        DimTH = c.String(),
                        DimL = c.String(),
                        PricePerSF = c.Double(nullable: false),
                        SqFt = c.Double(nullable: false),
                        Lbs = c.Double(nullable: false),
                        Type = c.String(),
                        RHC = c.String(),
                        Core = c.String(),
                        Strctr = c.String(),
                        Fnsh = c.String(),
                        Package_Id = c.Int(),
                        POrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .ForeignKey("dbo.POrders", t => t.POrder_Id)
                .Index(t => t.Package_Id)
                .Index(t => t.POrder_Id);
            
            CreateTable(
                "dbo.POrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Suite = c.Int(nullable: false),
                        Plan = c.String(),
                        LightUpgrade = c.Boolean(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        OrderPlaced = c.DateTime(nullable: false),
                        Name = c.String(),
                        HPhone = c.String(),
                        MPhone = c.String(),
                        MoveIn = c.DateTime(nullable: false),
                        customerID = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        Prov = c.String(),
                        Country = c.String(),
                        Postal = c.String(),
                        BillStreet = c.String(),
                        BillCity = c.String(),
                        BillProv = c.String(),
                        BillCountry = c.String(),
                        BillPostal = c.String(),
                        ProjectName = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.RoleClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Suite = c.Int(nullable: false),
                        Plan = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "POrder_Id", "dbo.POrders");
            DropForeignKey("dbo.Accessories", "POrder_Id", "dbo.POrders");
            DropForeignKey("dbo.Orders", "OrderWithPackage_Id", "dbo.Packages");
            DropForeignKey("dbo.Products", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.Accessories", "Product_Id", "dbo.Products");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Products", new[] { "POrder_Id" });
            DropIndex("dbo.Products", new[] { "Package_Id" });
            DropIndex("dbo.Orders", new[] { "OrderWithPackage_Id" });
            DropIndex("dbo.Accessories", new[] { "POrder_Id" });
            DropIndex("dbo.Accessories", new[] { "Product_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RoleClaims");
            DropTable("dbo.Projects");
            DropTable("dbo.POrders");
            DropTable("dbo.Products");
            DropTable("dbo.Packages");
            DropTable("dbo.Orders");
            DropTable("dbo.RoomFloors");
            DropTable("dbo.Accessories");
        }
    }
}
