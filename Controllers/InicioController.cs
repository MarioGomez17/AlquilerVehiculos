using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class InicioController : Controller
    {
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        public IActionResult Inicio()
        {
            ModeloVehiculo ModeloVehiculo = new();
            return View(ModeloVehiculo.TraerTodosVehiculos());
        }
        public IActionResult InicioFiltrado(string Ciudad)
        {
            ModeloVehiculo ModeloVehiculo = new();
            return View(ModeloVehiculo.TraerTodosVehiculos(Ciudad));
        }
    }
}
