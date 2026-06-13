using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Helpers;
using HojasPersonaje.Backend.Patrones;
using HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros;
using HojasPersonaje.Backend.Services.Interfaces.Vampiros;

namespace HojasPersonaje.Backend.Services.Implementaciones.Vampiros
{
    public class VampirosServices : IVampirosServices
    {
        private readonly IVampiroRepository _repository;

        public VampirosServices(IVampiroRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResponse<Vampiro>> Editar(VampiroDTO entidad)
        {
            try
            {
                return new ActionResponse<Vampiro>
                {
                    Exitoso = true
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Vampiro>
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
                return new ActionResponse<bool>
                {
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

        public async Task<ActionResponse<Vampiro>> Guardar(VampiroDTO entidad)
        {
            try
            {
                var vampiro = new VampiroBuilder().ConNombre(entidad.Nombre!)
                                                  .ConClanBane(entidad.clanBane!)
                                                  .ConDisciplinas(entidad.Disciplinas!)
                                                  .Build();
                return new ActionResponse<Vampiro>
                {
                    Exitoso = true,
                    Resultado = await _repository.Guardar(vampiro)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Vampiro>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<Vampiro>> ObtenerPorId(int id)
        {
            try
            {
                return new ActionResponse<Vampiro>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerPorId(id)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Vampiro>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<List<Vampiro>>> ObtenerTodos()
        {
            try
            {
                return new ActionResponse<List<Vampiro>>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerTodos()
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<List<Vampiro>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }
    }
}
