namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedfromdatetostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orars", "LuniStart", c => c.String());
            AlterColumn("dbo.Orars", "LuniEnd", c => c.String());
            AlterColumn("dbo.Orars", "MartiStart", c => c.String());
            AlterColumn("dbo.Orars", "MartiEnd", c => c.String());
            AlterColumn("dbo.Orars", "MiercuriStart", c => c.String());
            AlterColumn("dbo.Orars", "MiercuriEnd", c => c.String());
            AlterColumn("dbo.Orars", "JoiStart", c => c.String());
            AlterColumn("dbo.Orars", "JoiEnd", c => c.String());
            AlterColumn("dbo.Orars", "VineriStart", c => c.String());
            AlterColumn("dbo.Orars", "VineriEnd", c => c.String());
            AlterColumn("dbo.Orars", "SambataStart", c => c.String());
            AlterColumn("dbo.Orars", "SmbataEnd", c => c.String());
            AlterColumn("dbo.Orars", "DuminicaStart", c => c.String());
            AlterColumn("dbo.Orars", "DuminicaEnd", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orars", "DuminicaEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "DuminicaStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "SmbataEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "SambataStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "VineriEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "VineriStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "JoiEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "JoiStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "MiercuriEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "MiercuriStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "MartiEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "MartiStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "LuniEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orars", "LuniStart", c => c.DateTime(nullable: false));
        }
    }
}
