using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UPC.PRUEBA.WEB.ViewModels
{
    public class SolicitudDetalleViewModel
    {
        public int? IdDetalleSol { get; set; }

        public int IdSolicitud { get; set; }

        public int IdCurso { get; set; }

        public string Curso { get; set; }

        public int CursoCreditos { get; set; }

        public string Profesor { get; set; }

        public string Aula { get; set; }

        public string Sede { get; set; }

        public string Observacion { get; set; }

        public SelectList Cursos { get; set; }

    }
}