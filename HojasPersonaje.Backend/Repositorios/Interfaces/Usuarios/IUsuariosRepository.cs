using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Helpers;

namespace HojasPersonaje.Backend.Repositorios.Interfaces.Usuarios
{
    public interface IUsuariosRepository
    {
        Task<List<Usuario>> ObtenerTodos();
        Task<Usuario?> ObtenerPorId(int id);
        Task<Usuario> Guardar(Usuario entidad);
        Task<Usuario> Editar(Usuario entidad);
        Task Eliminar(int id);
        Task<Usuario> ObtenerPorNombre(string nombre);
    }
}
