using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UPC.PRUEBA.DATA.Context;
using UPC.PRUEBA.ENTITIES.Models;

namespace UPC.PRUEBA.WEB.Seeder
{
    public class DbSeeder
    {
        public static async Task Seed(AppDbContext context)
        {
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
                await context.SaveChangesAsync();
            }

            if (!context.Cursos.Any())
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
                await context.SaveChangesAsync();
            }
        }
    }
}