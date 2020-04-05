namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reteta1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RetetaModels", "PacientCNP", "dbo.Pacients");
            DropIndex("dbo.RetetaModels", new[] { "PacientCNP" });
            AddColumn("dbo.RetetaModels", "Medicament1", c => c.String());
            AddColumn("dbo.RetetaModels", "Administrare1", c => c.String());
            AddColumn("dbo.RetetaModels", "Medicament2", c => c.String());
            AddColumn("dbo.RetetaModels", "Administrare2", c => c.String());
            AddColumn("dbo.RetetaModels", "Medicament3", c => c.String());
            AddColumn("dbo.RetetaModels", "Administrare3", c => c.String());
            AddColumn("dbo.RetetaModels", "Medicament4", c => c.String());
            AddColumn("dbo.RetetaModels", "Administrare4", c => c.String());
            AddColumn("dbo.RetetaModels", "Medicament5", c => c.String());
            AddColumn("dbo.RetetaModels", "Administrare5", c => c.String());
            AddColumn("dbo.RetetaModels", "CodReteta", c => c.Guid(nullable: false));
            AlterColumn("dbo.RetetaModels", "PacientCNP", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.RetetaModels", "PacientCNP");
            AddForeignKey("dbo.RetetaModels", "PacientCNP", "dbo.Pacients", "CNP", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RetetaModels", "PacientCNP", "dbo.Pacients");
            DropIndex("dbo.RetetaModels", new[] { "PacientCNP" });
            AlterColumn("dbo.RetetaModels", "PacientCNP", c => c.String(maxLength: 128));
            DropColumn("dbo.RetetaModels", "CodReteta");
            DropColumn("dbo.RetetaModels", "Administrare5");
            DropColumn("dbo.RetetaModels", "Medicament5");
            DropColumn("dbo.RetetaModels", "Administrare4");
            DropColumn("dbo.RetetaModels", "Medicament4");
            DropColumn("dbo.RetetaModels", "Administrare3");
            DropColumn("dbo.RetetaModels", "Medicament3");
            DropColumn("dbo.RetetaModels", "Administrare2");
            DropColumn("dbo.RetetaModels", "Medicament2");
            DropColumn("dbo.RetetaModels", "Administrare1");
            DropColumn("dbo.RetetaModels", "Medicament1");
            CreateIndex("dbo.RetetaModels", "PacientCNP");
            AddForeignKey("dbo.RetetaModels", "PacientCNP", "dbo.Pacients", "CNP");
        }
    }
}
