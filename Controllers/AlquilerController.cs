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

        [Authorize]
        public IActionResult InformacionAlquiler()
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
            return View();
        }

        public IActionResult ObtenerPrecioAlquiler()
        {
            string PrecioAlquiler = "Hola, este es tu mensaje";
            return Json(new { PrecioAlquiler });
        }
    }
}
