using HojasPersonaje.Backend.Entidades.Hojas;

namespace HojasPersonaje.Backend.Repositorios.Interfaces.Hojas
{
    public interface ICronicaRepository
    {
        Task<List<Cronica>> ObtenerTodos();
        Task<Cronica?> ObtenerPorId(int id);
        Task<Cronica> Guardar(Cronica entidad);
        Task<Cronica> Editar(Cronica entidad);
        Task Eliminar(int id);
        Task<Cronica?> ObtenerPorCodigo(string codigo);
        Task<bool> BuscarCualquierCodigo(string codigo);
        Task<List<Cronica>> ObtenerTodos(int id);
    }
}
