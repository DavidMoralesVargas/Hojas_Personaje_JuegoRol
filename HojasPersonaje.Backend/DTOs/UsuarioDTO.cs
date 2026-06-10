using HojasPersonaje.Backend.Entidades.Usuarios;

namespace HojasPersonaje.Backend.DTOs
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Pin {  get; set; }
        public TipoUsuario tipoUsuario { get; set;  }
    }
}
