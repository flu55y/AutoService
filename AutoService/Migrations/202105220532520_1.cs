namespace AutoService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.__MigrationHistory",
                c => new
                    {
                        MigrationId = c.String(nullable: false, maxLength: 150),
                        ContextKey = c.String(nullable: false, maxLength: 300),
                        Model = c.Binary(nullable: false),
                        ProductVersion = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => new { t.MigrationId, t.ContextKey });
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarNumber = c.String(nullable: false, maxLength: 20),
                        OwnerId = c.Int(nullable: false),
                        Mark = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CarNumber)
                .ForeignKey("dbo.Owners", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(),
                        CarNumber = c.String(nullable: false, maxLength: 20),
                        Reason = c.String(nullable: false, maxLength: 200),
                        Status = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Cars", t => t.CarNumber, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CarNumber);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        SpecialityId = c.Int(nullable: false),
                        FIO = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Specialities", t => t.SpecialityId, cascadeDelete: true)
                .Index(t => t.SpecialityId);
            
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        SpecialityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.SpecialityId);
            
            CreateTable(
                "dbo.OrderSpareparts",
                c => new
                    {
                        OrderSparepartId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        SparepartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderSparepartId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Spareparts", t => t.SparepartId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.SparepartId);
            
            CreateTable(
                "dbo.Spareparts",
                c => new
                    {
                        SparepartId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Guarantee = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.SparepartId);
            
            CreateTable(
                "dbo.OrderTypeOfWorks",
                c => new
                    {
                        OrderTypeOfWorkId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        TypeOfWorkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderTypeOfWorkId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfWorks", t => t.TypeOfWorkId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.TypeOfWorkId);
            
            CreateTable(
                "dbo.TypeOfWorks",
                c => new
                    {
                        TypeOfWorkId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deadline = c.String(nullable: false),
                        Gurantee = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TypeOfWorkId);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        OwnerId = c.Int(nullable: false, identity: true),
                        FIO = c.String(nullable: false, maxLength: 150),
                        Phone = c.String(nullable: false, maxLength: 20),
                        DriverLicense = c.String(nullable: false, maxLength: 20),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.OrderTypeOfWorks", "TypeOfWorkId", "dbo.TypeOfWorks");
            DropForeignKey("dbo.OrderTypeOfWorks", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderSpareparts", "SparepartId", "dbo.Spareparts");
            DropForeignKey("dbo.OrderSpareparts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Employees", "SpecialityId", "dbo.Specialities");
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Orders", "CarNumber", "dbo.Cars");
            DropIndex("dbo.OrderTypeOfWorks", new[] { "TypeOfWorkId" });
            DropIndex("dbo.OrderTypeOfWorks", new[] { "OrderId" });
            DropIndex("dbo.OrderSpareparts", new[] { "SparepartId" });
            DropIndex("dbo.OrderSpareparts", new[] { "OrderId" });
            DropIndex("dbo.Employees", new[] { "SpecialityId" });
            DropIndex("dbo.Orders", new[] { "CarNumber" });
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            DropIndex("dbo.Cars", new[] { "OwnerId" });
            DropTable("dbo.Owners");
            DropTable("dbo.TypeOfWorks");
            DropTable("dbo.OrderTypeOfWorks");
            DropTable("dbo.Spareparts");
            DropTable("dbo.OrderSpareparts");
            DropTable("dbo.Specialities");
            DropTable("dbo.Employees");
            DropTable("dbo.Orders");
            DropTable("dbo.Cars");
            DropTable("dbo.__MigrationHistory");
        }
    }
}
