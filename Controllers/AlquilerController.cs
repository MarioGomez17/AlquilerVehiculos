using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class AlquilerController : Controller
    {
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }

        [Authorize]
        public IActionResult CrearAlquiler(int Id_vehiculo)
        {
            ModeloCrearAlquiler ModeloCrearAlquiler = new(Id_vehiculo);
            return View(ModeloCrearAlquiler);
        }

        public IActionResult AccionCrearAlquiler(DateTime FechaInicio, DateTime FechaFin, float Precio, string SiLavada, int Vehiculo, int Lugar, int MetodoPago, int Seguro)
        {
            ModeloAlquiler ModeloAlquiler = new();
            int Lavada = SiLavada == "on" ? 1 : 0;
            ModeloAlquilador ModeloAlquilador = new();
            if (ModeloAlquilador.ValidarAlquilador(DatosUsuarioSesion().Id))
            {
                ModeloAlquilador.CrearAlquilador(DatosUsuarioSesion().Id);
            }
            ModeloAlquilador = ModeloAlquilador.TraerAlquiladorUsuario(DatosUsuarioSesion().Id);
            int Alquilador = ModeloAlquilador.Id;
            ModeloAlquiler.CrearAquiler(FechaInicio, FechaFin, Precio, Lavada, Alquilador, Vehiculo, Lugar, MetodoPago, Seguro);
            return RedirectToAction("Inicio", "Inicio");
        }

        [Authorize]
        public IActionResult InformacionAlquiler(int IdAlquiler)
        {
            return View();
        }

        [Authorize]
        public IActionResult CalificarAlquiler()
        {
            return View();
        }

        [Authorize]
        public IActionResult HistorialAlquileres()
        {
            ModeloAlquileresUsuario ModeloAlquileresUsuario = new(DatosUsuarioSesion().Id);
            return View(ModeloAlquileresUsuario);
        }

        [Authorize]
        public IActionResult ObtenerPrecioAlquiler(int IdVehiculo, int IdSeguro)
        {
            ModeloSeguroAlquiler ModeloSeguroAlquiler = new();
            ModeloSeguroAlquiler = ModeloSeguroAlquiler.TraerSeguroAlquiler(IdSeguro);
            ModeloVehiculo ModeloVehiculo = new();
            ModeloVehiculo = ModeloVehiculo.TraerVehiculo(IdVehiculo);
            return Json(new { PrecioSeguro = ModeloSeguroAlquiler.PrecioSeguroAlquiler, PrecioAlquilerDiaVehiculo = ModeloVehiculo.PrecioAlquilerDia });
        }
    }
}
