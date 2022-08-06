using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UPC.PRUEBA.WEB.ViewModels
{
    public class SolicitudViewModel
    {
        public int? IdSolicitud { get; set; }

        [Display(Name = "Alumno")]
        public int IdAlumno { get; set; }

        public string AlumnoNombres { get; set; }
        
        public string AlumnoApellidos { get; set; }

        public DateTime FechaSolicitud { get; set; }

        [Display(Name = "Codigo Registrante")]
        public string CodRegistrante { get; set; }

        [Display(Name = "Carrera")]
        public string Carrera { get; set; }

        [Display(Name = "Periodo")]
        public string Periodo { get; set; }

        public int NroCursos { get; set; }

        public List<SolicitudDetalleViewModel> SolicitudDetalles { get; set; }

        public SelectList Alumnos { get; set; }

    }
}