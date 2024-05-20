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
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
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
        [Authorize(Policy = "SoloAdministrador")]
        public IActionResult GenerarReporteAlquileresAdministradorPDF()
        {
            ReporteAlquileresAdministrador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresAdministradorPDF();
            return RedirectToAction("GestionarTodosAlquileres", "Administrador");
        }
        [Authorize(Policy = "SoloAdministrador")]
        public IActionResult GenerarReporteAlquileresAdministradorEXCEL()
        {
            ReporteAlquileresAdministrador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresAdministradorEXCEL();
            return RedirectToAction("GestionarTodosAlquileres", "Administrador");
        }
        [Authorize(Policy = "SoloAdministrador")]
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
    }
}