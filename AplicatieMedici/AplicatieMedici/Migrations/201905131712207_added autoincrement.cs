namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedautoincrement : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Istorics");
            AlterColumn("dbo.Istorics", "IstoricId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Istorics", "IstoricId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Istorics");
            AlterColumn("dbo.Istorics", "IstoricId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Istorics", "IstoricId");
        }
    }
}
