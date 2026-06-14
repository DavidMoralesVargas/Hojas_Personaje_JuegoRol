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
                var vampiro = await _repository.ObtenerPorIdFull(entidad.id);

                if (vampiro == null)
                {
                    return new ActionResponse<Vampiro>
                    {
                        Exitoso = false,
                        Mensaje = "El vampiro no existe."
                    };
                }

                var eliminar = await _repository.EliminarDisciplinas(vampiro);
                if (!eliminar)
                {
                    return new ActionResponse<Vampiro>
                    {
                        Exitoso = false,
                        Mensaje = "Ha ocurrido un error al eliminar las depedencias"
                    };
                }

                vampiro.Nombre = entidad.Nombre;
                vampiro.disciplinaVampiros = new List<DisciplinaVampiro>();
                vampiro.DebilidadesClanes = new List<ClanBane>()
                {
                    entidad.clanBane!
                };

                foreach(var disciplina in entidad.Disciplinas!)
                {      
                    vampiro.disciplinaVampiros!.Add(new DisciplinaVampiro
                    {
                        disciplinaId = disciplina.Id
                    });
                }

                return new ActionResponse<Vampiro>
                {
                    Exitoso = true,
                    Resultado = await _repository.Editar(vampiro)
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
                await _repository.Eliminar(id);

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
                    Resultado = await _repository.ObtenerPorIdFull(id)
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
