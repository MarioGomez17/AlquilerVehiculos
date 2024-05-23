using Microsoft.AspNetCore.Mvc;
using ALQUILER_VEHICULOS.Models;
using ALQUILER_VEHICULOS.Reports;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Diagnostics;
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquileresAlquilador" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult GenerarReporteAlquileresAlquiladorEXCEL()
        {
            ReporteAlquileresAlquilador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresAlquiladorEXCEL();
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquileresAlquilador" + DatosUsuarioSesion().NumeroIdentificacion + ".xlsx";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquileresPropietario" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult GenerarReporteAlquileresPropietarioEXCEL()
        {
            ReporteAlquileresPropietario Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresPropietarioEXCEL();
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquileresPropietario" + DatosUsuarioSesion().NumeroIdentificacion + ".xlsx";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquileresAdministrador" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
            return RedirectToAction("GestionarTodosAlquileres", "Administrador");
        }
        public IActionResult GenerarReporteAlquileresAdministradorEXCEL()
        {
            ReporteAlquileresAdministrador Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteAlquileresAdministradorEXCEL();
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquileresAdministrador" + DatosUsuarioSesion().NumeroIdentificacion + ".xlsx";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquilerPropietario" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquilerAdministrador" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteAlquilerAlquilador" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteUsuario" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteUsuarios" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
            return RedirectToAction("GestionarTodosUsuarios", "Administrador");
        }
        public IActionResult GenerarReporteUsuariosAdministradorExcel()
        {
            ReporteUsuarios Reporte = new(DatosUsuarioSesion().Id);
            Reporte.GenerarReporteUsuariosEXCEL();
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteUsuarios" + DatosUsuarioSesion().NumeroIdentificacion + ".xlsx";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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
            string RutaActual = Directory.GetCurrentDirectory();
            string RutaArchivo = RutaActual + @"\wwwroot\Reportes\ReporteUsuarioAdministrador" + DatosUsuarioSesion().NumeroIdentificacion + ".pdf";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {RutaArchivo}") { CreateNoWindow = true });
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