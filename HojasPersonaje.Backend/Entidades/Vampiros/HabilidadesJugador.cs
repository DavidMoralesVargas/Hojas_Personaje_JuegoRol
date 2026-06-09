using HojasPersonaje.Backend.Entidades.Hojas;

namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class HabilidadesJugador
    {
        public int Id { get; set; }
        
        public HojasDePersonaje? hojasDePersonaje { get; set; }
        public int hojasDePersonajeId { get; set; }

        public HabilidadDisciplina? habilidadDisciplina { get; set; }
        public int habilidadDisciplinaId { get; set; }
    }
}
