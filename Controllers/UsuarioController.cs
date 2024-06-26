﻿using Microsoft.AspNetCore.Authentication.Cookies;
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
        public IActionResult IniciarSesion(string[] Mensaje, string returnUrl = null)
        {
            ClaimsPrincipal ClaimsPrincipal = HttpContext.User;
            if (ClaimsPrincipal != null)
            {
                if (ClaimsPrincipal.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Inicio", "Inicio");
                }
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ViewBag.Mensaje = Mensaje;
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        public IActionResult Registrarse(string[] Mensaje)
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
            ModeloTipoIdentificacionUsuario TipoIdentificacionUsuario = new();
            ViewBag.Mensaje = Mensaje;
            return View(TipoIdentificacionUsuario.TraerTodosTiposdeIdentificacion());
        }
        [Authorize]
        public IActionResult InformacionUsuario()
        {
            if (DatosUsuarioSesion() != null)
            {
                _ = ActualizarDatosUsuarioSesion();
                ViewBag.AlquileresPendientes = DatosUsuarioSesion().AlquileresPendientes;
            }
            ModeloEmpresa Empresa = new();
            ViewBag.RutaFoto = Empresa.RutaFoto;
            ViewBag.NombreEmpresa = Empresa.Nombre;
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
        public async Task<IActionResult> AccionIniciarSesion(string Correo, string Contrasena, string MantenerSesion, string ReturnULR)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario = ModeloUsuario.TraerUsuario(Correo, Contrasena);
            if (ModeloUsuario != null)
            {
                if (ModeloUsuario.Estado == "Activo")
                {
                    var Claims = new List<Claim> {
                        new (ClaimTypes.Name, ModeloUsuario.Nombre),
                        new ("Correo", ModeloUsuario.Correo),
                        new (ClaimTypes.Role, ModeloUsuario.Rol)
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
                    if (Url.IsLocalUrl(ReturnULR))
                    {
                        return Redirect(ReturnULR);
                    }
                    else
                    {
                        return RedirectToAction("Inicio", "Inicio");
                    }
                }
                else
                {
                    object[] Mensaje =
                    [
                        "Correo o Contraseña Equivocados",
                        Correo
                    ];
                    return RedirectToAction("IniciarSesion", "Usuario", new { Mensaje });
                }
            }
            else
            {
                object[] Mensaje =
                    [
                        "Correo o Contraseña Equivocados",
                        Correo
                    ];
                return RedirectToAction("IniciarSesion", "Usuario", new { Mensaje });
            }
        }
        public IActionResult AccionRegistrarse(string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, string Contrasena)
        {
            ModeloUsuario ModeloUsuario = new();
            if (ModeloUsuario.ValidarUsuario(NumeroIdentificacion))
            {
                if (ModeloUsuario.RegistrarUsuario(Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo, Contrasena))
                {
                    return RedirectToAction("IniciarSesion", "Usuario");
                }
                else
                {
                    object[] Mensaje =
                    [
                        "Complete los campos del formulario correctamente",
                        Nombre,
                        Apellido,
                        TipoIdentificacion,
                        NumeroIdentificacion,
                        Telefono,
                        Correo
                    ];
                    return RedirectToAction("Registrarse", "Usuario", new { Mensaje });
                }
            }
            else
            {
                object[] Mensaje =
                    [
                        "El usuario con ese número de identificación ya existe",
                        Nombre,
                        Apellido,
                        TipoIdentificacion,
                        NumeroIdentificacion,
                        Telefono,
                        Correo
                    ];
                return RedirectToAction("Registrarse", "Usuario", new { Mensaje });
            }
        }
        [Authorize]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Usuario");
        }
        public async Task<IActionResult> AccionActualizarUsuarioAsync(int Id, string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, string Contrasena)
        {
            ModeloUsuario ModeloUsuario = new();
            if (Contrasena == null)
            {
                ModeloUsuario.ActualizarUsuario(Id, Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo);
            }
            else
            {
                ModeloUsuario.ActualizarUsuario(Id, Nombre, Apellido, TipoIdentificacion, NumeroIdentificacion, Telefono, Correo, Contrasena);
            }
            var ClaimPrincipal = (ClaimsIdentity)User.Identity;
            var ClaimUsuarioActual = ClaimPrincipal.FindFirst(ClaimTypes.UserData);
            if (ClaimUsuarioActual != null)
            {
                ClaimPrincipal.RemoveClaim(ClaimUsuarioActual);
            }
            Claim nuevoClaimUsuario = new(ClaimTypes.UserData, JsonConvert.SerializeObject(ModeloUsuario.TraerUsuario(Id)));
            ClaimPrincipal.AddClaim(nuevoClaimUsuario);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimPrincipal));
            return RedirectToAction("InformacionUsuario", "Usuario");
        }
        public async Task<IActionResult> AccionEliminarUsuario(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario.EliminarUsuario(IdUsuario);
            await CerrarSesion();
            return RedirectToAction("IniciarSesion", "Usuario");
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