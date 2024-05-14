using Microsoft.AspNetCore.Mvc;
using ALQUILER_VEHICULOS.Models;
namespace ALQUILER_VEHICULOS.Controllers
{
    public class InicioController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        public IActionResult Inicio()
        {
            ModeloInicio ModeloInicio = new();
            return View(ModeloInicio);
        }
        public IActionResult InicioFiltrado(int FiltroCiudad, int FiltroTipoVehiculo, int FiltroMarca, string FiltroFechaInicio, string FiltroHoraInicio, string FiltroFechaFin, string FiltroHoraFin)
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
            DateTime ValorFiltroFechaInicio = DateTime.Parse(FiltroFechaInicio);
            ValorFiltroFechaInicio = ValorFiltroFechaInicio.Add(TimeSpan.Parse(FiltroHoraInicio));
            DateTime ValorFiltroFechaFin = DateTime.Parse(FiltroFechaFin);
            ValorFiltroFechaFin = ValorFiltroFechaFin.Add(TimeSpan.Parse(FiltroHoraFin));
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
            object[] Datos =
            [
                FiltroCiudad,
                FiltroTipoVehiculo,
                FiltroMarca,
                FiltroFechaInicio,
                FiltroHoraInicio,
                FiltroFechaFin,
                FiltroHoraFin
            ];
            ViewBag.Message = Datos;
            return View(ModeloInicio);
        }
        public IActionResult SinPermisos()
        {
            ModeloInicio ModeloInicio = new();
            return View(ModeloInicio);
        }
    }
}