using HojasPersonaje.Backend.Entidades.Usuarios;

namespace HojasPersonaje.Backend.DTOs
{
    public class UsuarioListarDTO
    {
        public int Id { get; set; }
        public string? Nombre_Usuario { get; set; }
        public string? Foto { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
    }
}
