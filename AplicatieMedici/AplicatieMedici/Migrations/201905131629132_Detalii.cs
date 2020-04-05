namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Detalii : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pacients", "DataNasterii", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pacients", "Sex", c => c.String());
            AddColumn("dbo.Pacients", "ActIdentitate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pacients", "ActIdentitate");
            DropColumn("dbo.Pacients", "Sex");
            DropColumn("dbo.Pacients", "DataNasterii");
        }
    }
}
