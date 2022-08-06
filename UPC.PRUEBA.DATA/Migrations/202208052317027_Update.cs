namespace UPC.PRUEBA.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DetalleSolicitudes", "IdSolicitud", "dbo.Solicitudes");
            AddForeignKey("dbo.DetalleSolicitudes", "IdSolicitud", "dbo.Solicitudes", "IdSolicitud");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleSolicitudes", "IdSolicitud", "dbo.Solicitudes");
            AddForeignKey("dbo.DetalleSolicitudes", "IdSolicitud", "dbo.Solicitudes", "IdSolicitud", cascadeDelete: true);
        }
    }
}
