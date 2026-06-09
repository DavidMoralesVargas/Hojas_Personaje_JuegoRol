namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public ICollection<HabilidadDisciplina>? habilidadDisciplinas { get; set; }
        public ICollection<DisciplinaVampiro>? disciplinaVampiros {  get; set; }
        public ICollection<DisciplinaJugador>? disciplinaJugadores { get; set; }
    }
}
