namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retetaupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RetetaModels", "DataEmitere", c => c.DateTime(nullable: false));
            AddColumn("dbo.RetetaModels", "DataRetragere", c => c.DateTime(nullable: false));
            AddColumn("dbo.RetetaModels", "Retras", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RetetaModels", "Retras");
            DropColumn("dbo.RetetaModels", "DataRetragere");
            DropColumn("dbo.RetetaModels", "DataEmitere");
        }
    }
}
