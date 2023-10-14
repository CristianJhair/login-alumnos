using Matricula.Models;
using Microsoft.AspNetCore.Mvc;

//Recursos y Servicios
using Matricula.Recursos;
using Matricula.Servicios.Contrato;

//Autenticación
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Numerics;

namespace Matricula.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        
        
        public AlumnosController( IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(TbAlumno alumno)
        {
            //Encriptar password
            alumno.Password = Utilidades.EncriptarClave(alumno.Password);


            TbAlumno usuario_creado = await _usuarioServicio.SaveUsuario(alumno);

            if (usuario_creado.NIdAlumno > 0)
                return RedirectToAction("IniciarSesion", "Alumnos");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();

        }

        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string username, string password)
        {
            //Encontrar alumno
            TbAlumno usuario_encontrado = await _usuarioServicio.GetUsuario(username, Utilidades.EncriptarClave(password));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreAlumno)
            };

            //Autenticación y cookies
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }

    }
}
