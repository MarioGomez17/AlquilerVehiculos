using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;
using Microsoft.AspNetCore.Authorization;

namespace VEHICLE_RENTAL.Controllers
{
    public class UsuarioController : Controller
    {
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var UserSessionData = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(UserSessionData);
        }
        public IActionResult IniciarSesion()
        {
            ClaimsPrincipal ClaimsPrincipal = HttpContext.User;
            if (ClaimsPrincipal != null)
            {
                if (ClaimsPrincipal.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
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
            return View();
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
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(MantenerSesion == "on" ? 30 : 5)
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
            ModeloUsuario UserModel = new();
            if (UserModel.ValidarUsuario(NumeroIdentificacion))
            {
                if (UserModel.RegistrarUsuario(Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo, Contrasena))
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
    }
}
