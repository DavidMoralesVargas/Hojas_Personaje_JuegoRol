using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Entidades.Hojas;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Hojas;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Hojas
{
    public class CronicaRepository : Repository<Cronica>, ICronicaRepository
    {
        private readonly DatabaseContext _context;
        public CronicaRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> BuscarCualquierCodigo(string codigo)
        {
            return await _context.Cronicas.AnyAsync(c => c.Codigo == codigo);
        }

        public async Task<Cronica?> ObtenerPorCodigo(string codigo)
        {
            return await _context.Cronicas.Include(x => x.PrincipiosCronicas).FirstOrDefaultAsync(x => x.Codigo == codigo);
        }

        public async Task<List<Cronica>> ObtenerTodos(int id)
        {
            return await _context.Cronicas.Include(x => x.PrincipiosCronicas).Where(x => x.DungeonMasterId == id).ToListAsync();
        }
    }
}
