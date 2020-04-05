namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retetaistoric : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RetetaModels", "PacientCNP", "dbo.Pacients");
            DropIndex("dbo.RetetaModels", new[] { "PacientCNP" });
            AddColumn("dbo.RetetaModels", "IstoricId", c => c.Int(nullable: false));
            CreateIndex("dbo.RetetaModels", "IstoricId");
            AddForeignKey("dbo.RetetaModels", "IstoricId", "dbo.Istorics", "IstoricId", cascadeDelete: true);
            DropColumn("dbo.RetetaModels", "PacientCNP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RetetaModels", "PacientCNP", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.RetetaModels", "IstoricId", "dbo.Istorics");
            DropIndex("dbo.RetetaModels", new[] { "IstoricId" });
            DropColumn("dbo.RetetaModels", "IstoricId");
            CreateIndex("dbo.RetetaModels", "PacientCNP");
            AddForeignKey("dbo.RetetaModels", "PacientCNP", "dbo.Pacients", "CNP", cascadeDelete: true);
        }
    }
}
