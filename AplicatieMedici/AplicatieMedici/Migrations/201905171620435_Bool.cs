namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Istorics", "DetaliiInterventie", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Istorics", "DetaliiInterventie", c => c.Boolean(nullable: false));
        }
    }
}
