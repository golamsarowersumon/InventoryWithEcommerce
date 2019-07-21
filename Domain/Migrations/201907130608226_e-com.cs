namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ecom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.E_commerceSalesOrderDetails",
                c => new
                    {
                        E_SalerOrderDetailsId = c.Int(nullable: false, identity: true),
                        E_OrderQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        E_SO_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        E_PO_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsShipping = c.Boolean(nullable: false),
                        E_ShippingCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        E_Item_Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemId = c.Int(),
                        ProductId = c.Int(),
                        StoreId = c.Int(),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                        SubSubSubStoreId = c.Int(),
                        SubSubSubSubStoreId = c.Int(),
                        E_SalerOrderMasterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.E_SalerOrderDetailsId)
                .ForeignKey("dbo.E_commerceSalesOrderMaster", t => t.E_SalerOrderMasterId, cascadeDelete: true)
                .Index(t => t.E_SalerOrderMasterId);
            
            CreateTable(
                "dbo.E_commerceSalesOrderMaster",
                c => new
                    {
                        E_SalerOrderMasterId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        E_OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.E_SalerOrderMasterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.E_commerceSalesOrderDetails", "E_SalerOrderMasterId", "dbo.E_commerceSalesOrderMaster");
            DropIndex("dbo.E_commerceSalesOrderDetails", new[] { "E_SalerOrderMasterId" });
            DropTable("dbo.E_commerceSalesOrderMaster");
            DropTable("dbo.E_commerceSalesOrderDetails");
        }
    }
}
