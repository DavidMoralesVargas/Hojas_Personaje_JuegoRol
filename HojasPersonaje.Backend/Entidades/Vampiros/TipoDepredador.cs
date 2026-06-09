namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class TipoDepredador
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public ICollection<HojaVampiro>? HojasVampiro {  get; set; }
    }
}
