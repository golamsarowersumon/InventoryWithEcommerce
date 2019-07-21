namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salessetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalesElementStups",
                c => new
                    {
                        SalesElementSetupId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        SalesPricePercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesPriceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VatPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VarAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BrandId = c.Int(),
                        ModelId = c.Int(),
                    })
                .PrimaryKey(t => t.SalesElementSetupId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalesElementStups");
        }
    }
}
