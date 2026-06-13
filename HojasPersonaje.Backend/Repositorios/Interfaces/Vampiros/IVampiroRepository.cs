using HojasPersonaje.Backend.Entidades.Vampiros;

namespace HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros
{
    public interface IVampiroRepository
    {
        Task<List<Vampiro>> ObtenerTodos();
        Task<Vampiro?> ObtenerPorId(int id);
        Task<Vampiro> Guardar(Vampiro entidad);
        Task<Vampiro> Editar(Vampiro entidad);
        Task Eliminar(int id);
    }
}
