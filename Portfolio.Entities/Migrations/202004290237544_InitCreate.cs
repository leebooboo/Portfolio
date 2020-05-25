namespace Portfolio.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryCode = c.String(maxLength: 20),
                        CategoryName = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 50),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Checkouts",
                c => new
                    {
                        CheckoutId = c.Long(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.Long(nullable: false),
                        ProductCount = c.Int(nullable: false),
                        InsertDt = c.DateTime(nullable: false),
                        UpdateDt = c.DateTime(),
                    })
                .PrimaryKey(t => t.CheckoutId)
                .Index(t => new { t.UserId, t.ProductId });
            
            CreateTable(
                "dbo.ImageUseTypes",
                c => new
                    {
                        ImageUseTypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(maxLength: 20),
                        UseYn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ImageUseTypeId);
            
            CreateTable(
                "dbo.Inquiries",
                c => new
                    {
                        InquiryId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        RefId = c.Long(),
                        RefLevel = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false, maxLength: 2400),
                        IsLocked = c.Boolean(nullable: false),
                        IpAdress = c.String(),
                        InsertDt = c.DateTime(nullable: false),
                        UpdateDt = c.DateTime(),
                    })
                .PrimaryKey(t => t.InquiryId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        CategoryId = c.Int(),
                        ProductName = c.String(nullable: false, maxLength: 50),
                        Summary = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PromotionPrice = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .Index(t => t.ProductName);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Long(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Count = c.Int(nullable: false),
                        SumPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsertDt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Long(nullable: false, identity: true),
                        OrderNo = c.String(maxLength: 13),
                        UserId = c.String(nullable: false, maxLength: 128),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderTypeId = c.Int(nullable: false),
                        OrderDetailId = c.Long(nullable: false),
                        InsertDt = c.DateTime(nullable: false),
                        UpdateDt = c.DateTime(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.OrderTypes", t => t.OrderTypeId, cascadeDelete: true)
                .Index(t => t.OrderNo)
                .Index(t => new { t.UserId, t.InsertDt })
                .Index(t => t.OrderTypeId);
            
            CreateTable(
                "dbo.OrderTypes",
                c => new
                    {
                        OrderTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.OrderTypeId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ProductImageId = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        ImageUseTypeId = c.Int(nullable: false),
                        ImageName = c.String(nullable: false, maxLength: 50),
                        ImageType = c.String(nullable: false, maxLength: 10),
                        ImageSize = c.Int(nullable: false),
                        ImagePath = c.String(nullable: false, maxLength: 255),
                        InsertDt = c.DateTime(nullable: false),
                        UpdateDt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("dbo.ImageUseTypes", t => t.ImageUseTypeId, cascadeDelete: true)
                .Index(t => new { t.ProductId, t.ImageUseTypeId });
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        RefId = c.Long(),
                        RefLevel = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false, maxLength: 2400),
                        IpAdress = c.String(),
                        InsertDt = c.DateTime(nullable: false),
                        UpdateDt = c.DateTime(),
                        Rating = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.UploadImages",
                c => new
                    {
                        ProductImageId = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        ImageUseTypeId = c.Int(nullable: false),
                        ImageName = c.String(nullable: false, maxLength: 50),
                        ImageType = c.String(nullable: false, maxLength: 10),
                        ImageSize = c.Int(nullable: false),
                        ImagePath = c.String(nullable: false, maxLength: 255),
                        InsertDt = c.DateTime(nullable: false),
                        UpdateDt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("dbo.ImageUseTypes", t => t.ImageUseTypeId, cascadeDelete: true)
                .Index(t => new { t.ProductId, t.ImageUseTypeId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UploadImages", "ImageUseTypeId", "dbo.ImageUseTypes");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ImageUseTypeId", "dbo.ImageUseTypes");
            DropForeignKey("dbo.Orders", "OrderTypeId", "dbo.OrderTypes");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Inquiries", "ProductId", "dbo.Products");
            DropIndex("dbo.UploadImages", new[] { "ProductId", "ImageUseTypeId" });
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId", "ImageUseTypeId" });
            DropIndex("dbo.Orders", new[] { "OrderTypeId" });
            DropIndex("dbo.Orders", new[] { "UserId", "InsertDt" });
            DropIndex("dbo.Orders", new[] { "OrderNo" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Products", new[] { "ProductName" });
            DropIndex("dbo.Inquiries", new[] { "ProductId" });
            DropIndex("dbo.Checkouts", new[] { "UserId", "ProductId" });
            DropTable("dbo.UploadImages");
            DropTable("dbo.Reviews");
            DropTable("dbo.ProductImages");
            DropTable("dbo.OrderTypes");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Inquiries");
            DropTable("dbo.ImageUseTypes");
            DropTable("dbo.Checkouts");
            DropTable("dbo.Categories");
        }
    }
}
