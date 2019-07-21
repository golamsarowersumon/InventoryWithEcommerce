namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Districts", "CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.Upazilas", "DistrictId", c => c.Int(nullable: false));
            CreateIndex("dbo.Districts", "CountryId");
            CreateIndex("dbo.Upazilas", "DistrictId");
            AddForeignKey("dbo.Districts", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
            AddForeignKey("dbo.Upazilas", "DistrictId", "dbo.Districts", "DistrictId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Upazilas", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Districts", "CountryId", "dbo.Countries");
            DropIndex("dbo.Upazilas", new[] { "DistrictId" });
            DropIndex("dbo.Districts", new[] { "CountryId" });
            DropColumn("dbo.Upazilas", "DistrictId");
            DropColumn("dbo.Districts", "CountryId");
        }
    }
}
