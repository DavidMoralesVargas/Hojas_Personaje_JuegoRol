using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Helpers;

namespace HojasPersonaje.Backend.Services.Interfaces.Vampiros
{
    public interface IHabilidadDisciplinaServices
    {
        Task<ActionResponse<List<HabilidadDisciplina>>> ObtenerTodos();
        Task<ActionResponse<HabilidadDisciplina>> ObtenerPorId(int id);
        Task<ActionResponse<List<HabilidadDisciplina>>> GuardarTodos(List<HabilidadDisciplina> entidades);
        Task<ActionResponse<HabilidadDisciplina>> Editar(HabilidadDisciplina entidad);
        Task<ActionResponse<bool>> Eliminar(int id);
        Task<ActionResponse<List<HabilidadDisciplina>>> ObtenerTodosPorId(int id);
        Task<ActionResponse<HabilidadDisciplina>> Guardar(HabilidadDisciplina entidad);
    }
}
