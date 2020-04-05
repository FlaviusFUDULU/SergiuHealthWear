namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedaccountguidforpacient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pacients", "AccountGuid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pacients", "AccountGuid");
        }
    }
}
