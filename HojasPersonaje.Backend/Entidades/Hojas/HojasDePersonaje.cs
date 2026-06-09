using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Entidades.Vampiros;

namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class HojasDePersonaje
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Ambicion {  get; set; }
        public string? Concepto { get; set; }
        public string? Desire {  get; set; }
        

        public Usuario? usuario { get; set; }
        public int usuarioId { get; set; }
        public Cronica? Cronica { get; set; }
        public int CronicaId { get; set; }


        public ICollection<Nota>? Notas { get; set; }
        public ICollection<Posesion>? Posesiones { get; set; }
        public ICollection<AtributoHoja>? Atributos { get; set; } 
        public ICollection<Biografia>? Biografias { get; set; }
        public ICollection<Background>? Backgrounds { get; set; }
        public ICollection<Weapon>? Weapons { get; set; }
        public ICollection<Merito>? Meritos { get; set; } 
        public ICollection<Flaw>? Flaws { get; set; }
        public ICollection<Habilidad>? Habilidades { get; set; }
        public ICollection<ExperienciaHoja>? Experiencias { get; set; }
        public ICollection<ConviccionPiedra>? Convicciones { get; set; }
        public ICollection<HojaVampiro>? HojasVampiros { get; set; }
        public ICollection<DisciplinaJugador>? disciplinaJugadores { get; set; }
        public ICollection<HabilidadesJugador>? habilidadesJugadores { get; set; }
    }
}
