namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1to1medicorar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orars",
                c => new
                    {
                        MedicCNP = c.String(nullable: false, maxLength: 128),
                        LuniStart = c.DateTime(nullable: false),
                        LuniEnd = c.DateTime(nullable: false),
                        MartiStart = c.DateTime(nullable: false),
                        MartiEnd = c.DateTime(nullable: false),
                        MiercuriStart = c.DateTime(nullable: false),
                        MiercuriEnd = c.DateTime(nullable: false),
                        JoiStart = c.DateTime(nullable: false),
                        JoiEnd = c.DateTime(nullable: false),
                        VineriStart = c.DateTime(nullable: false),
                        VineriEnd = c.DateTime(nullable: false),
                        SambataStart = c.DateTime(nullable: false),
                        SmbataEnd = c.DateTime(nullable: false),
                        DuminicaStart = c.DateTime(nullable: false),
                        DuminicaEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MedicCNP)
                .ForeignKey("dbo.Medics", t => t.MedicCNP)
                .Index(t => t.MedicCNP);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orars", "MedicCNP", "dbo.Medics");
            DropIndex("dbo.Orars", new[] { "MedicCNP" });
            DropTable("dbo.Orars");
        }
    }
}
