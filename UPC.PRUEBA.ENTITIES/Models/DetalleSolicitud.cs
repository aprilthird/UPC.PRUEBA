using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.PRUEBA.ENTITIES.Models
{
    [Table("DetalleSolicitudes", Schema = "dbo")]
    public class DetalleSolicitud
    {
        [Key]
        public int IdDetalleSol { get; set; }

        public int IdSolicitud { get; set; }

        public Solicitud Solicitud { get; set; }

        public int IdCurso { get; set; }

        public Curso Curso { get; set; }

        public string Profesor { get; set; }

        public string Aula { get; set; }

        public string Sede { get; set; }

        public string Observacion { get; set; }
    }
}
