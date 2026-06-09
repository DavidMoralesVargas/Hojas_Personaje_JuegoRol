using HojasPersonaje.Backend.Entidades.Hojas;

namespace HojasPersonaje.Backend.Entidades.Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre_Usuario { get; set; }
        public string? Email { get; set; }
        public string? Contrasena { get; set; }
        public string? Foto { get; set; }
        public TipoUsuario tipoUsuario { get; set; }

        public ICollection<HojasDePersonaje>? hojasPersonajes { get; set; }
        public ICollection<Cronica>? cronicas { get; set; }
    }
}
