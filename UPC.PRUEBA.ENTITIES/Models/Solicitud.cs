using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.PRUEBA.ENTITIES.Models
{
    [Table("Solicitudes", Schema = "dbo")]
    public class Solicitud
    {
        [Key]
        public int IdSolicitud { get; set; }

        public int IdAlumno { get; set; }

        public Alumno Alumno { get; set; }

        public DateTime FechaSolicitud { get; set; }

        public string CodRegistrante { get; set; }

        public string Carrera { get; set; }

        public string Periodo { get; set; }

        public List<DetalleSolicitud> DetalleSolicitudes { get; set; }
    }
}
