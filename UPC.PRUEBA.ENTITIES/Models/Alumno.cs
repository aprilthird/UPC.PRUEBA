using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.PRUEBA.ENTITIES.Models
{
    [Table("Alumnos", Schema = "dbo")]
    public class Alumno
    {
        [Key]
        public int IdAlumno { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public List<Solicitud> Solicitudes { get; set; }
    }
}
