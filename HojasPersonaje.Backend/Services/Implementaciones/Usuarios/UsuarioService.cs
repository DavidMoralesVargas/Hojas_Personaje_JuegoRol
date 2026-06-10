using BCrypt.Net;
using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Helpers;
using HojasPersonaje.Backend.Repositorios.Interfaces.Generico;
using HojasPersonaje.Backend.Repositorios.Interfaces.Usuarios;
using HojasPersonaje.Backend.Services.Interfaces.Usuarios;

namespace HojasPersonaje.Backend.Services.Implementaciones.Usuarios
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuariosRepository _repository;

        public UsuarioService(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        //Método para editar usuario
        public async Task<ActionResponse<Usuario>> Editar(Usuario entidad)
        {
            try
            {
                return new ActionResponse<Usuario>
                {
                    Exitoso = true,
                    Resultado = await _repository.Editar(entidad)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Usuario>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        //Método para eliminar usuario
        public async Task<ActionResponse<bool>> Eliminar(int id)
        {
            try
            {
                await _repository.Eliminar(id);
                return new ActionResponse<bool>
                {
                    Resultado = true,
                    Exitoso = true
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<bool>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        //Verifica que el usuario existe para devolverlo, sino, lo crea
        public async Task<ActionResponse<Usuario>> Ingresar(UsuarioDTO entidad)
        {
            try
            {
                var usuario = await _repository.ObtenerPorNombre(entidad.NombreUsuario!);

                //Verificar que el usuario exista
                if(usuario == null)
                {
                    //Crea el usuario
                    var newUsuario = ConvertirAUsuario(entidad);

                    return new ActionResponse<Usuario>
                    {
                        Exitoso = true,
                        Resultado = await _repository.Guardar(newUsuario)
                    };
                }

                //Verifica el pin del usuario
                if (!VerificarPIN(entidad.Pin!, usuario!.Contrasena!))
                {
                    return new ActionResponse<Usuario>
                    {
                        Mensaje = "PIN Incorrecto",
                        Exitoso = false
                    };
                }

                //Pin correcto
                return new ActionResponse<Usuario>
                {
                    Exitoso = true,
                    Resultado = usuario
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Usuario>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        //Obtiene el usuario por id
        public async Task<ActionResponse<Usuario>> ObtenerPorId(int id)
        {
            try
            {
                return new ActionResponse<Usuario>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerPorId(id)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Usuario>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        //Obtiene toda la lista de usuarios
        public async Task<ActionResponse<List<Usuario>>> ObtenerTodos()
        {
            try
            {
                return new ActionResponse<List<Usuario>>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerTodos()
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<List<Usuario>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        //Crea un objeto Usuario desde un UsuarioDTO
        private Usuario ConvertirAUsuario(UsuarioDTO usuarioDTO)
        {
            //Encropta la contraseña/pin
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Pin);

            return new Usuario
            {
                Nombre_Usuario = usuarioDTO.NombreUsuario,
                Contrasena = passwordHash,
                Foto = string.Empty,
                tipoUsuario = usuarioDTO.tipoUsuario
            };
        }

        //Verifica que el pin ingresado sea el mismo que el guardado
        private bool VerificarPIN(string verificar, string pin)
        {
            //Método de librería para verificar pin
            return BCrypt.Net.BCrypt.Verify(verificar, pin);
        }
    }
}
