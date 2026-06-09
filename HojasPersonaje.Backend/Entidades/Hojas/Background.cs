namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class Background
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Nivel { get; set; }
        
        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; }
    }
}
