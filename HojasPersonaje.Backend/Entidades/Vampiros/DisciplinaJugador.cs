using HojasPersonaje.Backend.Entidades.Hojas;

namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class DisciplinaJugador
    {
        public int Id { get; set; }

        public Disciplina? disciplina { get; set; }
        public int disciplinaId { get; set; }

        public HojasDePersonaje? hojasDePersonaje { get; set; }
        public int hojasDePersonajeId { get; private set; }
    }
}
