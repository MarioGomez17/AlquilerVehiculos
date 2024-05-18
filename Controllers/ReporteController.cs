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
        public IActionResult ReporteAlquileresAlquilador()
        {
            ModeloAlquileresUsuario ModeloAlquileresUsuario = new(DatosUsuarioSesion().Id);
            return View(ModeloAlquileresUsuario.HistorialAlquileresAlquilador);
        }
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        public IActionResult GenerarReporteAlquileresAlquiladorPDF(){
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
        public IActionResult EnviarReportesAlquileresAlquiladorPorCorreo(string Correo)
        {
            ReporteAlquileresAlquilador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.EnviarReportesAlquileresAlquiladorPorCorreo(Correo);
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult GenerarReporteAlquileresPropietarioPDF(){
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
        public IActionResult EnviarReportesAlquileresPropietarioPorCorreo(string Correo)
        {
            ReporteAlquileresPropietario Reporte = new(DatosUsuarioSesion().Id);
            Reporte.EnviarReportesAlquileresPropietarioPorCorreo(Correo);
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        [Authorize(Policy = "SoloAdministrador")]
        public IActionResult GenerarReporteAlquileresAdministradorPDF(){
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
        public IActionResult EnviarReportesAlquileresAdministradorPorCorreo(string Correo)
        {
            ReporteAlquileresAdministrador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.EnviarReportesAlquileresAdministradorPorCorreo(Correo);
            return RedirectToAction("GestionarTodosAlquileres", "Administrador");
        }
    }
}