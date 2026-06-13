using HojasPersonaje.Backend.Datos;
using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Repositorios.Implementaciones.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Backend.Repositorios.Implementaciones.Usuarios
{
    public class UsuariosRepository : Repository<Usuario>, IUsuariosRepository 
    {

        private readonly DatabaseContext _context;

        public UsuariosRepository(DatabaseContext context) : base(context) 
        { 
            _context = context;
        }

        public async Task<Usuario> ObtenerPorNombre(string nombre)
        {
            return (await _context.Usuarios.FirstOrDefaultAsync(n => n.Nombre_Usuario == nombre))!;
        }

    }
}
