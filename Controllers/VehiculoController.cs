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
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        [Authorize]
        public IActionResult RegistrarVehiculo()
        {
            return View();
        }
        [Authorize]
        public IActionResult InformacionVehiculos()
        {
            ModeloPropietario ModeloPropietario = new();
            ModeloPropietario = ModeloPropietario.TraerPropietario(DatosUsuarioSesion().Id);
            ModeloVehiculo ModeloVehiculo = new();
            List<ModeloVehiculo> Vehiculos = ModeloVehiculo.TraerTodosVehiculosPropietario(ModeloPropietario.Id);
            return View(Vehiculos);
        }
        [Authorize]
        public IActionResult InformacionVehiculo(int Id_Vehiculo)
        {
            ModeloVehiculo ModeloVehiculo = new();
            return View(ModeloVehiculo.TraerVehiculo(Id_Vehiculo));
        }
        public IActionResult InformacionVehiculoCrearAlquiler(int Id_Vehiculo)
        {
            ModeloVehiculo ModeloVehiculo = new();
            return View(ModeloVehiculo.TraerVehiculo(Id_Vehiculo));
        }
    }
}

