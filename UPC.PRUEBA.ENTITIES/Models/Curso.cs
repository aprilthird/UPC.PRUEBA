using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.PRUEBA.ENTITIES.Models
{
    [Table("Cursos", Schema = "dbo")]
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int NroCreditos { get; set; }

        public bool Activo { get; set; }

        public List<DetalleSolicitud> DetalleSolicitudes { get; set; }
    }
}
