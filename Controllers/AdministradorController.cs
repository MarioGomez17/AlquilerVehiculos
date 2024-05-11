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
            ModeloTipoVehiculo TiModeloTipoVehiculo = new();
            return View(TiModeloTipoVehiculo.TraerTodosTipoVehiculo());
        }
        public IActionResult GestionarMarcaVehiculo()
        {
            return View();
        }
        public IActionResult GestionarLineaVehiculo()
        {
            ModeloMarca Marca= new();
            return View(Marca.TraerTodosMetodasMarcas());
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
            ModeloLugarAlquiler LugaresAlquiler = new();
            return View(LugaresAlquiler.TraerTodosLugaresAlquiler());
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
        public IActionResult AccionAgregarLugar(string Lugar){
            ModeloLugarAlquiler ModeloLugar = new();
            ModeloLugar.AgregarLugarRecogidaEntrega(Lugar);
            return RedirectToAction("GestionarLugarRecogidaEntrega", "Administrador");
        }
        public IActionResult AccionActualizarLugar(int Id, string Lugar){
            ModeloLugarAlquiler ModeloLugar = new();
            ModeloLugar.ActualizarLugarRecogidaEntrega(Id, Lugar);
            return RedirectToAction("GestionarLugarRecogidaEntrega", "Administrador");
        }
        public IActionResult AccionAgregarClasificacion(int TipoVehiculo, string Clasificacion){
            ModeloClasificacionVehículo ClasificacionVehículo = new();
            ClasificacionVehículo.AgregarClasificacion(TipoVehiculo, Clasificacion);
            return RedirectToAction("GestionarClasificacionVehiculo", "Administrador");
        }
        public IActionResult AccionActualizarClasificacion(int Id, string Clasificacion){
            ModeloClasificacionVehículo ClasificacionVehículo = new();
            ClasificacionVehículo.ActualizarClasificacion(Id, Clasificacion);
            return RedirectToAction("GestionarClasificacionVehiculo", "Administrador");
        }
        public IActionResult AccionAgregarLinea(int Marca, string Linea){
            ModeloLinea ModeloLinea = new();
            ModeloLinea.AgregarLinea(Marca, Linea);
            return RedirectToAction("GestionarLineaVehiculo", "Administrador");
        }
        public IActionResult AccionActualizarLinea(int Id, string Linea){
            ModeloLinea ModeloLinea = new();
            ModeloLinea.ActualizarLinea(Id, Linea);
            return RedirectToAction("GestionarLineaVehiculo", "Administrador");
        }
    }
}