using Matricula.Models;

namespace Matricula.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<TbAlumno> GetUsuario(string username, string password);
        Task<TbAlumno> SaveUsuario(TbAlumno modelo);
    }
}
