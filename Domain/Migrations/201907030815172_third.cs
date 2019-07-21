namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryDetails", "IsHot", c => c.Boolean(nullable: false));
            AddColumn("dbo.InventoryDetails", "IsNew", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryDetails", "IsNew");
            DropColumn("dbo.InventoryDetails", "IsHot");
        }
    }
}
