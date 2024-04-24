﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ALQUILER_VEHICULOS.Models;

namespace ALQUILER_VEHICULOS.Controllers
{
    public class AlquilerController : Controller
    {
        private ModeloUsuario DatosUsuarioSesion()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
            return JsonConvert.DeserializeObject<ModeloUsuario>(DatosUsuarioSesion);
        }
        [Authorize]
        public IActionResult CrearAlquiler(int Id_vehiculo)
        {
            ModeloCrearAlquiler ModeloCrearAlquiler = new(Id_vehiculo);
            return View(ModeloCrearAlquiler);
        }
        public IActionResult AccionCrearAlquiler(DateTime FechaInicio, DateTime FechaFin, float Precio, string SiLavada, int Vehiculo, int Lugar, int MetodoPago, int Seguro)
        {
            ModeloAlquiler ModeloAlquiler = new();
            int Lavada = SiLavada == "on"? 1 : 0;
            ModeloAlquilador ModeloAlquilador = new();
            ModeloAlquilador ModeloAlquiladorAux = null;
            if(ModeloAlquilador.ValidarAlquilador(DatosUsuarioSesion().Id)){
                ModeloAlquilador.CrearAlquilador(DatosUsuarioSesion().Id);
            }
            ModeloAlquiladorAux = ModeloAlquilador.TraerAlquilador(DatosUsuarioSesion().Id);
            int Alquilador = ModeloAlquiladorAux.Id;
            Console.WriteLine(FechaInicio + "\n" + FechaFin + "\n" + Precio + "\n" + Lavada + "\n" + Alquilador + "\n" + Vehiculo + "\n" + Lugar + "\n" + MetodoPago + "\n" + Seguro);
            //ModeloAlquiler.CrearAquiler(FechaInicio, FechaFin, Precio, Lavada, Alquilador, Vehiculo, Lugar, MetodoPago, Seguro);
            return RedirectToAction("Inicio", "Inicio");
        }
        [Authorize]
        public IActionResult InformacionAlquiler()
        {
            return View();
        }
        [Authorize]
        public IActionResult CalificarAlquiler()
        {
            return View();
        }
        [Authorize]
        public IActionResult HistorialAlquileres()
        {
            return View();
        }
        public IActionResult ObtenerPrecioAlquiler()
        {
            float PrecioAlquiler = 999;
            return Json(new { PrecioAlquiler });
        }
    }
}
