namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Items", "ModelId", "dbo.Models");
            DropIndex("dbo.Items", new[] { "ModelId" });
            DropIndex("dbo.Items", new[] { "BrandId" });
            AddColumn("dbo.ProcurementDetails", "SO_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InventoryDetails", "SO_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Items", "ModelId", c => c.Int());
            AlterColumn("dbo.Items", "BrandId", c => c.Int());
            CreateIndex("dbo.Items", "ModelId");
            CreateIndex("dbo.Items", "BrandId");
            AddForeignKey("dbo.Items", "BrandId", "dbo.Brands", "BrandId");
            AddForeignKey("dbo.Items", "ModelId", "dbo.Models", "ModelId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Items", "BrandId", "dbo.Brands");
            DropIndex("dbo.Items", new[] { "BrandId" });
            DropIndex("dbo.Items", new[] { "ModelId" });
            AlterColumn("dbo.Items", "BrandId", c => c.Int(nullable: false));
            AlterColumn("dbo.Items", "ModelId", c => c.Int(nullable: false));
            DropColumn("dbo.InventoryDetails", "SO_Price");
            DropColumn("dbo.ProcurementDetails", "SO_Price");
            CreateIndex("dbo.Items", "BrandId");
            CreateIndex("dbo.Items", "ModelId");
            AddForeignKey("dbo.Items", "ModelId", "dbo.Models", "ModelId", cascadeDelete: true);
            AddForeignKey("dbo.Items", "BrandId", "dbo.Brands", "BrandId", cascadeDelete: true);
        }
    }
}
