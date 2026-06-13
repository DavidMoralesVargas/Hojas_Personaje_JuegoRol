using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Helpers;

namespace HojasPersonaje.Backend.Services.Interfaces.Vampiros
{
    public interface IVampirosServices
    {
        Task<ActionResponse<List<Vampiro>>> ObtenerTodos();
        Task<ActionResponse<Vampiro>> ObtenerPorId(int id);
        Task<ActionResponse<Vampiro>> Guardar(VampiroDTO entidad);
        Task<ActionResponse<Vampiro>> Editar(VampiroDTO entidad);
        Task<ActionResponse<bool>> Eliminar(int id);
    }
}
