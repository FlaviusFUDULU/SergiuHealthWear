namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateistoric : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Istorics", "Tratament", c => c.String());
            AddColumn("dbo.Istorics", "Spital", c => c.String());
            AddColumn("dbo.Istorics", "Sectie", c => c.String());
            AddColumn("dbo.Istorics", "Data", c => c.DateTime(nullable: false));
            AddColumn("dbo.Istorics", "ZileSpitalizare", c => c.String());
            AddColumn("dbo.Istorics", "InterventieChirurgicala", c => c.Boolean(nullable: false));
            AddColumn("dbo.Istorics", "DetaliiInterventie", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pacients", "AlergiiIntolerante", c => c.String());
            AddColumn("dbo.Pacients", "BoliCronice", c => c.String());
            AddColumn("dbo.Pacients", "GrupaSange", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pacients", "GrupaSange");
            DropColumn("dbo.Pacients", "BoliCronice");
            DropColumn("dbo.Pacients", "AlergiiIntolerante");
            DropColumn("dbo.Istorics", "DetaliiInterventie");
            DropColumn("dbo.Istorics", "InterventieChirurgicala");
            DropColumn("dbo.Istorics", "ZileSpitalizare");
            DropColumn("dbo.Istorics", "Data");
            DropColumn("dbo.Istorics", "Sectie");
            DropColumn("dbo.Istorics", "Spital");
            DropColumn("dbo.Istorics", "Tratament");
        }
    }
}
