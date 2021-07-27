namespace Electric.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        AccountName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        RoleId = c.Int(nullable: false),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                        ImageURL = c.String(nullable: false, maxLength: 100),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        DateBirth = c.DateTime(nullable: false),
                        Gender = c.Int(),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 50),
                        Note = c.String(maxLength: 1000),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        BillCode = c.String(maxLength: 10),
                        DateOrder = c.DateTime(),
                        Status = c.Int(),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Guarantee",
                c => new
                    {
                        GuaranteeID = c.Int(nullable: false, identity: true),
                        OrderDetailID = c.Int(),
                        ProductID = c.Int(),
                        Product_Number = c.Int(),
                        Serial_Number = c.Guid(),
                        Status = c.String(maxLength: 50),
                        DateStart = c.DateTime(storeType: "date"),
                        DateEnd = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.GuaranteeID)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetailID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.OrderDetailID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 50),
                        CategoryID = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleDetailID = c.Int(),
                        Amount = c.Int(),
                        ImageURL = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 4000),
                        Detail = c.String(maxLength: 1000),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Imei",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imei = c.String(nullable: false, maxLength: 50),
                        OrderDeatilID = c.Int(nullable: false),
                        OrderDetail_OrderDetailID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetail_OrderDetailID)
                .Index(t => t.OrderDetail_OrderDetailID);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.VoteID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ImageNews",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageName = c.String(maxLength: 50),
                        NewID = c.Int(nullable: false),
                        ImageURL = c.String(nullable: false, maxLength: 100),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.News", t => t.NewID, cascadeDelete: true)
                .Index(t => t.NewID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewID = c.Int(nullable: false, identity: true),
                        ImageURL = c.String(nullable: false, maxLength: 100),
                        Title = c.String(maxLength: 100),
                        Detail = c.String(maxLength: 4000),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.NewID);
            
            CreateTable(
                "dbo.ImageProducts",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageName = c.String(maxLength: 50),
                        ProductID = c.Int(nullable: false),
                        ImageURL = c.String(nullable: false, maxLength: 100),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.SaleDetails",
                c => new
                    {
                        SaleDetailID = c.Int(nullable: false),
                        SaleID = c.Int(),
                        ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.SaleDetailID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.Sales", t => t.SaleID)
                .Index(t => t.SaleID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleID = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        Discount = c.Int(),
                        Name = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.SaleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleDetails", "SaleID", "dbo.Sales");
            DropForeignKey("dbo.SaleDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ImageProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ImageNews", "NewID", "dbo.News");
            DropForeignKey("dbo.Votes", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Votes", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Imei", "OrderDetail_OrderDetailID", "dbo.OrderDetails");
            DropForeignKey("dbo.Guarantee", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Guarantee", "OrderDetailID", "dbo.OrderDetails");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Accounts", "RoleId", "dbo.Role");
            DropIndex("dbo.SaleDetails", new[] { "ProductID" });
            DropIndex("dbo.SaleDetails", new[] { "SaleID" });
            DropIndex("dbo.ImageProducts", new[] { "ProductID" });
            DropIndex("dbo.ImageNews", new[] { "NewID" });
            DropIndex("dbo.Votes", new[] { "ProductID" });
            DropIndex("dbo.Votes", new[] { "CustomerID" });
            DropIndex("dbo.Imei", new[] { "OrderDetail_OrderDetailID" });
            DropIndex("dbo.Guarantee", new[] { "ProductID" });
            DropIndex("dbo.Guarantee", new[] { "OrderDetailID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.Accounts", new[] { "RoleId" });
            DropTable("dbo.Sales");
            DropTable("dbo.SaleDetails");
            DropTable("dbo.ImageProducts");
            DropTable("dbo.News");
            DropTable("dbo.ImageNews");
            DropTable("dbo.Votes");
            DropTable("dbo.Imei");
            DropTable("dbo.Products");
            DropTable("dbo.Guarantee");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
            DropTable("dbo.Role");
            DropTable("dbo.Accounts");
        }
    }
}
