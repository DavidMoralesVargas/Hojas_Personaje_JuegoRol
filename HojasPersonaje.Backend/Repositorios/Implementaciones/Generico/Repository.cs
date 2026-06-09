using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Repositorios.Interfaces.Generico;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Generico
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> ObtenerTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> ObtenerPorId(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Guardar(T entidad)
        {
            await _dbSet.AddAsync(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }

        public async Task<T> Editar(T entidad)
        {
            _dbSet.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }

        public async Task Eliminar(int id)
        {
            var entidad = await _dbSet.FindAsync(id);

            if (entidad != null)
            {
                _dbSet.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
