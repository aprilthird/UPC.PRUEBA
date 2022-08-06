namespace UPC.PRUEBA.DATA.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UPC.PRUEBA.ENTITIES.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UPC.PRUEBA.DATA.Context.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UPC.PRUEBA.DATA.Context.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            if (!context.Alumnos.Any())
            {
                var alumnosLst = new List<Alumno>();
                for (var i = 0; i < 100; ++i)
                {
                    alumnosLst.Add(new Alumno()
                    {
                        Nombres = $"Alumno Prueba {i}",
                        Apellidos = $"Alumno Prueba {i}"
                    });
                }
                context.Alumnos.AddRange(alumnosLst);
                context.SaveChanges();
            }

            if(!context.Cursos.Any())
            {
                var cursosLst = new List<Curso>();
                for (var i = 0; i < 20; ++i)
                {
                    cursosLst.Add(new Curso()
                    {
                        Nombre = $"Curso Prueba {i}",
                        Descripcion = $"Descripcion {i * 100}",
                        NroCreditos = new Random().Next(1, 5),
                        Activo = i < 15
                    });
                }
                context.Cursos.AddRange(cursosLst);
                context.SaveChanges();
            }
        }
    }
}
