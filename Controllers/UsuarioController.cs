using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;
using Microsoft.AspNetCore.Authorization;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class UsuarioController : Controller
    {
        //---------------------------------------------- VISTAS ----------------------------------------------
        public IActionResult IniciarSesion()
        {
            ClaimsPrincipal ClaimsPrincipal = HttpContext.User;
            if (ClaimsPrincipal != null)
            {
                if (ClaimsPrincipal.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Inicio", "Inicio");
                }
            }
            return View();
        }
        public IActionResult Registrarse()
        {
            ModeloTipoIdentificacionUsuario TipoIdentificacionUsuario = new();
            return View(TipoIdentificacionUsuario.TraerTodosTiposdeIdentificacion());
        }
        [Authorize]
        public IActionResult InformacionUsuario()
        {
            ModeloTipoIdentificacionUsuario TipoIdentificacion = new();
            List<ModeloTipoIdentificacionUsuario> TiposIdentificacion = TipoIdentificacion.TraerTodosTiposdeIdentificacion();
            ModeloUsuario ModeloUsuario = new();
            ModeloEditarUsuario ModeloEditarUsuario = new()
            {
                Usuario = ModeloUsuario.TraerUsuario(DatosUsuarioSesion().Id),
                TiposIdentificacion = TiposIdentificacion
            };
            return View(ModeloEditarUsuario);
        }
        //---------------------------------------------- ACCIONES ----------------------------------------------
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        [HttpPost]
        public async Task<IActionResult> AccionIniciarSesion(string Correo, string Contrasena, string MantenerSesion)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario = ModeloUsuario.TraerUsuario(Correo, Contrasena);
            if (ModeloUsuario != null)
            {
                if (ModeloUsuario.Estado == "Activo")
                {
                    var Claims = new List<Claim> {
                        new (ClaimTypes.Name, ModeloUsuario.Nombre),
                        new ("Correo", ModeloUsuario.Correo)
                    };
                    ClaimsIdentity ClaimsIdentity = new(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    Claim DatosUsuario = new(ClaimTypes.UserData, JsonConvert.SerializeObject(ModeloUsuario));
                    ClaimsIdentity.AddClaim(DatosUsuario);
                    AuthenticationProperties AuthenticationProperties = new()
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(MantenerSesion == "on" ? 60 : 5)
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdentity), AuthenticationProperties);
                    return RedirectToAction("Inicio", "Inicio");
                }
                else
                {
                    ViewBag.Message = "Correo o Contraseña Equivocados";
                    return View("IniciarSesion");
                }
            }
            else
            {
                ViewBag.Message = "Correo o Contraseña Incorrectos";
                return View("IniciarSesion");
            }
        }
        public IActionResult AccionRegistrarse(string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, string Contrasena)
        {
            ModeloUsuario ModeloUsuario = new();
            if (ModeloUsuario.ValidarUsuario(NumeroIdentificacion))
            {
                if (ModeloUsuario.RegistrarUsuario(Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo, Contrasena))
                {
                    return View("IniciarSesion");
                }
                else
                {
                    ViewBag.Message = "Complete los campos del formulario correctamente";
                    Registrarse();
                    return View("Registrarse");
                }
            }
            else
            {
                ViewBag.Message = "El usuario ya existe";
                Registrarse();
                return View("Registrarse");
            }
        }
        [Authorize]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("IniciarSesion");
        }
        [Authorize]
        public IActionResult AccionActualizarUsuario(string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, string Contrasena)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario = ModeloUsuario.TraerUsuario(DatosUsuarioSesion().Id);
            int IdUsuario = ModeloUsuario.Id;
            if (Contrasena == null)
            {
                ModeloUsuario.ActualizarUsuario(IdUsuario, Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo);
            }
            else
            {
                ModeloUsuario.ActualizarUsuario(IdUsuario, Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo, Contrasena);
            }
            InformacionUsuario();
            return View("InformacionUsuario");
        }
        public async Task<IActionResult> AccionEliminarUsuario(int IdUsuario){
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario.EliminarUsuario(IdUsuario);
            await CerrarSesion();
            return View("IniciarSesion");
        }
    }
}
