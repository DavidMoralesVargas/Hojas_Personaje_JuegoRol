namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class Posesion
    {
        public int Id { get; set; }
        public string? posesion {  get; set; }

        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; }
    }
}
