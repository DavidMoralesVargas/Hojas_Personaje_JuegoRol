using HojasPersonaje.Backend.Entidades.Vampiros;

namespace HojasPersonaje.Backend.DTOs
{
    public class DisciplinaDTO
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public List<HabilidadDisciplina>? habilidades { get; set; }
    }
}
