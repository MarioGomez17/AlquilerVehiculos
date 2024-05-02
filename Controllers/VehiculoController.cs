using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class VehiculoController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
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
        public IActionResult InformacionVehiculo(int IdVehiculo)
        {
            ModeloVerVehiculo ModeloVerVehiculo = new(IdVehiculo);
            return View(ModeloVerVehiculo);
        }
        public IActionResult InformacionVehiculoCrearAlquiler(int IdVehiculo)
        {
            ModeloVehiculo ModeloVehiculo = new();
            return View(ModeloVehiculo.TraerVehiculo(IdVehiculo));
        }
        //---------------------------------------------- ACCIONES ----------------------------------------------
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        public IActionResult AccionActualizarVehiculo(int Id, string Placa, int Cilindrada, int Modelo, float PrecioAlquilerDia, string Color, int CantidadPasajeros, int ClasificacionVehiculo, int Linea, string NumeroSeguro, string NumeroCertificadoCDA, int TipoCombustible, int Ciudad)
        {
            ModeloVehiculo ModeloVehiculo = new ();
            ModeloVehiculo.ActualizarVehiculo(Id, Placa, Cilindrada, Modelo, PrecioAlquilerDia, Color, CantidadPasajeros, ClasificacionVehiculo, Linea, NumeroSeguro, NumeroCertificadoCDA, TipoCombustible, Ciudad);
            InformacionVehiculo(Id);
            return View("InformacionVehiculo");
        }

        public IActionResult AccionEliminarVehiculo(int IdVehiculo)
        {
            ModeloVehiculo ModeloVehiculo = new ();
            ModeloVehiculo.EliminarVehiculo(IdVehiculo);
            InformacionVehiculos();
            return View("InformacionVehiculos");
        }

        public IActionResult TraerTodasClasificacionesPorTipo(int IdTipoVehiculo)
        {
            ModeloClasificacionVehículo ModeloClasificacionVehículo = new();
            return Json(new { ClasificacionesVehiculo = ModeloClasificacionVehículo.TraerTodasClasificacionesPorTipo(IdTipoVehiculo) });
        }
        public IActionResult TraerTodasLineasPorMarca(int IdMarca)
        {
            ModeloLinea ModeloLinea = new();
            return Json(new { Lineas =  ModeloLinea.TraerTodasLineasPorMarca(IdMarca)});
        }
        public IActionResult TraerTodosMetodasMarcasPorTIpo(int IdTipoVehiculo)
        {
            ModeloMarca ModeloMarca = new();
            return Json(new { Marcas = ModeloMarca.TraerTodosMetodasMarcasPorTIpo(IdTipoVehiculo) });
        }
    }
}

