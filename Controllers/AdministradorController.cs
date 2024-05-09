using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class AdministradorController : Controller
    {
        [Authorize]
        public IActionResult VerTodosUsuarios()
        {
            return View();
        }
        [Authorize]
        public IActionResult VerTodosVehiculos()
        {
            return View();
        }
        [Authorize]
        public IActionResult VerTodosAlquileres()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarTipoVehiculo()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarClasificacionVehiculo()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarMarcaVehiculo()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarLineaVehiculo()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarTipoCombustible()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarCiudad()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarSeguroAlquiler()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarMetodoPago()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarLugarRecogidaEntrega()
        {
            return View();
        }
        [Authorize]
        public IActionResult VerUsuarioAdministrador()
        {
            return View();
        }
        [Authorize]
        public IActionResult AgregarTipoIdentificacion()
        {
            return View();
        }
    }
}