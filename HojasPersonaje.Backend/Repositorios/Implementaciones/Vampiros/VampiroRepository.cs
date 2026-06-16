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

        public async Task<Vampiro?> ObtenerPorIdFull(int id)
        {
            return await _context.Vampiros.Include(cb => cb.DebilidadesClanes)
                                          .Include(dv => dv.disciplinaVampiros)!
                                          .ThenInclude(d => d.disciplina)
                                          .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<bool> EliminarDisciplinas(Vampiro vampiro)
        {
            _context.DebilidadesClanes.RemoveRange(vampiro.DebilidadesClanes!);
            _context.DisciplinasVampiros.RemoveRange(vampiro.disciplinaVampiros!);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }


        public override async Task Eliminar(int id)
        {
            var entidad = await _context.Vampiros.Include(cb => cb.DebilidadesClanes!)
                                                 .Include(dv => dv.disciplinaVampiros!)
                                                 .FirstOrDefaultAsync(x => x.Id == id);

            if (entidad != null)
            {
                _context.DisciplinasVampiros.RemoveRange(entidad.disciplinaVampiros!);
                _context.DebilidadesClanes.RemoveRange(entidad.DebilidadesClanes!);
                _context.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
