using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Vampiros
{
    public class DisciplinasRepository : Repository<Disciplina>, IDisciplinasRepository
    {
        private readonly DatabaseContext _context;
        public DisciplinasRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Disciplina>> Combo()
        {
            return await _context.Disciplinas.ToListAsync();
        }

        public override async Task<List<Disciplina>> ObtenerTodos()
        {
            return await _context.Disciplinas.Include(x => x.habilidadDisciplinas).ToListAsync();
        }
    }
}
