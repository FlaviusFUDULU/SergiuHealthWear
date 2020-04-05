namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedguidtologintomedic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medics", "accountGuid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medics", "accountGuid");
        }
    }
}
