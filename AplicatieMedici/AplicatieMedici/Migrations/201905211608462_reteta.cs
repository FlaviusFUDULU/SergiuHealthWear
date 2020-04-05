namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reteta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RetetaModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PacientCNP = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pacients", t => t.PacientCNP)
                .Index(t => t.PacientCNP);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RetetaModels", "PacientCNP", "dbo.Pacients");
            DropIndex("dbo.RetetaModels", new[] { "PacientCNP" });
            DropTable("dbo.RetetaModels");
        }
    }
}
