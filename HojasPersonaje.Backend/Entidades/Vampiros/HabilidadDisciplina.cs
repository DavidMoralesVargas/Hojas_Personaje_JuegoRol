namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class HabilidadDisciplina
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Nivel { get; set; }
        public bool Enardecimiento { get; set; }
        public string? Tirada { get; set; }

        public Disciplina? disciplina { get; set; }
        public int disciplinaId { get; set; }

        public ICollection<HabilidadesJugador>? habilidadesJugadores { get; set; }
    }
}
