namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Programaretablerelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Programares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicCNP = c.String(maxLength: 128),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Confirmed = c.Boolean(nullable: false),
                        PacientCNP = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medics", t => t.MedicCNP)
                .ForeignKey("dbo.Pacients", t => t.PacientCNP)
                .Index(t => t.MedicCNP)
                .Index(t => t.PacientCNP);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Programares", "PacientCNP", "dbo.Pacients");
            DropForeignKey("dbo.Programares", "MedicCNP", "dbo.Medics");
            DropIndex("dbo.Programares", new[] { "PacientCNP" });
            DropIndex("dbo.Programares", new[] { "MedicCNP" });
            DropTable("dbo.Programares");
        }
    }
}
