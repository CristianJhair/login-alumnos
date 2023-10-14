using Matricula.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Matricula.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MatriculaAdexContext _context;
        public HomeController(ILogger<HomeController> logger, MatriculaAdexContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string nombreUsuario = "";

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }
            ViewData["nombreUsuario"] = nombreUsuario;

            return View();
        }

        /*public IActionResult CursosDisponibles()
        {
            var cursosDisponibles = _context.TbCursos;
            ClaimsPrincipal claimuser = HttpContext.User;

            //Verificar autenticación
            if (claimuser.Identity.IsAuthenticated)
            {
                // Obtener el idAlumno del alumno autenticado
                var idAlumnoClaim = claimuser.FindFirst(ClaimTypes.NameIdentifier);
                if (idAlumnoClaim != null && int.TryParse(idAlumnoClaim.Value, out int idAlumno))
                
                {
                    // Consulta para obtener el ciclo actual del alumno
                    var cicloActual = _context.TbAlumnos
                        .Where(a => a.NIdAlumno == idAlumno)
                        .Select(a => a.NIdCiclo)
                        .FirstOrDefault();

                    if (cicloActual != 0)
                    {
                        // Consulta para obtener los cursos disponibles en el ciclo actual
                        cursosDisponibles
                            .Where(c => c.NIdCiclo == cicloActual)
                            .Where(c => !_context.TbMatriculas.Any(m => m.NIdCurso == c.NIdCurso && m.NIdAlumno == idAlumno))
                            .ToList();

                        return View(cursosDisponibles);
                    }
                }
            }
            return View(cursosDisponibles);
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("IniciarSesion", "Alumnos");
        }
    }
}