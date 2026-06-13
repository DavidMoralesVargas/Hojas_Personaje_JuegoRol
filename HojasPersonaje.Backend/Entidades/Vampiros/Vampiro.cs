namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class Vampiro
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public ICollection<ClanBane>? DebilidadesClanes {  get; set; }
        public ICollection<DisciplinaVampiro>? disciplinaVampiros { get; set; }
    }
}
