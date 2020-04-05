namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class internare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Istorics", "Internare", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Istorics", "Internare");
        }
    }
}
