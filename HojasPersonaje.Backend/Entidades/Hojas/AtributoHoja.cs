namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class AtributoHoja
    {
        public int Id { get; set; }
        public int Fuerza { get; set; }
        public int Destreza { get; set; }
        public int Resistencia { get; set; }
        public int Carisma { get; set; }
        public int Manipulacion {  get; set; }
        public int Compostura { get; set; }
        public int Inteligencia { get; set; }
        public int Astucia { get; set; }
        public int Resolucion { get; set; }

        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; }
    }
}
