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

        public async Task<ActionResponse<Usuario>> Guardar(Usuario entidad)
        {
            try
            {
                return new ActionResponse<Usuario>
                {
                    Exitoso = true,
                    Resultado = await _repository.Guardar(entidad)
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
    }
}
