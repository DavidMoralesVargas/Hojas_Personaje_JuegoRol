namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class EspecialidadHabilidad
    {
        public int Id { get; set; }
        public string? Habilidad { get; set; }
        public string? Especialidad { get; set; }
        
        public Habilidad? habilidad { get; set; }
        public int habilidadId { get; set; }
    }
}
