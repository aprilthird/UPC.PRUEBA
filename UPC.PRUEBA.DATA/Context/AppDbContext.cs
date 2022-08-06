using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.PRUEBA.ENTITIES.Models;

namespace UPC.PRUEBA.DATA.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Solicitud> Solicitudes { get; set; }

        public DbSet<DetalleSolicitud> DetalleSolicitudes { get; set; }

        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Solicitud>()
                .HasRequired(x => x.Alumno)
                .WithMany(x => x.Solicitudes)
                .HasForeignKey(x => x.IdAlumno)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleSolicitud>()
                .HasRequired(x => x.Solicitud)
                .WithMany(x => x.DetalleSolicitudes)
                .HasForeignKey(x => x.IdSolicitud)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleSolicitud>()
                .HasRequired(x => x.Curso)
                .WithMany(x => x.DetalleSolicitudes)
                .HasForeignKey(x => x.IdCurso)
                .WillCascadeOnDelete(false);


        }
    }
}
