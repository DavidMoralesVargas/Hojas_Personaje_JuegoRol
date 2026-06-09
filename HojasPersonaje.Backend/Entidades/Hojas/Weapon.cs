namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class Weapon
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Damage { get; set; }
        
        public HojasDePersonaje? HojaPersonaje { get; set; }
        public int HojaPersonajeId { get; set; }
    }
}
