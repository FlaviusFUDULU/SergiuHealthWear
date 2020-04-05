namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedistoric : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Istorics",
                c => new
                    {
                        IstoricId = c.String(nullable: false, maxLength: 128),
                        PacientCNP = c.String(maxLength: 128),
                        Diagnostic = c.String(),
                    })
                .PrimaryKey(t => t.IstoricId)
                .ForeignKey("dbo.Pacients", t => t.PacientCNP)
                .Index(t => t.PacientCNP);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Istorics", "PacientCNP", "dbo.Pacients");
            DropIndex("dbo.Istorics", new[] { "PacientCNP" });
            DropTable("dbo.Istorics");
        }
    }
}
