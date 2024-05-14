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
        //---------------------------------------------- VISTAS ----------------------------------------------
        public IActionResult GestionarTodosUsuarios()
        {
            ModeloUsuario Usuario = new();
            return View(Usuario.TraerUsuariosAdministrador(DatosUsuarioSesion().Id));
        }
        public IActionResult GestionarTodosVehiculos()
        {
            ModeloVehiculo Vehiculos = new();
            return View(Vehiculos.TraerTodosVehiculosAdministrador());
        }
        public IActionResult GestionarTodosAlquileres()
        {
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquileres());
        }
        public IActionResult GestionarTipoVehiculo()
        {
            ModeloTipoVehiculo TipoVehiculo = new();
            return View(TipoVehiculo.TraerTodosTipoVehiculo());
        }
        public IActionResult GestionarClasificacionVehiculo()
        {
            ModeloTipoVehiculo TiModeloTipoVehiculo = new();
            return View(TiModeloTipoVehiculo.TraerTodosTipoVehiculo());
        }
        public IActionResult GestionarMarcaVehiculo()
        {
            ModeloTipoVehiculo TipoVehiculo = new();
            return View(TipoVehiculo.TraerTodosTipoVehiculo());
        }
        public IActionResult GestionarLineaVehiculo()
        {
            ModeloMarca Marca = new();
            return View(Marca.TraerTodosMetodasMarcas());
        }
        public IActionResult GestionarTipoCombustible()
        {
            ModeloTipoCombustible TipoCombustible = new();
            return View(TipoCombustible.TraerTodosTiposComustible());
        }
        public IActionResult GestionarCiudad()
        {
            ModeloDepartamento ModeloDepartamento = new();
            return View(ModeloDepartamento.TraerDepartamentos());
        }
        public IActionResult GestionarColores()
        {
            ModeloColor Color = new();
            return View(Color.TraerColores());
        }
        public IActionResult GestionarCantidadPasajeros()
        {
            ModeloCantidadPasajeros CantidadPasajeros = new();
            return View(CantidadPasajeros.TraerCantidadesPasajeros());
        }
        public IActionResult GestionarCilindradas()
        {
            ModeloCilindrada Cilindrada = new();
            return View(Cilindrada.TraerCilindradas());
        }
        public IActionResult GestionarModelos()
        {
            ModeloModelo Modelo = new();
            return View(Modelo.TraerModelos());
        }
        public IActionResult GestionarSeguroAlquiler()
        {
            ModeloSeguroAlquiler SeguroAlquiler = new();
            return View(SeguroAlquiler.TraerTodosSegurosAlquiler());
        }
        public IActionResult GestionarMetodoPago()
        {
            ModeloMetodoPagoAlquiler MetodoPago = new();
            return View(MetodoPago.TraerTodosMetodosPagoAlquiler());
        }
        public IActionResult GestionarLugarRecogidaEntrega()
        {
            ModeloLugarAlquiler LugaresAlquiler = new();
            return View(LugaresAlquiler.TraerTodosLugaresAlquiler());
        }
        public IActionResult VerUsuarioAdministrador(int IdUsuario)
        {
            ModeloTipoIdentificacionUsuario TipoIdentificacion = new();
            ModeloUsuario ModeloUsuario = new();
            ModeloRol Roles = new();
            ModeloEditarUsuario ModeloEditarUsuario = new()
            {
                Usuario = ModeloUsuario.TraerUsuario(IdUsuario),
                TiposIdentificacion = TipoIdentificacion.TraerTodosTiposdeIdentificacion(),
                Roles = Roles.TraerRoles()
            };
            return View(ModeloEditarUsuario);
        }
        public IActionResult GestionarTipoIdentificacion()
        {
            ModeloTipoIdentificacionUsuario TipoIdentificacion = new();
            return View(TipoIdentificacion.TraerTodosTiposdeIdentificacion());
        }
        //---------------------------------------------- ACCIONES ----------------------------------------------
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        public IActionResult AccionAgregarCiudad(int Departamento, string Ciudad)
        {
            ModeloCiudad ModeloCiudad = new();
            ModeloCiudad.AgregarCiudad(Departamento, Ciudad);
            return RedirectToAction("GestionarCiudad", "Administrador");
        }
        public IActionResult AccionActualizarCiudad(int Id, string Ciudad)
        {
            ModeloCiudad ModeloCiudad = new();
            ModeloCiudad.ActualizarCiudad(Id, Ciudad);
            return RedirectToAction("GestionarCiudad", "Administrador");
        }
        public IActionResult AccionAgregarLugar(string Lugar)
        {
            ModeloLugarAlquiler ModeloLugar = new();
            ModeloLugar.AgregarLugarRecogidaEntrega(Lugar);
            return RedirectToAction("GestionarLugarRecogidaEntrega", "Administrador");
        }
        public IActionResult AccionActualizarLugar(int Id, string Lugar)
        {
            ModeloLugarAlquiler ModeloLugar = new();
            ModeloLugar.ActualizarLugarRecogidaEntrega(Id, Lugar);
            return RedirectToAction("GestionarLugarRecogidaEntrega", "Administrador");
        }
        public IActionResult AccionAgregarClasificacion(int TipoVehiculo, string Clasificacion)
        {
            ModeloClasificacionVehículo ClasificacionVehículo = new();
            ClasificacionVehículo.AgregarClasificacion(TipoVehiculo, Clasificacion);
            return RedirectToAction("GestionarClasificacionVehiculo", "Administrador");
        }
        public IActionResult AccionActualizarClasificacion(int Id, string Clasificacion)
        {
            ModeloClasificacionVehículo ClasificacionVehículo = new();
            ClasificacionVehículo.ActualizarClasificacion(Id, Clasificacion);
            return RedirectToAction("GestionarClasificacionVehiculo", "Administrador");
        }
        public IActionResult AccionAgregarLinea(int Marca, string Linea)
        {
            ModeloLinea ModeloLinea = new();
            ModeloLinea.AgregarLinea(Marca, Linea);
            return RedirectToAction("GestionarLineaVehiculo", "Administrador");
        }
        public IActionResult AccionActualizarLinea(int Id, string Linea)
        {
            ModeloLinea ModeloLinea = new();
            ModeloLinea.ActualizarLinea(Id, Linea);
            return RedirectToAction("GestionarLineaVehiculo", "Administrador");
        }
        public IActionResult AccionAgregarMarca(int Tipo, string Marca)
        {
            ModeloMarca ModeloMarca = new();
            ModeloMarca.AgregarMarca(Tipo, Marca);
            return RedirectToAction("GestionarMarcaVehiculo", "Administrador");
        }
        public IActionResult AccionActualizarMarca(int Id, string Marca)
        {
            ModeloMarca ModeloMarca = new();
            ModeloMarca.ActualizarMarca(Id, Marca);
            return RedirectToAction("GestionarMarcaVehiculo", "Administrador");
        }
        public IActionResult AccionAgregarCantidadPasajeros(int CantidadPasajeros)
        {
            ModeloCantidadPasajeros ModeloCantidadPasajeros = new();
            ModeloCantidadPasajeros.AgregarCantidadPasajeros(CantidadPasajeros);
            return RedirectToAction("GestionarCantidadPasajeros", "Administrador");
        }
        public IActionResult AccionActualizarCantidadPasajeros(int Id, int CantidadPasajeros)
        {
            ModeloCantidadPasajeros ModeloCantidadPasajeros = new();
            ModeloCantidadPasajeros.ActualizarCantidadPasajeros(Id, CantidadPasajeros);
            return RedirectToAction("GestionarCantidadPasajeros", "Administrador");
        }
        public IActionResult AccionAgregarCilindrada(int Cilindrada)
        {
            ModeloCilindrada ModeloCilindrada = new();
            ModeloCilindrada.AgregarCilindrada(Cilindrada);
            return RedirectToAction("GestionarCilindradas", "Administrador");
        }
        public IActionResult AccionActualizarCilindrada(int Id, int Cilindrada)
        {
            ModeloCilindrada ModeloCilindrada = new();
            ModeloCilindrada.ActualizarCilindrada(Id, Cilindrada);
            return RedirectToAction("GestionarCilindradas", "Administrador");
        }
        public IActionResult AccionAgregarColor(string Color)
        {
            ModeloColor ModeloColor = new();
            ModeloColor.AgregarColor(Color);
            return RedirectToAction("GestionarColores", "Administrador");
        }
        public IActionResult AccionActualizarColor(int Id, string Color)
        {
            ModeloColor ModeloColor = new();
            ModeloColor.ActualizarColor(Id, Color);
            return RedirectToAction("GestionarColores", "Administrador");
        }
        public IActionResult AccionAgregarModelo(int Modelo)
        {
            ModeloModelo ModeloModelo = new();
            ModeloModelo.AgregarModelo(Modelo);
            return RedirectToAction("GestionarModelos", "Administrador");
        }
        public IActionResult AccionActualizarModelo(int Id, int Modelo)
        {
            ModeloModelo ModeloModelo = new();
            ModeloModelo.ActualizarModelo(Id, Modelo);
            return RedirectToAction("GestionarModelos", "Administrador");
        }
        public IActionResult AccionAgregarSeguroAlquiler(string NombreSeguro, float PrecioSeguro)
        {
            ModeloSeguroAlquiler Seguro = new();
            Seguro.AgregarSeguroAlquiler(NombreSeguro, PrecioSeguro);
            return RedirectToAction("GestionarSeguroAlquiler", "Administrador");
        }
        public IActionResult AccionActualizarSeguroAlquiler(int Id, string NombreSeguro, float PrecioSeguro)
        {
            ModeloSeguroAlquiler Seguro = new();
            Seguro.ActualizarSeguroAlquiler(Id, NombreSeguro, PrecioSeguro);
            return RedirectToAction("GestionarSeguroAlquiler", "Administrador");
        }
        public IActionResult AccionAgregarMetodoPago(string MetodoPago)
        {
            ModeloMetodoPagoAlquiler MetodoPagoAlquiler = new();
            MetodoPagoAlquiler.AgregarMetodoPago(MetodoPago);
            return RedirectToAction("GestionarMetodoPago", "Administrador");
        }
        public IActionResult AccionActualizarMetodoPago(int Id, string MetodoPago)
        {
            ModeloMetodoPagoAlquiler MetodoPagoAlquiler = new();
            MetodoPagoAlquiler.ActualizarMetodoPago(Id, MetodoPago);
            return RedirectToAction("GestionarMetodoPago", "Administrador");
        }
        public IActionResult AccionAgregarTipoVehiculo(string TipoVehiculo)
        {
            ModeloTipoVehiculo ModeloTipoVehiculo = new();
            ModeloTipoVehiculo.AgregarTipoVehiculo(TipoVehiculo);
            return RedirectToAction("GestionarTipoVehiculo", "Administrador");
        }
        public IActionResult AccionActualizarTipoVehiculo(int Id, string TipoVehiculo)
        {
            ModeloTipoVehiculo ModeloTipoVehiculo = new();
            ModeloTipoVehiculo.ActualizarTipoVehiculo(Id, TipoVehiculo);
            return RedirectToAction("GestionarTipoVehiculo", "Administrador");
        }
        public IActionResult AccionAgregarTipoCombustible(string Combustible)
        {
            ModeloTipoCombustible TipoCombustible = new();
            TipoCombustible.AgregarTipoCombustible(Combustible);
            return RedirectToAction("GestionarTipoCombustible", "Administrador");
        }
        public IActionResult AccionActualizarTipoCombustible(int Id, string Combustible)
        {
            ModeloTipoCombustible TipoCombustible = new();
            TipoCombustible.ActualizarTipoCombustible(Id, Combustible);
            return RedirectToAction("GestionarTipoCombustible", "Administrador");
        }
        public IActionResult AccionAgregarTipoIdentificacion(string TipoIdentificacion, string Simbolo)
        {
            ModeloTipoIdentificacionUsuario TIpoIdentificacion = new();
            TIpoIdentificacion.AgregarTipoIdentificacion(TipoIdentificacion, Simbolo);
            return RedirectToAction("GestionarTipoIdentificacion", "Administrador");
        }
        public IActionResult AccionActualizarTipoIdentificacion(int Id, string TipoIdentificacion, string Simbolo)
        {
            ModeloTipoIdentificacionUsuario TIpoIdentificacion = new();
            TIpoIdentificacion.ActualizarTipoIdentificacion(Id, TipoIdentificacion, Simbolo);
            return RedirectToAction("GestionarTipoIdentificacion", "Administrador");
        }
        public IActionResult AccionActualizarUsuarioAdministrador(int IdUsuario, string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, string Contrasena, int Rol)
        {
            ModeloUsuario ModeloUsuario = new();
            if (Contrasena == null)
            {
                ModeloUsuario.AccionActualizarUsuarioAdministrador(IdUsuario, Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo, Rol);
            }
            else
            {
                ModeloUsuario.AccionActualizarUsuarioAdministrador(IdUsuario, Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo, Contrasena, Rol);
            }
            return RedirectToAction("VerUsuarioAdministrador", "Administrador", new { IdUsuario });
        }
    }
}