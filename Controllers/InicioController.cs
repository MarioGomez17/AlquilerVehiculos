using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class InicioController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        public IActionResult Inicio()
        {
            ModeloInicio ModeloInicio = new();
            return View(ModeloInicio);
        }
        public IActionResult InicioFiltrado(int FiltroCiudad, int FiltroTipoVehiculo, int FiltroMarca, DateTime FiltroFechaInicio, DateTime FiltroFechaFin)
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
            ModeloAlquiler ModeloAlquiler = new();
            List<ModeloAlquiler> Alquileres = ModeloAlquiler.TraerAlquileres();
            ModeloInicio ModeloInicio = new(Vehiculos);
            foreach(var Alquiler in Alquileres){
                if((FiltroFechaInicio >= Alquiler.FechaIncio && FiltroFechaInicio <= Alquiler.FechaFin) || (FiltroFechaFin >= Alquiler.FechaIncio && FiltroFechaFin <= Alquiler.FechaFin)){
                    int IdVehiculoEliminar = Alquiler.Vehiculo.Id;
                    ModeloVehiculo VehiculoEliminar = ModeloInicio.Vehiculos.Find(ModeloVehiculo => ModeloVehiculo.Id == IdVehiculoEliminar);
                    ModeloInicio.Vehiculos.Remove(VehiculoEliminar);
                }
            }
            AlquilerController.FechaInicio = FiltroFechaInicio;
            AlquilerController.FechaFin = FiltroFechaFin;
            object[] Datos =
            [
                FiltroCiudad,
                FiltroTipoVehiculo,
                FiltroMarca,
                FiltroFechaInicio,
                FiltroFechaFin,
            ];
            ViewBag.Message = Datos;
            return View(ModeloInicio);
        }
    }
}
