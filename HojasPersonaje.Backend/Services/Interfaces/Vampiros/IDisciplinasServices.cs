using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Helpers;

namespace HojasPersonaje.Backend.Services.Interfaces.Vampiros
{
    public interface IDisciplinasServices
    {
        Task<ActionResponse<List<Disciplina>>> ObtenerTodos();
        Task<ActionResponse<Disciplina>> ObtenerPorId(int id);
        Task<ActionResponse<Disciplina>> Guardar(DisciplinaDTO entidad);
        Task<ActionResponse<Disciplina>> Editar(Disciplina entidad);
        Task<ActionResponse<bool>> Eliminar(int id);
        Task<ActionResponse<List<Disciplina>>> Combo();
    }
}
