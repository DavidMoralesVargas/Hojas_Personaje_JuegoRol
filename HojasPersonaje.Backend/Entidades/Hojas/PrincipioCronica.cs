namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class PrincipioCronica
    {
        public int Id { get; set; }
        public string? PrincipiosCronica { get; set; }
       
        public Cronica? Cronica { get; set; }
        public int CronicaId { get; set; }
    }
}
