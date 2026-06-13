using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Helpers;

namespace HojasPersonaje.Backend.Services.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task<ActionResponse<List<Usuario>>> ObtenerTodos();
        Task<ActionResponse<Usuario>> ObtenerPorId(int id);
        Task<ActionResponse<Usuario>> Ingresar(UsuarioDTO entidad);
        Task<ActionResponse<Usuario>> Editar(Usuario entidad);
        Task<ActionResponse<bool>> Eliminar(int id);
        Task<ActionResponse<bool>> VerificarUsuarioRol(string nombre, string rol); 
    }
}
