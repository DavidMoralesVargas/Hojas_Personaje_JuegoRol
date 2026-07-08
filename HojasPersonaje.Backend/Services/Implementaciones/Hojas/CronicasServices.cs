using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Hojas;
using HojasPersonaje.Backend.Helpers;
using HojasPersonaje.Backend.Repositorios.Interfaces.Hojas;
using HojasPersonaje.Backend.Repositorios.Interfaces.Usuarios;
using HojasPersonaje.Backend.Services.Interfaces.Hojas;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace HojasPersonaje.Backend.Services.Implementaciones.Hojas
{
    public class CronicasServices : ICronicasServices
    {
        private readonly ICronicaRepository _cronicaRepository;
        private readonly IUsuariosRepository _userRepository;

        public CronicasServices(ICronicaRepository cronicaRepository, IUsuariosRepository userRepository)
        {
            _cronicaRepository = cronicaRepository;
            _userRepository = userRepository;
        }

        public async Task<ActionResponse<Cronica>> Editar(CronicaDTO entidad)
        {
            try
            {
                var cronica = await _cronicaRepository.ObtenerPorId(entidad.Id);
                if(cronica == null)
                {
                    return new ActionResponse<Cronica>
                    {
                        Exitoso = false
                    };
                }

                cronica.NombreCronica = entidad.NombreCronica;
                cronica.PaisCronica = entidad.PaisCronica;

                return new ActionResponse<Cronica>
                {
                    Exitoso = true,
                    Resultado = await _cronicaRepository.Editar(cronica)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Cronica>
                {
                    Mensaje = ex.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<ActionResponse<bool>> Eliminar(int id)
        {
            try
            {
                await _cronicaRepository.Eliminar(id);

                return new ActionResponse<bool>
                {
                    Exitoso = true,
                    Resultado = true
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<bool>
                {
                    Mensaje = ex.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<ActionResponse<Cronica>> Guardar(CronicaDTO entidad, string nombreDM)
        {
            try
            {
                var usuario = await _userRepository.ObtenerPorNombre(nombreDM);
                if (usuario == null)
                {
                    return new ActionResponse<Cronica>
                    {
                        Exitoso = false,
                        Mensaje = "No se encontro el usuario con ese nombre"
                    };
                }

                var cronicaNew = new Cronica()
                {
                    FechaCreacion = DateTime.UtcNow,
                    Finalizado = false,
                    DungeonMasterId = usuario.Id,
                    PaisCronica = entidad.PaisCronica,
                    NombreCronica = entidad.NombreCronica,
                    Codigo = await GenerarCodigo(),
                    PrincipiosCronicas = new List<PrincipioCronica>{ new PrincipioCronica { PrincipiosCronica = entidad.PrincipiosCronica } }
                };

                return new ActionResponse<Cronica>
                {
                    Exitoso = true,
                    Resultado = await _cronicaRepository.Guardar(cronicaNew)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Cronica>
                {
                    Mensaje = ex.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<ActionResponse<Cronica>> ObtenerPorCodigo(string codigo)
        {
            try
            {
                return new ActionResponse<Cronica>
                {
                    Exitoso = true,
                    Resultado = await _cronicaRepository.ObtenerPorCodigo(codigo)
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<Cronica>
                {
                    Mensaje = ex.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<ActionResponse<Cronica>> ObtenerPorId(int id)
        {
            try
            {
                return new ActionResponse<Cronica>
                {
                    Exitoso = true,
                    Resultado = await _cronicaRepository.ObtenerPorId(id)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Cronica>
                {
                    Mensaje = ex.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<ActionResponse<List<Cronica>>> ObtenerTodos()
        {
            try
            {
                return new ActionResponse<List<Cronica>>
                {
                    Exitoso = true,
                    Resultado = await _cronicaRepository.ObtenerTodos()
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<List<Cronica>>
                {
                    Mensaje = ex.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<ActionResponse<List<Cronica>>> ObtenerTodosPorId(string nombre)
        {
            try
            {
                var usuario = await _userRepository.ObtenerPorNombre(nombre);

                if(usuario == null)
                {
                    return new ActionResponse<List<Cronica>>
                    {
                        Exitoso = false,
                        Mensaje = "No se encontró el usuario con ese nombre"
                    };
                }

                return new ActionResponse<List<Cronica>>
                {
                    Exitoso = true,
                    Resultado = await _cronicaRepository.ObtenerTodos(usuario.Id)
                };

            }
            catch(Exception ex)
            {
                return new ActionResponse<List<Cronica>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        private async Task<string> GenerarCodigo()
        {
            const string Caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random random = new();

            while (true)
            {
                var codigo = new string(
                    Enumerable.Range(0, 6)
                        .Select(_ => Caracteres[random.Next(Caracteres.Length)])
                        .ToArray()
                );

                bool existe = await _cronicaRepository.BuscarCualquierCodigo(codigo);

                if (!existe)
                {
                    return codigo;
                }
            }
        }
    }
}
