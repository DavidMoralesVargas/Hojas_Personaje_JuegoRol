using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Vampiros
{
    public class HabilidadDisciplinaRepository : Repository<HabilidadDisciplina>, IHabilidadDisciplinaRepository
    {
        private readonly DatabaseContext _context;

        public HabilidadDisciplinaRepository(DatabaseContext context) : base(context) 
        { 
            _context = context;
        }

        public async Task<List<HabilidadDisciplina>> ObtenerTodosPorId(int id)
        {
            return await _context.HabilidadesDisciplinas.Where(d => d.disciplinaId == id)
                                                        .ToListAsync();
        }

    }
}
