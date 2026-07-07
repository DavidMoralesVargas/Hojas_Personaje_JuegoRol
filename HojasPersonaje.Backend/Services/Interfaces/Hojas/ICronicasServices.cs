using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Hojas;
using HojasPersonaje.Backend.Helpers;

namespace HojasPersonaje.Backend.Services.Interfaces.Hojas
{
    public interface ICronicasServices
    {
        Task<ActionResponse<List<Cronica>>> ObtenerTodos();
        Task<ActionResponse<Cronica>> ObtenerPorId(int id);
        Task<ActionResponse<Cronica>> Guardar(CronicaDTO entidad, string nombreDM);
        Task<ActionResponse<Cronica>> Editar(CronicaDTO entidad);
        Task<ActionResponse<bool>> Eliminar(int id);
        Task<ActionResponse<Cronica>> ObtenerPorCodigo(string codigo);
        Task<ActionResponse<List<Cronica>>> ObtenerTodosPorId(string nombre);
    }
}
