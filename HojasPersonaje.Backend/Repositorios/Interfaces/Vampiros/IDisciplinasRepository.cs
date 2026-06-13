

using HojasPersonaje.Backend.Entidades.Vampiros;

namespace HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros
{
    public interface IDisciplinasRepository
    {
        Task<List<Disciplina>> ObtenerTodos();
        Task<Disciplina?> ObtenerPorId(int id);
        Task<Disciplina> Guardar(Disciplina entidad);
        Task<Disciplina> Editar(Disciplina entidad);
        Task Eliminar(int id);
        Task<List<Disciplina>> Combo();
    }
}
