namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zinelucratoare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orars", "ZiNelucratoareLuni", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orars", "ZiNelucratoareMarti", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orars", "ZiNelucratoareMiercuri", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orars", "ZiNelucratoareJoi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orars", "ZiNelucratoareVineri", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orars", "ZiNelucratoareSambata", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orars", "ZiNelucratoareDuminica", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orars", "ZiNelucratoareDuminica");
            DropColumn("dbo.Orars", "ZiNelucratoareSambata");
            DropColumn("dbo.Orars", "ZiNelucratoareVineri");
            DropColumn("dbo.Orars", "ZiNelucratoareJoi");
            DropColumn("dbo.Orars", "ZiNelucratoareMiercuri");
            DropColumn("dbo.Orars", "ZiNelucratoareMarti");
            DropColumn("dbo.Orars", "ZiNelucratoareLuni");
        }
    }
}
