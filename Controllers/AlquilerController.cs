using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
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
            DateOnly[] Fechas = [FechaInicio, FechaFin];
            ModeloCrearAlquiler ModeloCrearAlquiler = new(IdVehiculo);
            ViewBag.Message = Fechas;
            return View(ModeloCrearAlquiler);
        }
        [Authorize]
        public IActionResult InformacionAlquilerAlquilador(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler = ModeloAlquiler.TraerAlquiler(IdAlquiler);
            return View(ModeloAlquiler);
        }
        [Authorize]
        public IActionResult InformacionAlquilerPropietario(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquiler(IdAlquiler));
        }
        [Authorize]
        public IActionResult CalificarAlquiler(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquiler(IdAlquiler));
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
        public IActionResult AccionFinalizarAlquiler(int IdAlquiler)
        {
            ModeloAlquiler ModeloAlquiler = new();
            ModeloAlquiler.FinalizarAlquiler(IdAlquiler);
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
    }
}
