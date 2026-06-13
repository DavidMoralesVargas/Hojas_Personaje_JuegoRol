using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Vampiros
{
    public class DisciplinasRepository : Repository<Disciplina>, IDisciplinasRepository
    {
        public DisciplinasRepository(DatabaseContext context) : base(context) { }
    }
}
