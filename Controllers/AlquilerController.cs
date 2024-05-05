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
        public IActionResult InformacionAlquilerAlquilador(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquiler(IdAlquiler));
        }
        [Authorize]
        public IActionResult InformacionAlquilerPropietario(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquiler(IdAlquiler));
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
            return RedirectToAction("HistorialAlquileres", "Alquiler");
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
        public IActionResult AccionIniciarAlquiler(int IdAlquiler){
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler.IniciarAlquiler(IdAlquiler);
            InformacionAlquilerAlquilador(IdAlquiler);
            return RedirectToAction("InformacionAlquilerAlquilador", "Alquiler");
        }
        public IActionResult AccionFinalizarAlquiler(int IdAlquiler){
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler.FinalizarAlquiler(IdAlquiler);
            InformacionAlquilerPropietario(IdAlquiler);
            return RedirectToAction("InformacionAlquilerPropietario", "Alquiler");
        }
        public IActionResult AccionCancelarAlquiler(int IdAlquiler){
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler.CancelarAlquiler(IdAlquiler);
            HistorialAlquileres();
            return RedirectToAction("HistorialAlquileres", "Alquiler");
        }
        public IActionResult AccionPagarAlquiler(int IdAlquiler){
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler.PagarAlquiler(IdAlquiler);
            InformacionAlquilerAlquilador(IdAlquiler);
            return RedirectToAction("InformacionAlquilerAlquilador", "Alquiler");
        }
    }
}
