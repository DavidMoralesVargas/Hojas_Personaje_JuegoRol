namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class ExperienciaHoja
    {
        public int Id { get; set; }
        public int ExperienciaTotal { get; set; }
        public int ExperienciaGastada { get; set; }
        
        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; }
    }
}
