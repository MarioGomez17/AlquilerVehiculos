using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class AlquilerController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        public static DateOnly FechaInicio;
        public static DateOnly FechaFin;
        [Authorize]
        public IActionResult CrearAlquiler(int IdVehiculo)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ViewBag.FechaInicio = FechaInicio;
            ViewBag.FechaFin = FechaFin;
            ModeloCrearAlquiler ModeloCrearAlquiler = new(IdVehiculo);
            return View(ModeloCrearAlquiler);
        }
        [Authorize]
        public IActionResult InformacionAlquilerAlquilador(int IdAlquiler)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler = ModeloAlquiler.TraerAlquiler(IdAlquiler);
            return View(ModeloAlquiler);
        }
        [Authorize]
        public IActionResult InformacionAlquilerPropietario(int IdAlquiler)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquiler(IdAlquiler));
        }
        [Authorize]
        public IActionResult CalificarAlquiler(int IdAlquiler)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquiler(IdAlquiler));
        }
        [Authorize]
        public IActionResult HistorialAlquileresAsync()
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario.EliminarAlquileresPendientesUsuario(DatosUsuarioSesion().Id);
            _ = ActualizarDatosUsuarioSesion();
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ModeloAlquileresUsuario ModeloAlquileresUsuario = new(DatosUsuarioSesion().Id);
            return View(ModeloAlquileresUsuario);
        }
        //---------------------------------------------- ACCIONES ----------------------------------------------
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
        public IActionResult AccionCrearAlquiler(string FechaInicio, string FechaFin, float Precio, string SiLavada, int Vehiculo, int Lugar, int MetodoPago, int Seguro)
        {
            DateOnly ValorFiltroFechaInicio = DateOnly.Parse(FechaInicio);
            DateOnly ValorFiltroFechaFin = DateOnly.Parse(FechaFin);
            ModeloAlquiler ModeloAlquiler = new();
            int Lavada = SiLavada == "on" ? 1 : 0;
            ModeloAlquilador ModeloAlquilador = new();
            if (ModeloAlquilador.ValidarAlquilador(DatosUsuarioSesion().Id))
            {
                ModeloAlquilador.CrearAlquilador(DatosUsuarioSesion().Id);
            }
            ModeloAlquilador = ModeloAlquilador.TraerAlquiladorUsuario(DatosUsuarioSesion().Id);
            int Alquilador = ModeloAlquilador.Id;
            float Ganancias = (float)(Precio * 0.15);
            ModeloAlquiler.CrearAquiler(ValorFiltroFechaInicio, ValorFiltroFechaFin, Precio, Lavada, Alquilador, Vehiculo, Lugar, MetodoPago, Seguro, Ganancias);
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult ObtenerPrecioAlquiler(int IdVehiculo, int IdSeguro)
        {
            ModeloSeguroAlquiler ModeloSeguroAlquiler = new();
            ModeloSeguroAlquiler = ModeloSeguroAlquiler.TraerSeguroAlquiler(IdSeguro);
            ModeloVehiculo ModeloVehiculo = new();
            ModeloVehiculo = ModeloVehiculo.TraerVehiculo(IdVehiculo);
            return Json(new { PrecioSeguro = ModeloSeguroAlquiler.PrecioSeguroAlquiler, PrecioAlquilerDiaVehiculo = ModeloVehiculo.PrecioAlquilerDia });
        }
        public IActionResult AccionIniciarAlquiler(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler.IniciarAlquiler(IdAlquiler);
            return RedirectToAction("InformacionAlquilerAlquilador", "Alquiler", new { IdAlquiler });
        }
        public IActionResult AccionFinalizarAlquiler(int IdAlquiler, int DiasRetraso)
        {
            ModeloAlquiler ModeloAlquiler = new();
            if (DiasRetraso > 0)
            {
                ModeloAlquiler.RecalcularPrecioAlquiler(IdAlquiler, DiasRetraso);
                ModeloAlquiler.FinalizarAlquiler(IdAlquiler);
            }
            else
            {
                ModeloAlquiler.FinalizarAlquiler(IdAlquiler);
            }
            return RedirectToAction("InformacionAlquilerPropietario", "Alquiler", new { IdAlquiler });
        }
        public IActionResult AccionCancelarAlquiler(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler.CancelarAlquiler(IdAlquiler);
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult AccionPagarAlquiler(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler.PagarAlquiler(IdAlquiler);
            return RedirectToAction("InformacionAlquilerAlquilador", "Alquiler", new { IdAlquiler });
        }
        public IActionResult AccionCalificarAlquiler(int IdAlquiler, string Calificacion, string Comentario)
        {
            ModeloAlquiler modeloAlquiler = new();
            modeloAlquiler.CalificarAlquiler(IdAlquiler, Calificacion, Comentario);
            return RedirectToAction("InformacionAlquilerAlquilador", "Alquiler", new { IdAlquiler });
        }
        private async Task ActualizarDatosUsuarioSesion()
        {
            int IdUsuario = DatosUsuarioSesion().Id;
            ModeloUsuario ModeloUsuario = new();
            var ClaimPrincipal = (ClaimsIdentity)User.Identity;
            var ClaimUsuarioActual = ClaimPrincipal.FindFirst(ClaimTypes.UserData);
            if (ClaimUsuarioActual != null)
            {
                ClaimPrincipal.RemoveClaim(ClaimUsuarioActual);
            }
            Claim nuevoClaimUsuario = new(ClaimTypes.UserData, JsonConvert.SerializeObject(ModeloUsuario.TraerUsuario(IdUsuario)));
            ClaimPrincipal.AddClaim(nuevoClaimUsuario);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimPrincipal));
        }
    }
}
