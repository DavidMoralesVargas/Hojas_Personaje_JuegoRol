namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class DisciplinaVampiro
    {
        public int Id { get; set; }
        
        public Vampiro? vampiro { get; set; }
        public int vampiroId { get; set; }

        public Disciplina? disciplina { get; set; }
        public int disciplinaId { get; set; }
    }
}
