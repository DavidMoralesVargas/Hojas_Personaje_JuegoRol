using HojasPersonaje.Backend.Entidades.Vampiros;

namespace HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros
{
    public interface IHabilidadDisciplinaRepository
    {
        Task<List<HabilidadDisciplina>> ObtenerTodos();
        Task<HabilidadDisciplina?> ObtenerPorId(int id);
        Task<HabilidadDisciplina> Guardar(HabilidadDisciplina entidad);
        Task<HabilidadDisciplina> Editar(HabilidadDisciplina entidad);
        Task Eliminar(int id);
        Task<List<HabilidadDisciplina>> ObtenerTodosPorId(int id);
    }
}
