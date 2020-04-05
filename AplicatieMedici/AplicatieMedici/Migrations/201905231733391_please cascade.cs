namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pleasecascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Istorics", "PacientCNP", "dbo.Pacients");
            DropIndex("dbo.Istorics", new[] { "PacientCNP" });
            AlterColumn("dbo.Istorics", "PacientCNP", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Istorics", "PacientCNP");
            AddForeignKey("dbo.Istorics", "PacientCNP", "dbo.Pacients", "CNP", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Istorics", "PacientCNP", "dbo.Pacients");
            DropIndex("dbo.Istorics", new[] { "PacientCNP" });
            AlterColumn("dbo.Istorics", "PacientCNP", c => c.String(maxLength: 128));
            CreateIndex("dbo.Istorics", "PacientCNP");
            AddForeignKey("dbo.Istorics", "PacientCNP", "dbo.Pacients", "CNP");
        }
    }
}
