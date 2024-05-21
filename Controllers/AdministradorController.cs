using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace ALQUILER_VEHICULOS.Controllers
{
    [Authorize(Policy = "SoloAdministrador")]
    public class AdministradorController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        public IActionResult GestionarTodosUsuarios()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloUsuario Usuario = new();
            return View(Usuario.TraerUsuariosAdministrador(DatosUsuarioSesion().Id));
        }
        public IActionResult GestionarTodosVehiculos()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloVehiculo Vehiculos = new();
            return View(Vehiculos.TraerTodosVehiculosAdministrador());
        }
        public IActionResult GestionarTodosAlquileres()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquileres());
        }
        public IActionResult VerAlquilerAdministrador(int IdAlquiler)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloAlquiler ModeloAlquiler = new();
            return View(ModeloAlquiler.TraerAlquiler(IdAlquiler));
        }
        public IActionResult GestionarTipoVehiculo()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloTipoVehiculo TipoVehiculo = new();
            return View(TipoVehiculo.TraerTodosTipoVehiculo());
        }
        public IActionResult GestionarClasificacionVehiculo()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloTipoVehiculo TiModeloTipoVehiculo = new();
            return View(TiModeloTipoVehiculo.TraerTodosTipoVehiculo());
        }
        public IActionResult GestionarMarcaVehiculo()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloTipoVehiculo TipoVehiculo = new();
            return View(TipoVehiculo.TraerTodosTipoVehiculo());
        }
        public IActionResult GestionarLineaVehiculo()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloMarca Marca = new();
            return View(Marca.TraerTodosMetodasMarcas());
        }
        public IActionResult GestionarTipoCombustible()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloTipoCombustible TipoCombustible = new();
            return View(TipoCombustible.TraerTodosTiposComustible());
        }
        public IActionResult GestionarCiudad()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloDepartamento ModeloDepartamento = new();
            return View(ModeloDepartamento.TraerDepartamentos());
        }
        public IActionResult GestionarColores()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloColor Color = new();
            return View(Color.TraerColores());
        }
        public IActionResult GestionarCantidadPasajeros()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloCantidadPasajeros CantidadPasajeros = new();
            return View(CantidadPasajeros.TraerCantidadesPasajeros());
        }
        public IActionResult GestionarCilindradas()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloCilindrada Cilindrada = new();
            return View(Cilindrada.TraerCilindradas());
        }
        public IActionResult GestionarModelos()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloModelo Modelo = new();
            return View(Modelo.TraerModelos());
        }
        public IActionResult GestionarSeguroAlquiler()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloSeguroAlquiler SeguroAlquiler = new();
            return View(SeguroAlquiler.TraerTodosSegurosAlquiler());
        }
        public IActionResult GestionarMetodoPago()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloMetodoPagoAlquiler MetodoPago = new();
            return View(MetodoPago.TraerTodosMetodosPagoAlquiler());
        }
        public IActionResult GestionarLugarRecogidaEntrega()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloLugarAlquiler LugaresAlquiler = new();
            return View(LugaresAlquiler.TraerTodosLugaresAlquiler());
        }
        public IActionResult VerUsuarioAdministrador(int IdUsuario)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
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
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ModeloTipoIdentificacionUsuario TipoIdentificacion = new();
            return View(TipoIdentificacion.TraerTodosTiposdeIdentificacion());
        }
        public IActionResult GestionarEmpresa()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }

            ModeloGestionarEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.Empresa.RutaFoto;
            return View(Empresa);
        }
        public IActionResult GestionarRolesPermisos()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            return View(Empresa);
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
        public IActionResult AccionActualizarEmpresa(int Id, string Nombre, string NIT, string Direccion, int Ciudad, string Barrio, string Telefono, string Correo, IFormFile FotoEmpresa)
        {
            ModeloEmpresa Empresa = new();
            if (FotoEmpresa != null)
            {
                string NombreFoto = FotoEmpresa.FileName;
                var RutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", NombreFoto);
                using var fileStream = new FileStream(RutaArchivo, FileMode.Create);
                FotoEmpresa.CopyTo(fileStream);
                ModeloVehiculo ModeloVehiculo = new();
                Empresa.AccionActualizarEmpresa(Id, Nombre, NIT, Direccion, Ciudad, Barrio, Telefono, Correo, NombreFoto);
            }
            else
            {
                Empresa.AccionActualizarEmpresa(Id, Nombre, NIT, Direccion, Ciudad, Barrio, Telefono, Correo);
            }
            return RedirectToAction("GestionarEmpresa", "Administrador");
        }
    }
}