namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class Biografia
    {
        public int Id { get; set; }
        public int EdadReal { get; set; }
        public int EdadAparente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaMuerte { get; set; }
        public string? Apariencia { get; set; }
        public string? RastosDistintivos { get; set; }
        public string? Historia { get; set; }
        public string? Resumen {  get; set; }

        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; } 
    }
}
