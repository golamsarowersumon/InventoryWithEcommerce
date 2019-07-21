namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class custregister1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerRegisters", "CustomerEmail_EmailId", "dbo.Emails");
            DropIndex("dbo.CustomerRegisters", new[] { "CustomerEmail_EmailId" });
            AddColumn("dbo.CustomerRegisters", "CustomerEmail", c => c.String());
            DropColumn("dbo.CustomerRegisters", "CustomerEmail_EmailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerRegisters", "CustomerEmail_EmailId", c => c.Int());
            DropColumn("dbo.CustomerRegisters", "CustomerEmail");
            CreateIndex("dbo.CustomerRegisters", "CustomerEmail_EmailId");
            AddForeignKey("dbo.CustomerRegisters", "CustomerEmail_EmailId", "dbo.Emails", "EmailId");
        }
    }
}
