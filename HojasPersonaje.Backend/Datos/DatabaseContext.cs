using HojasPersonaje.Backend.Entidades.Hojas;
using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Entidades.Vampiros;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Backend.Datos
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


        //Hojas de personaje
        public DbSet<AtributoHoja> AtributosHojas { get; set; }
        public DbSet<Background> Backgrounds { get; set; }
        public DbSet<Biografia> Biografias { get; set; }
        public DbSet<ConviccionPiedra> ConviccionesPiedras { get; set; }
        public DbSet<Cronica> Cronicas { get; set; }
        public DbSet<EspecialidadHabilidad> EspecialidadesHabilidades { get; set; }
        public DbSet<ExperienciaHoja> ExperienciasHojas { get; set; }
        public DbSet<Flaw> Flaws { get; set; }
        public DbSet<Habilidad> Habilidades { get; set; }
        public DbSet<HojasDePersonaje> HojasPersonajes { get; set; }
        public DbSet<Merito> Meritos { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Posesion> Posesiones { get; set; }
        public DbSet<Weapon> Weapons { get; set; }

        //Usuarios
        public DbSet<Usuario> Usuarios { get; set; }

        //Vampiros
        public DbSet<ClanBane> DebilidadesClanes { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<DisciplinaJugador> DisciplinasJugadores { get; set; }
        public DbSet<DisciplinaVampiro> DisciplinasVampiros { get; set; }
        public DbSet<HabilidadDisciplina> HabilidadesDisciplinas { get; set; }
        public DbSet<HabilidadesJugador> HabilidadesJugadores { get; set; }
        public DbSet<HojaVampiro> HojasVampiros { get; set; }
        public DbSet<TipoDepredador> TiposDepredador { get; set; }
        public DbSet<Vampiro> Vampiros { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
