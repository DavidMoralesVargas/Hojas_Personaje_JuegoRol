using HojasPersonaje.Backend.Entidades.Vampiros;

namespace HojasPersonaje.Backend.DTOs
{
    public class VampiroDTO
    {
        public int id {  get; set; }
        public string? Nombre { get; set; }
        public ClanBane? clanBane { get; set; }
        public List<Disciplina>? Disciplinas { get; set; }
    }
}
