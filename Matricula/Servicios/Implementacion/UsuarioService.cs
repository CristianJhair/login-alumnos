using Matricula.Models;
using Matricula.Servicios.Contrato;
using Microsoft.EntityFrameworkCore;

namespace Matricula.Servicios.Implementacion
{
    public class UsuarioService: IUsuarioService
    {
        private readonly MatriculaAdexContext _dbContext;
        public UsuarioService(MatriculaAdexContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Encontrar usuario
        public async Task<TbAlumno> GetUsuario(string username, string password)
        {
            TbAlumno usuario_encontrado = await _dbContext.TbAlumnos.Where(u => u.Username == username && u.Password == password)
                 .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        //Guardar usuario
        public async Task<TbAlumno> SaveUsuario(TbAlumno modelo)
        {
            _dbContext.TbAlumnos.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
