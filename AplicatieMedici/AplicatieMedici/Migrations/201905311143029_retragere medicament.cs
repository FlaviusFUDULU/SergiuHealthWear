namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retrageremedicament : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RetetaModels", "MedicamentRetras1", c => c.Boolean(nullable: false));
            AddColumn("dbo.RetetaModels", "MedicamentRetras2", c => c.Boolean(nullable: false));
            AddColumn("dbo.RetetaModels", "MedicamentRetras3", c => c.Boolean(nullable: false));
            AddColumn("dbo.RetetaModels", "MedicamentRetras4", c => c.Boolean(nullable: false));
            AddColumn("dbo.RetetaModels", "MedicamentRetras5", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RetetaModels", "MedicamentRetras5");
            DropColumn("dbo.RetetaModels", "MedicamentRetras4");
            DropColumn("dbo.RetetaModels", "MedicamentRetras3");
            DropColumn("dbo.RetetaModels", "MedicamentRetras2");
            DropColumn("dbo.RetetaModels", "MedicamentRetras1");
        }
    }
}
