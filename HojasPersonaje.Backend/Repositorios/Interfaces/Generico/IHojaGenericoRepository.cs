namespace HojasPersonaje.Backend.Repositorios.Interfaces.Generico
{
    public interface IHojaGenericoRepository<T> where T : class
    {
        Task<T?> ObtenerPorId(int idHoja);
        Task<T> Guardar(T entidad);
        Task<T> Editar(T entidad);
        Task Eliminar(int idHoja);
    }
}
