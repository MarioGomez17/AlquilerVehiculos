using Microsoft.AspNetCore.Mvc;
using ALQUILER_VEHICULOS.Models;
using ALQUILER_VEHICULOS.Reports;
using Newtonsoft.Json;
using System.Security.Claims;
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
        public IActionResult GenerarReportePDF(){
            ReporteAlquileresAlquilador Reporte = new();
            Reporte.GenerarReporteAlquileresAlquilador(DatosUsuarioSesion().Id);
            return RedirectToAction("ReporteAlquileresAlquilador", "Reporte");
        }
    }
}