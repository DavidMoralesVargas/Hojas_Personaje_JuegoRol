using HojasPersonaje.Backend.Entidades.Usuarios;

namespace HojasPersonaje.Backend.Entidades.Hojas
{
    public class Cronica
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? NombreCronica { get; set; }
        public string? PaisCronica { get; set; }
        public bool Finalizado { get; set; }
        public string? Codigo { get; set; }
        
        public Usuario? DungeonMaster { get; set; }
        public int DungeonMasterId { get; set; }

        public ICollection<PrincipioCronica>? PrincipiosCronicas { get; set; }
    }
}
