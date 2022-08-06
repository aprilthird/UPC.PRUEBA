using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UPC.PRUEBA.CORE.Helpers;
using UPC.PRUEBA.ENTITIES.Models;
using UPC.PRUEBA.WEB.Seeder;
using UPC.PRUEBA.WEB.ViewModels;

namespace UPC.PRUEBA.WEB.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController() : base()
        {
        }

        public async Task<ActionResult> Index()
        {
            if(ConstantHelpers.GENERAL.SEEDS_ENABLED)
            {
                await DbSeeder.Seed(_context);
            }

            return View();
        }

        public async Task<ActionResult> Solicitudes(string Periodo = null, string Fecha = null, string Alumno = null, string Curso = null)
        {
            var query = _context.Solicitudes.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(Periodo))
                query = query.Where(x => x.Periodo.Contains(Periodo));

            if (!string.IsNullOrEmpty(Alumno))
                query = query.Where(x => x.Alumno.Nombres.Contains(Alumno) || x.Alumno.Apellidos.Contains(Alumno));

            if (!string.IsNullOrEmpty(Curso))
                query = query.Where(x => x.DetalleSolicitudes.Any(y => y.Curso.Nombre.Contains(Curso)));

            //if (string.IsNullOrEmpty(Curso))
            //    query = query.Where(x => x.Periodo.Contains(Periodo));

            var model = await query
                .Select(x => new SolicitudViewModel 
                {
                    IdSolicitud = x.IdSolicitud,
                    AlumnoNombres = x.Alumno.Nombres,
                    AlumnoApellidos = x.Alumno.Apellidos,
                    Carrera = x.Carrera,
                    Periodo = x.Periodo,
                    CodRegistrante = x.CodRegistrante,
                    FechaSolicitud = x.FechaSolicitud,
                    IdAlumno = x.IdAlumno
                }).ToListAsync();

            return View(model);
        }

        public async Task<ActionResult> Solicitud(int? IdSolicitud = null)
        {
            var model = new SolicitudViewModel();

            var alumnosLst = await _context.Alumnos.AsNoTracking().ToListAsync();
            model.Alumnos = new SelectList(alumnosLst.Select(x => new SelectListItem { Value = x.IdAlumno.ToString(), Text = $"{x.Apellidos}, {x.Nombres}" }).ToList());

            if (IdSolicitud.HasValue)
            {
                var solInDb = await _context.Solicitudes.FindAsync(IdSolicitud.Value);
                model.IdAlumno = solInDb.IdAlumno;
                model.Periodo = solInDb.Periodo;
                model.Carrera = solInDb.Carrera;
                model.CodRegistrante = solInDb.CodRegistrante;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Solicitud(SolicitudViewModel viewModel, int? IdSolicitud = null)
        {
            var dets = await _context.DetalleSolicitudes
                .Where(x => x.IdSolicitud == IdSolicitud)
                .ToListAsync();

            if (IdSolicitud.HasValue)
            {
                var solInDb = await _context.Solicitudes.FindAsync(IdSolicitud.Value);
                solInDb.IdAlumno = viewModel.IdAlumno;
                solInDb.Periodo = viewModel.Periodo;
                solInDb.Carrera = viewModel.Carrera;
                solInDb.CodRegistrante = viewModel.CodRegistrante;
                await _context.SaveChangesAsync();
            }
            else
            {
                var sol = new Solicitud
                {
                    IdAlumno = viewModel.IdAlumno,
                    Periodo = viewModel.Periodo,
                    Carrera = viewModel.Carrera,
                    CodRegistrante = viewModel.CodRegistrante,
                    FechaSolicitud = DateTime.Now,
                };

                _context.Solicitudes.Add(sol);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Solicitudes");
        }

        public async Task<ActionResult> Detalles(int IdSolicitud)
        {
            var model = new SolicitudViewModel();
            var solInDb = await _context.Solicitudes.FindAsync(IdSolicitud);
            model.IdSolicitud = IdSolicitud;
            model.IdAlumno = solInDb.IdAlumno;
            model.Periodo = solInDb.Periodo;
            model.Carrera = solInDb.Carrera;
            model.CodRegistrante = solInDb.CodRegistrante;
            model.SolicitudDetalles = await _context.DetalleSolicitudes
                .Where(x => x.IdSolicitud == IdSolicitud)
                .Select(x => new SolicitudDetalleViewModel
                {
                    IdDetalleSol = x.IdDetalleSol,
                    IdSolicitud = x.IdSolicitud,
                    Aula = x.Aula,
                    Profesor = x.Profesor,
                    IdCurso = x.IdCurso,
                    Curso = x.Curso.Nombre,
                    CursoCreditos = x.Curso.NroCreditos,
                    Observacion = x.Observacion,
                    Sede = x.Sede
                }).AsNoTracking().ToListAsync();
            return View(model);
        }

        public async Task<ActionResult> Detalle(int IdSolicitud, int? IdSolDetalle = null)
        {
            var model = new SolicitudDetalleViewModel();

            var cursosLst = await _context.Cursos.Where(x => x.Activo).AsNoTracking().ToListAsync();
            model.Cursos = new SelectList(cursosLst.Select(x => new SelectListItem { Value = x.IdCurso.ToString(), Text = x.Nombre }).ToList());

            if (IdSolDetalle.HasValue)
            {
                var detInDb = await _context.DetalleSolicitudes.FindAsync(IdSolDetalle.Value);
                model.IdDetalleSol = detInDb.IdDetalleSol;
                model.IdSolicitud = detInDb.IdSolicitud;
                model.IdCurso = detInDb.IdCurso;
                model.Aula = detInDb.Aula;
                model.Profesor = detInDb.Profesor;
                model.Observacion = detInDb.Observacion;
                model.Sede = detInDb.Sede;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Detalle(int IdSolicitud, SolicitudDetalleViewModel viewModel, int? IdSolDetalle = null)
        {
            var dets = await _context.DetalleSolicitudes
                .Where(x => x.IdSolicitud == IdSolicitud)
                .ToListAsync();

            var cursosLst = await _context.Cursos.Where(x => x.Activo).AsNoTracking().ToListAsync();
            viewModel.Cursos = new SelectList(cursosLst.Select(x => new SelectListItem { Value = x.IdCurso.ToString(), Text = x.Nombre }).ToList());

            if (dets.Any(d => d.IdCurso == viewModel.IdCurso || ))
            {
                return View(viewModel);
            }

            if (IdSolDetalle.HasValue)
            {
                var detInDb = await _context.DetalleSolicitudes.FindAsync(IdSolDetalle.Value);
                detInDb.IdCurso = viewModel.IdCurso;
                detInDb.Aula = viewModel.Aula;
                detInDb.Profesor = viewModel.Profesor;
                detInDb.Observacion = viewModel.Observacion;
                detInDb.Sede = viewModel.Sede;
                await _context.SaveChangesAsync();
            }
            else
            {
                var det = new DetalleSolicitud
                {
                    IdSolicitud = IdSolicitud,
                    IdCurso = viewModel.IdCurso,
                    Aula = viewModel.Aula,
                    Profesor = viewModel.Profesor,
                    Observacion = viewModel.Observacion,
                    Sede = viewModel.Sede,
                };

                _context.DetalleSolicitudes.Add(det);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Detalles", new { IdSolicitud = IdSolicitud });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}