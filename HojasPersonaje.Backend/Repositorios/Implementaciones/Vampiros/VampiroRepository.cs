using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Vampiros
{
    public class VampiroRepository : Repository<Vampiro>, IVampiroRepository
    {
        private readonly DatabaseContext _context;

        public VampiroRepository(DatabaseContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task<List<Vampiro>> ObtenerTodos()
        {
            return await _context.Vampiros.Include(cb => cb.DebilidadesClanes)
                                          .Include(dv => dv.disciplinaVampiros)
                                          .ToListAsync();
        }
    }
}
