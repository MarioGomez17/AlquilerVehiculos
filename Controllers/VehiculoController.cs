using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class VehiculoController : Controller
    {
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var UserSessionData = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(UserSessionData);
        }

        [Authorize]
        public IActionResult RegistrarVehiculo()
        {
            return View();
        }

        [Authorize]
        public IActionResult InformacionVehiculos()
        {
            return View();
        }

        [Authorize]
        public IActionResult InformacionVehiculo(int Id_Vehiculo)
        {
            ModeloVehiculo Vehicle = new();
            return View(Vehicle.TraerVehiculo(Id_Vehiculo));
        }

        public IActionResult InformacionVehiculoCrearAlquiler(int Id_Vehiculo)
        {
            ModeloVehiculo Vehicle = new();
            return View(Vehicle.TraerVehiculo(Id_Vehiculo));
        }
    }
}

