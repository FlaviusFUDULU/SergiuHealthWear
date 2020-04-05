namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retetaistoricnoset : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RetetaModels", "CodReteta");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RetetaModels", "CodReteta", c => c.Guid(nullable: false));
        }
    }
}
