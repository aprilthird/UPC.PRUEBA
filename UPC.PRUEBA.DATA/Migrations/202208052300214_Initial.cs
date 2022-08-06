namespace UPC.PRUEBA.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alumnos",
                c => new
                    {
                        IdAlumno = c.Int(nullable: false, identity: true),
                        Nombres = c.String(),
                        Apellidos = c.String(),
                    })
                .PrimaryKey(t => t.IdAlumno);
            
            CreateTable(
                "dbo.Solicitudes",
                c => new
                    {
                        IdSolicitud = c.Int(nullable: false, identity: true),
                        IdAlumno = c.Int(nullable: false),
                        FechaSolicitud = c.DateTime(nullable: false),
                        CodRegistrante = c.String(),
                        Carrera = c.String(),
                        Periodo = c.String(),
                    })
                .PrimaryKey(t => t.IdSolicitud)
                .ForeignKey("dbo.Alumnos", t => t.IdAlumno)
                .Index(t => t.IdAlumno);
            
            CreateTable(
                "dbo.Cursos",
                c => new
                    {
                        IdCurso = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        NroCreditos = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdCurso);
            
            CreateTable(
                "dbo.DetalleSolicitudes",
                c => new
                    {
                        IdDetalleSol = c.Int(nullable: false, identity: true),
                        IdSolicitud = c.Int(nullable: false),
                        IdCurso = c.Int(nullable: false),
                        Profesor = c.String(),
                        Aula = c.String(),
                        Sede = c.String(),
                        Observacion = c.String(),
                    })
                .PrimaryKey(t => t.IdDetalleSol)
                .ForeignKey("dbo.Cursos", t => t.IdCurso)
                .ForeignKey("dbo.Solicitudes", t => t.IdSolicitud, cascadeDelete: true)
                .Index(t => t.IdSolicitud)
                .Index(t => t.IdCurso);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleSolicitudes", "IdSolicitud", "dbo.Solicitudes");
            DropForeignKey("dbo.DetalleSolicitudes", "IdCurso", "dbo.Cursos");
            DropForeignKey("dbo.Solicitudes", "IdAlumno", "dbo.Alumnos");
            DropIndex("dbo.DetalleSolicitudes", new[] { "IdCurso" });
            DropIndex("dbo.DetalleSolicitudes", new[] { "IdSolicitud" });
            DropIndex("dbo.Solicitudes", new[] { "IdAlumno" });
            DropTable("dbo.DetalleSolicitudes");
            DropTable("dbo.Cursos");
            DropTable("dbo.Solicitudes");
            DropTable("dbo.Alumnos");
        }
    }
}
