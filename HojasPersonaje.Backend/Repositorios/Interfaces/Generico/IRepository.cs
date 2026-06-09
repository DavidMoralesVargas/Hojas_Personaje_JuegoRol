namespace HojasPersonaje.Backend.Repositorios.Interfaces.Generico
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> ObtenerTodos();
        Task<T?> ObtenerPorId(int id);
        Task<T> Guardar(T entidad);
        Task<T> Editar(T entidad);
        Task Eliminar(int id);
    }
}
