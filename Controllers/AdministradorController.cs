using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    [Authorize(Policy = "SoloAdministrador")]
    public class AdministradorController : Controller
    {
        
        public IActionResult VerTodosUsuarios()
        {
            return View();
        }
        public IActionResult VerTodosVehiculos()
        {
            return View();
        }
        public IActionResult VerTodosAlquileres()
        {
            return View();
        }
        public IActionResult AgregarTipoVehiculo()
        {
            return View();
        }
        public IActionResult AgregarClasificacionVehiculo()
        {
            return View();
        }
        public IActionResult AgregarMarcaVehiculo()
        {
            return View();
        }
        public IActionResult AgregarLineaVehiculo()
        {
            return View();
        }
        public IActionResult AgregarTipoCombustible()
        {
            return View();
        }
        public IActionResult AgregarCiudad()
        {
            return View();
        }
        public IActionResult AgregarSeguroAlquiler()
        {
            return View();
        }
        public IActionResult AgregarMetodoPago()
        {
            return View();
        }
        public IActionResult AgregarLugarRecogidaEntrega()
        {
            return View();
        }
        public IActionResult VerUsuarioAdministrador()
        {
            return View();
        }
        public IActionResult AgregarTipoIdentificacion()
        {
            return View();
        }
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
    }
}