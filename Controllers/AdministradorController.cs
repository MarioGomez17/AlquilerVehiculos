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
        
        public IActionResult GestionarTodosUsuarios()
        {
            return View();
        }
        public IActionResult GestionarTodosVehiculos()
        {
            return View();
        }
        public IActionResult GestionarTodosAlquileres()
        {
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquileres());
        }
        public IActionResult GestionarTipoVehiculo()
        {
            return View();
        }
        public IActionResult GestionarClasificacionVehiculo()
        {
            return View();
        }
        public IActionResult GestionarMarcaVehiculo()
        {
            return View();
        }
        public IActionResult GestionarLineaVehiculo()
        {
            return View();
        }
        public IActionResult GestionarTipoCombustible()
        {
            return View();
        }
        public IActionResult GestionarCiudad()
        {
            ModeloDepartamento ModeloDepartamento = new();
            return View(ModeloDepartamento.TraerDepartamentos());
        }
        public IActionResult GestionarSeguroAlquiler()
        {
            return View();
        }
        public IActionResult GestionarMetodoPago()
        {
            return View();
        }
        public IActionResult GestionarLugarRecogidaEntrega()
        {
            return View();
        }
        public IActionResult VerUsuarioAdministrador()
        {
            return View();
        }
        public IActionResult GestionarTipoIdentificacion()
        {
            return View();
        }
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        public IActionResult AccionAgregarCiudad(int Departamento, string Ciudad){
            ModeloCiudad ModeloCiudad = new();
            ModeloCiudad.AgregarCiudad(Departamento, Ciudad);
            return RedirectToAction("GestionarCiudad", "Administrador");
        }
        
        public IActionResult AccionActualizarCiudad(int Id, string Ciudad){
            ModeloCiudad ModeloCiudad = new();
            ModeloCiudad.ActualizarCiudad(Id, Ciudad);
            return RedirectToAction("GestionarCiudad", "Administrador");
        }

    }
}