namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pleasemedic : DbMigration
    {
        public override void Up()
        {         
            
            CreateTable(
                "dbo.Medics",
                c => new
                    {
                        CNP = c.String(nullable: false, maxLength: 128),
                        Nume = c.String(nullable: false),
                        Prenume = c.String(nullable: false),
                        Email = c.String(),
                        Adresa = c.String(nullable: false),
                        Numartelefon = c.String(),
                        DataNastertii = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CNP);
        }
        
        public override void Down()
        {
            DropTable("dbo.Medics");
        }
    }
}
