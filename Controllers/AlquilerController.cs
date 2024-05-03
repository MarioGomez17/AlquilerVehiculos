using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;
using System.Globalization;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class AlquilerController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        public static DateTime FechaInicio;
        public static DateTime FechaFin;
        [Authorize]
        public IActionResult CrearAlquiler(int IdVehiculo)
        {
            DateTime[] Fechas = [FechaInicio, FechaFin];
            ModeloCrearAlquiler ModeloCrearAlquiler = new(IdVehiculo);
            ViewBag.Message = Fechas;
            return View(ModeloCrearAlquiler);
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
        //---------------------------------------------- ACCIONES ----------------------------------------------
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        [HttpPost]
        public IActionResult AccionCrearAlquiler(string FechaInicio, string FechaFin, float Precio, string SiLavada, int Vehiculo, int Lugar, int MetodoPago, int Seguro)
        {
            DateTime ValorFiltroFechaInicio = DateTime.Parse(FechaInicio);
            DateTime ValorFiltroFechaFin = DateTime.Parse(FechaFin);
            ModeloAlquiler ModeloAlquiler = new();
            int Lavada = SiLavada == "on" ? 1 : 0;
            ModeloAlquilador ModeloAlquilador = new();
            if (ModeloAlquilador.ValidarAlquilador(DatosUsuarioSesion().Id))
            {
                ModeloAlquilador.CrearAlquilador(DatosUsuarioSesion().Id);
            }
            ModeloAlquilador = ModeloAlquilador.TraerAlquiladorUsuario(DatosUsuarioSesion().Id);
            int Alquilador = ModeloAlquilador.Id;
            ModeloAlquiler.CrearAquiler(ValorFiltroFechaInicio, ValorFiltroFechaFin, Precio, Lavada, Alquilador, Vehiculo, Lugar, MetodoPago, Seguro);
            return RedirectToAction("Inicio", "Inicio");
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
