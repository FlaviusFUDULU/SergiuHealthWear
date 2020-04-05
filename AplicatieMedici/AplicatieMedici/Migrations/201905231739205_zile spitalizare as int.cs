namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zilespitalizareasint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Istorics", "ZileSpitalizare", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Istorics", "ZileSpitalizare", c => c.String());
        }
    }
}
