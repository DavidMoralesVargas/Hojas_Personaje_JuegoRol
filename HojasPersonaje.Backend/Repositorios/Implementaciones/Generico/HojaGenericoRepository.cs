using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Entidades.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Generico;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Generico
{
    public class HojaGenericoRepository<T> : IHojaGenericoRepository<T> where T : HojaPersonajeID
    {

        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public HojaGenericoRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> Editar(T entidad)
        {
            _dbSet.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }

        public async Task Eliminar(int idHoja)
        {
            var entidad = await _dbSet.FirstOrDefaultAsync(x => x.HojaPersonajeId == idHoja);
            if(entidad != null)
            {
                _dbSet.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> Guardar(T entidad)
        {
            _dbSet.Add(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }

        public async Task<T?> ObtenerPorId(int idHoja)
        {
            var entidad = await _dbSet.FirstOrDefaultAsync(x => x.HojaPersonajeId == idHoja);

            return entidad != null ? entidad : null;
        }
    }
}
