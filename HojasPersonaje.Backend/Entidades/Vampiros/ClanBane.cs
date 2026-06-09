namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class ClanBane
    {
        public int Id { get; set; }
        public string? Bane { get; set; }
        public string? Compulsion { get; set; }

        public Vampiro? vampiro { get; set; }
        public int vampiroId { get; set; }
    }
}
