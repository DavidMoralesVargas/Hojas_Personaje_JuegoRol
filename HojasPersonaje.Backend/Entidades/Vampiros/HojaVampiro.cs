using HojasPersonaje.Backend.Entidades.Hojas;

namespace HojasPersonaje.Backend.Entidades.Vampiros
{
    public class HojaVampiro
    {
        public int Id { get; set; }
        public string? Sire {  get; set; }
        public string? Titulo { get; set; }
        

        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; }
        public TipoDepredador? TipoDepredador { get; set; }
        public int TipoDepredadorId { get; set; }
    }
}
