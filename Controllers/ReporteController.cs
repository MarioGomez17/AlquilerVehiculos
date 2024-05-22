using Microsoft.AspNetCore.Mvc;
using ALQUILER_VEHICULOS.Models;
using ALQUILER_VEHICULOS.Reports;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace ALQUILER_VEHICULOS.Controllers
{
    public class ReporteController : Controller
    {
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            if (Identity.FindFirst(ClaimTypes.UserData) != null)
            {
                var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
                return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
            }
            else
            {
                return null;
            }
        }
        public IActionResult GenerarReporteAlquileresAlquiladorPDF()
        {
            ReporteAlquileresAlquilador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresAlquiladorPDF();
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult GenerarReporteAlquileresAlquiladorEXCEL()
        {
            ReporteAlquileresAlquilador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresAlquiladorEXCEL();
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult EnviarReportesAlquileresAlquiladorPorCorreo()
        {
            ReporteAlquileresAlquilador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.EnviarReportesAlquileresAlquiladorPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult GenerarReporteAlquileresPropietarioPDF()
        {
            ReporteAlquileresPropietario Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresPropietarioPDF();
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult GenerarReporteAlquileresPropietarioEXCEL()
        {
            ReporteAlquileresPropietario Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresPropietarioEXCEL();
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult EnviarReportesAlquileresPropietarioPorCorreo()
        {
            ReporteAlquileresPropietario Reporte = new(DatosUsuarioSesion().Id);
            Reporte.EnviarReportesAlquileresPropietarioPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult GenerarReporteAlquileresAdministradorPDF()
        {
            ReporteAlquileresAdministrador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresAdministradorPDF();
            return RedirectToAction("GestionarTodosAlquileres", "Administrador");
        }
        public IActionResult GenerarReporteAlquileresAdministradorEXCEL()
        {
            ReporteAlquileresAdministrador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresAdministradorEXCEL();
            return RedirectToAction("GestionarTodosAlquileres", "Administrador");
        }
        public IActionResult EnviarReportesAlquileresAdministradorPorCorreo()
        {
            ReporteAlquileresAdministrador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.EnviarReportesAlquileresAdministradorPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("GestionarTodosAlquileres", "Administrador");
        }
        public IActionResult GenerarReporteAlquilerPropietarioPDF(int IdAlquiler)
        {
            ReporteAlquilerPropietario Reporte = new(DatosUsuarioSesion().Id, IdAlquiler);
            Reporte.GenerarReporteAlquilerPropietarioPDF();
            return RedirectToAction("InformacionAlquilerPropietario", "Alquiler", new { IdAlquiler });
        }
        public IActionResult EnviarReportesAlquilerPropietarioPorCorreo(int IdAlquiler)
        {
            ReporteAlquilerPropietario Reporte = new(DatosUsuarioSesion().Id, IdAlquiler);
            Reporte.EnviarReportesAlquilerPropietarioPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("InformacionAlquilerPropietario", "Alquiler", new { IdAlquiler });
        }
        public IActionResult GenerarReporteAlquilerAdministradorPDF(int IdAlquiler)
        {
            ReporteAlquilerAdministrador Reporte = new(DatosUsuarioSesion().Id, IdAlquiler);
            Reporte.GenerarReporteAlquilerAdministradorPDF();
            return RedirectToAction("VerAlquilerAdministrador", "Administrador", new { IdAlquiler });
        }
        public IActionResult EnviarReportesAlquilerAdministradorPorCorreo(int IdAlquiler)
        {
            ReporteAlquilerAdministrador Reporte = new(DatosUsuarioSesion().Id, IdAlquiler);
            Reporte.EnviarReportesAlquilerAdministradorPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("VerAlquilerAdministrador", "Administrador", new { IdAlquiler });
        }
        public IActionResult GenerarReporteAlquilerAlquiladorPDF(int IdAlquiler)
        {
            ReporteAlquilerAlquilador Reporte = new(DatosUsuarioSesion().Id, IdAlquiler);
            Reporte.GenerarReporteAlquilerAlquiladorPDF();
            return RedirectToAction("InformacionAlquilerAlquilador", "Alquiler", new { IdAlquiler });
        }
        public IActionResult EnviarReportesAlquilerAlquiladorPorCorreo(int IdAlquiler)
        {
            ReporteAlquilerAlquilador Reporte = new(DatosUsuarioSesion().Id, IdAlquiler);
            Reporte.EnviarReportesAlquilerAlquiladorPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("InformacionAlquilerAlquilador", "Alquiler", new { IdAlquiler });
        }
        public IActionResult GenerarReporteUsuarioPDF()
        {
            ReporteUsuario Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteUsuarioPDF();
            return RedirectToAction("InformacionUsuario", "Usuario");
        }
        public IActionResult EnviarReportesUsuarioPorCorreo()
        {
            ReporteUsuario Reporte = new(DatosUsuarioSesion().Id);
            Reporte.EnviarReportesUsuarioPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("InformacionUsuario", "Usuario");
        }
        public IActionResult GenerarReporteUsuariosAdministradorPDF()
        {
            ReporteUsuarios Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteUsuariosPDF();
            return RedirectToAction("GestionarTodosUsuarios", "Administrador");
        }
        public IActionResult GenerarReporteUsuariosAdministradorExcel()
        {
            ReporteUsuarios Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteUsuariosEXCEL();
            return RedirectToAction("GestionarTodosUsuarios", "Administrador");
        }
        public IActionResult EnviarReportesUsuariosAdministradorPorCorreo()
        {
            ReporteUsuarios Reporte = new(DatosUsuarioSesion().Id);
            Reporte.EnviarReportesUsuariosPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("GestionarTodosUsuarios", "Administrador");
        }
        public IActionResult GenerarReporteUsuarioAdministradorPDF(int IdUsuario)
        {
            ReporteUsuarioAdministrador Reporte = new(DatosUsuarioSesion().Id, IdUsuario);
            Reporte.GenerarReporteUsuarioAdministradorPDF();
            return RedirectToAction("VerUsuarioAdministrador", "Administrador", new { IdUsuario });
        }
        public IActionResult EnviarReportesUsuarioAdministradorPorCorreo(int IdUsuario)
        {
            ReporteUsuarioAdministrador Reporte = new(DatosUsuarioSesion().Id, IdUsuario);
            Reporte.EnviarReportesUsuarioAdministradorPorCorreo(DatosUsuarioSesion().Correo);
            return RedirectToAction("VerUsuarioAdministrador", "Administrador", new { IdUsuario });
        }
    }
}