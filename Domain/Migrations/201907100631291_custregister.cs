namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class custregister : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerRegisters",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerPhone = c.String(),
                        Password = c.String(),
                        CustomerEmail_EmailId = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Emails", t => t.CustomerEmail_EmailId)
                .Index(t => t.CustomerEmail_EmailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerRegisters", "CustomerEmail_EmailId", "dbo.Emails");
            DropIndex("dbo.CustomerRegisters", new[] { "CustomerEmail_EmailId" });
            DropTable("dbo.CustomerRegisters");
        }
    }
}
