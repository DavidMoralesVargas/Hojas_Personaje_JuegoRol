namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class Nota
    {
        public int Id { get; set; }
        public string? nota {  get; set; }
        
        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; }
    }
}
