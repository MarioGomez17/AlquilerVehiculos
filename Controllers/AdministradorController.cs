using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace ALQUILER_VEHICULOS.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        public IActionResult GestionarTodosUsuarios()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarTodosUsuarios"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloUsuario Usuario = new();
                    return View(Usuario.TraerUsuariosAdministrador(DatosUsuarioSesion().Id));
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarTodosVehiculos()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarTodosVehiculos"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloVehiculo Vehiculos = new();
                    return View(Vehiculos.TraerTodosVehiculosAdministrador());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarTodosAlquileres()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarTodosAlquileres"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloAlquiler ModeloAlquiler = new();
                    return View(ModeloAlquiler.TraerAlquileres());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult VerAlquilerAdministrador(int IdAlquiler)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                ModeloEmpresa Empresa = new();
                ViewBag.RutaFoto = Empresa.RutaFoto;
                ViewBag.NombreEmpresa = Empresa.Nombre;
                ModeloAlquiler ModeloAlquiler = new();
                return View(ModeloAlquiler.TraerAlquiler(IdAlquiler));
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarTipoVehiculo()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarTipoVehiculo"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloTipoVehiculo TipoVehiculo = new();
                    return View(TipoVehiculo.TraerTodosTipoVehiculo());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarClasificacionVehiculo()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarClasificacionVehiculo"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloTipoVehiculo TiModeloTipoVehiculo = new();
                    return View(TiModeloTipoVehiculo.TraerTodosTipoVehiculo());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarMarcaVehiculo()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarMarcaVehiculo"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloTipoVehiculo TipoVehiculo = new();
                    return View(TipoVehiculo.TraerTodosTipoVehiculo());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarLineaVehiculo()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarLineaVehiculo"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloMarca Marca = new();
                    return View(Marca.TraerTodosMetodasMarcas());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarTipoCombustible()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarTipoCombustible"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloTipoCombustible TipoCombustible = new();
                    return View(TipoCombustible.TraerTodosTiposComustible());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarCiudad()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarCiudad"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloDepartamento ModeloDepartamento = new();
                    return View(ModeloDepartamento.TraerDepartamentos());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarColores()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarColores"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloColor Color = new();
                    return View(Color.TraerColores());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarCantidadPasajeros()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarCantidadPasajeros"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloCantidadPasajeros CantidadPasajeros = new();
                    return View(CantidadPasajeros.TraerCantidadesPasajeros());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarCilindradas()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarCilindradas"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloCilindrada Cilindrada = new();
                    return View(Cilindrada.TraerCilindradas());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarModelos()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarModelos"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloModelo Modelo = new();
                    return View(Modelo.TraerModelos());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarSeguroAlquiler()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarSeguroAlquiler"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloSeguroAlquiler SeguroAlquiler = new();
                    return View(SeguroAlquiler.TraerTodosSegurosAlquiler());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarMetodoPago()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarMetodoPago"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloMetodoPagoAlquiler MetodoPago = new();
                    return View(MetodoPago.TraerTodosMetodosPagoAlquiler());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarLugarRecogidaEntrega()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarLugarRecogidaEntrega"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloLugarAlquiler LugaresAlquiler = new();
                    return View(LugaresAlquiler.TraerTodosLugaresAlquiler());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult VerUsuarioAdministrador(int IdUsuario)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                ModeloEmpresa Empresa = new();
                ViewBag.RutaFoto = Empresa.RutaFoto;
                ViewBag.NombreEmpresa = Empresa.Nombre;
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
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarTipoIdentificacion()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarTipoIdentificacion"));
                if (TienePermiso)
                {
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    ModeloTipoIdentificacionUsuario TipoIdentificacion = new();
                    return View(TipoIdentificacion.TraerTodosTiposdeIdentificacion());
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarEmpresa()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarEmpresa"));
                if (TienePermiso)
                {
                    ModeloGestionarEmpresa GestorEmpresa = new();
                    ViewBag.RutaFoto = GestorEmpresa.Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = GestorEmpresa.Empresa.Nombre;
                    return View(GestorEmpresa);
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
        }
        public IActionResult GestionarRolesPermisos()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
                bool TienePermiso = DatosUsuarioSesion().Permisos.Any(Permiso => Permiso.Accion.Contains("GestionarRolesPermisos"));
                if (TienePermiso)
                {
                    ModeloGestionarPermisos RolesPermisos = new();
                    ModeloEmpresa Empresa = new();
                    ViewBag.RutaFoto = Empresa.RutaFoto;
                    ViewBag.NombreEmpresa = Empresa.Nombre;
                    return View(RolesPermisos);
                }
                else
                {
                    return RedirectToAction("SinPermisos", "Inicio");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Usuario");
            }
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
        public IActionResult TraerPermisosRol(int IdRol)
        {
            ModeloPermiso Permiso = new();
            return Json(new { Permisos = Permiso.TraerPermisosRol(IdRol) });
        }
        public IActionResult EliminarPermiso(int IdRol, int IdPermiso)
        {
            ModeloPermiso Permiso = new();
            Permiso.EliminarPermiso(IdRol, IdPermiso);
            return RedirectToAction("GestionarRolesPermisos", "Administrador");
        }
        public IActionResult AgregarPermiso(int IdRol, int IdPermiso)
        {
            ModeloPermiso Permiso = new();
            Permiso.AgregarPermiso(IdRol, IdPermiso);
            return RedirectToAction("GestionarRolesPermisos", "Administrador");
        }
        public IActionResult AgregarRol(string NuevoRol)
        {
            ModeloRol Rol = new();
            Rol.AgregarRol(NuevoRol);
            return RedirectToAction("GestionarRolesPermisos", "Administrador");
        }
    }
}