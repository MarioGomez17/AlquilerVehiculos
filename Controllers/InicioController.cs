using Microsoft.AspNetCore.Mvc;
using ALQUILER_VEHICULOS.Models;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace ALQUILER_VEHICULOS.Controllers
{
    public class InicioController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        public IActionResult Inicio()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloInicio ModeloInicio = new();
            return View(ModeloInicio);
        }
        public IActionResult InicioFiltrado(int FiltroCiudad, int FiltroTipoVehiculo, int FiltroMarca, string FiltroFechaInicio, string FiltroFechaFin)
        {
            ModeloVehiculo ModeloVehiculo = new();
            List<ModeloVehiculo> Vehiculos = [];
            if (FiltroCiudad != 0 && FiltroTipoVehiculo != 0 && FiltroMarca != 0)
            {
                Vehiculos = ModeloVehiculo.TraerTodosVehiculosTodosFiltros(FiltroCiudad, FiltroTipoVehiculo, FiltroMarca);
            }
            else if (FiltroCiudad != 0 && FiltroTipoVehiculo == 0 && FiltroMarca == 0)
            {
                Vehiculos = ModeloVehiculo.TraerTodosVehiculosFiltroCiudad(FiltroCiudad);
            }
            else if (FiltroCiudad == 0 && FiltroTipoVehiculo != 0 && FiltroMarca == 0)
            {
                Vehiculos = ModeloVehiculo.TraerTodosVehiculosFiltroTipo(FiltroTipoVehiculo);
            }
            else if (FiltroCiudad == 0 && FiltroTipoVehiculo == 0 && FiltroMarca != 0)
            {
                Vehiculos = ModeloVehiculo.TraerTodosVehiculosFiltroMarca(FiltroMarca);
            }
            else if (FiltroCiudad != 0 && FiltroTipoVehiculo != 0 && FiltroMarca == 0)
            {
                Vehiculos = ModeloVehiculo.TraerTodosVehiculosFiltroCiudadTipo(FiltroCiudad, FiltroTipoVehiculo);
            }
            else if (FiltroCiudad != 0 && FiltroTipoVehiculo == 0 && FiltroMarca != 0)
            {
                Vehiculos = ModeloVehiculo.TraerTodosVehiculosFiltroCiudadMarca(FiltroCiudad, FiltroMarca);
            }
            else if (FiltroCiudad == 0 && FiltroTipoVehiculo != 0 && FiltroMarca != 0)
            {
                Vehiculos = ModeloVehiculo.TraerTodosVehiculosFiltroTipoMarca(FiltroTipoVehiculo, FiltroMarca);
            }
            else if (FiltroCiudad == 0 && FiltroTipoVehiculo == 0 && FiltroMarca == 0)
            {
                Vehiculos = ModeloVehiculo.TraerTodosVehiculos();
            }
            ModeloInicio ModeloInicio = new(Vehiculos);
            ModeloAlquiler ModeloAlquiler = new();
            List<ModeloAlquiler> Alquileres = ModeloAlquiler.TraerAlquileres();
            DateOnly ValorFiltroFechaInicio = DateOnly.Parse(FiltroFechaInicio);
            DateOnly ValorFiltroFechaFin = DateOnly.Parse(FiltroFechaFin);
            foreach (var Alquiler in Alquileres)
            {
                if ((ValorFiltroFechaInicio >= Alquiler.FechaIncio && ValorFiltroFechaInicio <= Alquiler.FechaFin) || (ValorFiltroFechaFin >= Alquiler.FechaIncio && ValorFiltroFechaFin <= Alquiler.FechaFin))
                {
                    int IdVehiculoEliminar = Alquiler.Vehiculo.Id;
                    ModeloVehiculo VehiculoEliminar = ModeloInicio.Vehiculos.Find(ModeloVehiculo => ModeloVehiculo.Id == IdVehiculoEliminar);
                    ModeloInicio.Vehiculos.Remove(VehiculoEliminar);
                }
            }
            AlquilerController.FechaInicio = ValorFiltroFechaInicio;
            AlquilerController.FechaFin = ValorFiltroFechaFin;
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ViewBag.FiltroCiudad = FiltroCiudad;
            ViewBag.FiltroTipoVehiculo = FiltroTipoVehiculo;
            ViewBag.FiltroMarca = FiltroMarca;
            ViewBag.FiltroFechaInicio = FiltroFechaInicio;
            ViewBag.FiltroFechaFin = FiltroFechaFin;
            return View(ModeloInicio);
        }
        public IActionResult SinPermisos()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloInicio ModeloInicio = new();
            return View(ModeloInicio);
        }
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            if (Identity.FindFirst(ClaimTypes.UserData) != null)
            {
                var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
                return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
            }
            else
            {
                return null;
            }
        }
        private async Task ActualizarDatosUsuarioSesion()
        {
            if (DatosUsuarioSesion() != null)
            {
                int IdUsuario = DatosUsuarioSesion().Id;
                ModeloUsuario ModeloUsuario = new();
                var ClaimPrincipal = (ClaimsIdentity)User.Identity;
                var ClaimUsuarioActual = ClaimPrincipal.FindFirst(ClaimTypes.UserData);
                if (ClaimUsuarioActual != null)
                {
                    ClaimPrincipal.RemoveClaim(ClaimUsuarioActual);
                }
                Claim nuevoClaimUsuario = new(ClaimTypes.UserData, JsonConvert.SerializeObject(ModeloUsuario.TraerUsuario(IdUsuario)));
                ClaimPrincipal.AddClaim(nuevoClaimUsuario);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimPrincipal));
            }
        }
    }
}