namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class ConviccionPiedra
    {
        public int Id { get; set; }
        public string? PiedraToken { get; set; }
        public string? Convicciones { get; set; }

        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; }
    }
}
