namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ecommerce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.E_commerceSalesOrderDetails", "CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.E_commerceSalesOrderDetails", "DistrictId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.E_commerceSalesOrderDetails", "DistrictId");
            DropColumn("dbo.E_commerceSalesOrderDetails", "CountryId");
        }
    }
}
