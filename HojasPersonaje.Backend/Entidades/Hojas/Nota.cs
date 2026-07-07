using HojasPersonaje.Backend.Entidades.Generico;

namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class Nota : HojaPersonajeID
    {
        public int Id { get; set; }
        public string? nota {  get; set; }
        
        public HojasDePersonaje? HojaPersonaje { get; set; }
        
    }
}
