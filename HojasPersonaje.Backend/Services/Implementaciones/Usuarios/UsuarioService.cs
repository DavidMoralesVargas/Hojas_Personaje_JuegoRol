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
        public async Task<ActionResponse<Usuario>> Editar(UsuarioDTO entidad)
        {   
            try
            {
                //Primero busca al usuario antes de actualizarlo.
                var usuario = await _repository.ObtenerPorId(entidad.id);
                if(usuario == null)
                { //Si el usuario no existe, lanza un error
                    return new ActionResponse<Usuario>
                    {
                        Exitoso = false,
                        Mensaje = "No se encontró el usuario para editar"
                    };
                }
                //Actualiza los atributos para la edición
                usuario.Nombre_Usuario = entidad.NombreUsuario;
                usuario.Contrasena = entidad.Pin != "" && entidad.Pin != null ? BCrypt.Net.BCrypt.HashPassword(entidad.Pin) : usuario.Contrasena;


                return new ActionResponse<Usuario>
                {
                    Exitoso = true,
                    Resultado = await _repository.Editar(usuario) //Llama al repositorio para editar
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
        public async Task<ActionResponse<List<UsuarioListarDTO>>> ObtenerTodos()
        {
            try
            {
                var listaUsuario = await _repository.ObtenerTodos(); //Llamamos al repositorio para la lista de usuario

                //Transformamos la lista de usuarios en una DTO para enviar al cliente
                var nuevaLista = listaUsuario.Select(x => new UsuarioListarDTO
                {
                    Id = x.Id,
                    Nombre_Usuario = x.Nombre_Usuario,
                    Foto = x.Foto,
                    tipoUsuario = x.tipoUsuario
                }).ToList();

                return new ActionResponse<List<UsuarioListarDTO>>
                {
                    Exitoso = true,
                    Resultado = nuevaLista
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<List<UsuarioListarDTO>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        //Método que verifica que un usuario tenga un rol especificado
        public async Task<ActionResponse<bool>> VerificarUsuarioRol(string nombre, string rol)
        {
            try
            {
                var usuario = await _repository.ObtenerPorNombre(nombre);
                if(usuario == null)
                {
                    return new ActionResponse<bool>
                    {
                        Exitoso = false,
                        Mensaje = "El usuario con ese nombre no existe."
                    };
                }

                if (!usuario.tipoUsuario.ToString().Equals(rol))
                {
                    return new ActionResponse<bool>
                    {
                        Exitoso = false,
                        Mensaje = "El usuario no tiene ese rol."
                    };
                }

                return new ActionResponse<bool>
                {
                    Exitoso = true,
                    Resultado = true
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<bool>
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
