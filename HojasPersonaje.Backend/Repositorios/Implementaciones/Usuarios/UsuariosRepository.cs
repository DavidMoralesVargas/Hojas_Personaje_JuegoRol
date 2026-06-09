using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Usuarios;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Usuarios
{
    public class UsuariosRepository : Repository<Usuario>, IUsuariosRepository 
    {
        public UsuariosRepository(DatabaseContext context) : base(context) { }


    }
}
