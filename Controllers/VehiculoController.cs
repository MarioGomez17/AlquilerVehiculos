using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace ALQUILER_VEHICULOS.Controllers
{
    public class VehiculoController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        [Authorize]
        public IActionResult RegistrarVehiculo(string[] Mensaje)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ViewBag.Mensaje = Mensaje;
            ModeloRegistrarVehiculo ModeloRegistrarVehiculo = new();
            return View(ModeloRegistrarVehiculo);
        }
        [Authorize]
        public IActionResult InformacionVehiculos()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ModeloVehiculo ModeloVehiculo = new();
            List<ModeloVehiculo> Vehiculos = ModeloVehiculo.TraerTodosVehiculosPropietario(DatosUsuarioSesion().Id);
            return View(Vehiculos);
        }
        [Authorize]
        public IActionResult InformacionVehiculo(int IdVehiculo)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ModeloVerVehiculo ModeloVerVehiculo = new(IdVehiculo);
            return View(ModeloVerVehiculo);
        }
        public IActionResult InformacionVehiculoCrearAlquiler(int IdVehiculo)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ModeloVehiculo ModeloVehiculo = new();
            return View(ModeloVehiculo.TraerVehiculo(IdVehiculo));
        }
        //---------------------------------------------- ACCIONES ----------------------------------------------
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
        public IActionResult AccionActualizarVehiculo(int IdVehiculo, string Placa, int Cilindrada, int Modelo, float PrecioAlquilerDia, int Color, int CantidadPasajeros, int ClasificacionVehiculo, int Linea, string NumeroSeguro, string NumeroCertificadoCDA, int TipoCombustible, int Ciudad, IFormFile FotoVehiculo)
        {
            if (FotoVehiculo != null)
            {
                string NombreFoto = FotoVehiculo.FileName;
                ModeloPropietario ModeloPropietario = new();
                if (ModeloPropietario.ValidarPropietario(DatosUsuarioSesion().Id))
                {
                    ModeloPropietario.CrearPropietario(DatosUsuarioSesion().Id);
                }
                ModeloPropietario = ModeloPropietario.TraerPropietarioUsuario(DatosUsuarioSesion().Id);
                string RutaFoto = ModeloPropietario.Codigo + "_" + NombreFoto;
                var RutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FotoVehiculos", RutaFoto);
                using var fileStream = new FileStream(RutaArchivo, FileMode.Create);
                FotoVehiculo.CopyTo(fileStream);
                ModeloVehiculo ModeloVehiculo = new();
                ModeloVehiculo.ActualizarVehiculoConFoto(IdVehiculo, Placa, Cilindrada, Modelo, PrecioAlquilerDia, Color, CantidadPasajeros, ClasificacionVehiculo, Linea, NumeroSeguro, NumeroCertificadoCDA, TipoCombustible, Ciudad, RutaFoto);
            }
            else
            {
                ModeloVehiculo ModeloVehiculo = new();
                ModeloVehiculo.ActualizarVehiculo(IdVehiculo, Placa, Cilindrada, Modelo, PrecioAlquilerDia, Color, CantidadPasajeros, ClasificacionVehiculo, Linea, NumeroSeguro, NumeroCertificadoCDA, TipoCombustible, Ciudad);
            }
            return RedirectToAction("InformacionVehiculo", "Vehiculo", new { IdVehiculo });
        }
        public IActionResult AccionEliminarVehiculo(int IdVehiculo)
        {
            ModeloVehiculo ModeloVehiculo = new();
            ModeloVehiculo.EliminarVehiculo(IdVehiculo);
            return RedirectToAction("InformacionVehiculo", "Vehiculo", new { IdVehiculo });
        }
        public IActionResult AccionActivarVehiculo(int IdVehiculo)
        {
            ModeloVehiculo ModeloVehiculo = new();
            ModeloVehiculo.ActivarVehiculo(IdVehiculo);
            return RedirectToAction("InformacionVehiculo", "Vehiculo", new { IdVehiculo });
        }
        public IActionResult TraerTodasClasificacionesPorTipo(int IdTipoVehiculo)
        {
            ModeloClasificacionVehículo ModeloClasificacionVehículo = new();
            return Json(new { ClasificacionesVehiculo = ModeloClasificacionVehículo.TraerTodasClasificacionesPorTipo(IdTipoVehiculo) });
        }
        public IActionResult TraerTodasLineasPorMarca(int IdMarca)
        {
            ModeloLinea ModeloLinea = new();
            return Json(new { Lineas = ModeloLinea.TraerTodasLineasPorMarca(IdMarca) });
        }
        public IActionResult TraerTodasMarcasPorTIpo(int IdTipoVehiculo)
        {
            ModeloMarca ModeloMarca = new();
            return Json(new { Marcas = ModeloMarca.TraerTodasMarcasPorTIpo(IdTipoVehiculo) });
        }
        public IActionResult TraerTodosTIpo()
        {
            ModeloTipoVehiculo TipoVehiculo = new();
            return Json(new { TiposVehiculo = TipoVehiculo.TraerTodosTipoVehiculo() });
        }
        public IActionResult AccionRegistrarVehiculo(string Placa, int Cilindrada, int Modelo, float PrecioAlquilerDia, int Color, int CantidadPasajeros, int ClasificacionVehiculo, int Linea, string NumeroCertificadoCDA, string NumeroSeguro, int TipoCombustible, int Ciudad, IFormFile FotoVehiculo)
        {
            if (FotoVehiculo != null)
            {
                ModeloVehiculo ModeloVehiculo = new();
                if (ModeloVehiculo.ValidarVehiculo(Placa))
                {
                    string NombreFoto = FotoVehiculo.FileName;
                    ModeloPropietario ModeloPropietario = new();
                    if (ModeloPropietario.ValidarPropietario(DatosUsuarioSesion().Id))
                    {
                        ModeloPropietario.CrearPropietario(DatosUsuarioSesion().Id);
                    }
                    ModeloPropietario = ModeloPropietario.TraerPropietarioUsuario(DatosUsuarioSesion().Id);
                    int Propietario = ModeloPropietario.Id;
                    string RutaFoto = ModeloPropietario.Codigo + "_" + NombreFoto;
                    if (ModeloVehiculo.RegistrarVehiculo(Placa, Cilindrada, Modelo, PrecioAlquilerDia, Color, CantidadPasajeros, ClasificacionVehiculo, Linea, NumeroCertificadoCDA, NumeroSeguro, TipoCombustible, Ciudad, RutaFoto, Propietario))
                    {
                        var RutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FotoVehiculos", RutaFoto);
                        using var fileStream = new FileStream(RutaArchivo, FileMode.Create);
                        FotoVehiculo.CopyTo(fileStream);
                        return RedirectToAction("InformacionVehiculos", "Vehiculo");
                    }
                    else
                    {
                        object[] Mensaje = [
                            "Error al registrar vehículo. Por favor Intente de nuevo",
                            Placa,
                            Cilindrada,
                            Modelo,
                            PrecioAlquilerDia,
                            Color,
                            CantidadPasajeros,
                            NumeroCertificadoCDA,
                            NumeroSeguro,
                            TipoCombustible,
                            Ciudad
                        ];
                        return RedirectToAction("RegistrarVehiculo", "Vehiculo", new { Mensaje });
                    }
                }
                else
                {
                    object[] Mensaje = [
                            "La placa del vehículo ya está registrada",
                            Placa,
                            Cilindrada,
                            Modelo,
                            PrecioAlquilerDia,
                            Color,
                            CantidadPasajeros,
                            NumeroCertificadoCDA,
                            NumeroSeguro,
                            TipoCombustible,
                            Ciudad
                        ];
                    return RedirectToAction("RegistrarVehiculo", "Vehiculo", new { Mensaje });
                }
            }
            else
            {
                object[] Mensaje = [
                            "Error al cargar archivo",
                            Placa,
                            Cilindrada,
                            Modelo,
                            PrecioAlquilerDia,
                            Color,
                            CantidadPasajeros,
                            NumeroCertificadoCDA,
                            NumeroSeguro,
                            TipoCombustible,
                            Ciudad
                        ];
                return RedirectToAction("RegistrarVehiculo", "Vehiculo", new { Mensaje });
            }
        }
        private async Task ActualizarDatosUsuarioSesion()
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
