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
            var UserSessionData = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(UserSessionData);
        }
        public IActionResult Inicio()
        {
            ModeloVehiculo VehicleModel = new();
            return View(VehicleModel.TraerTodosVehiculos());
        }

        public IActionResult InicioFiltrado(string Ciudad)
        {
            ModeloVehiculo VehicleModel = new();
            return View(VehicleModel.TraerTodosVehiculos(Ciudad));
        }
    }
}
