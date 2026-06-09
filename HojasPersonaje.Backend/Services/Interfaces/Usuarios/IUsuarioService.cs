using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Helpers;

namespace HojasPersonaje.Backend.Services.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task<ActionResponse<List<Usuario>>> ObtenerTodos();
        Task<ActionResponse<Usuario>> ObtenerPorId(int id);
        Task<ActionResponse<Usuario>> Guardar(Usuario entidad);
        Task<ActionResponse<Usuario>> Editar(Usuario entidad);
        Task<ActionResponse<bool>> Eliminar(int id);
    }
}
