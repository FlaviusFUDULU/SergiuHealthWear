namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKmedicpacient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pacients", "MedicCNP", c => c.String(maxLength: 128));
            CreateIndex("dbo.Pacients", "MedicCNP");
            AddForeignKey("dbo.Pacients", "MedicCNP", "dbo.Medics", "CNP");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pacients", "MedicCNP", "dbo.Medics");
            DropIndex("dbo.Pacients", new[] { "MedicCNP" });
            DropColumn("dbo.Pacients", "MedicCNP");
        }
    }
}
