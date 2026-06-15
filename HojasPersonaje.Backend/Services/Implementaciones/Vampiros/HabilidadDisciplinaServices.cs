using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Helpers;
using HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros;
using HojasPersonaje.Backend.Services.Interfaces.Vampiros;

namespace HojasPersonaje.Backend.Services.Implementaciones.Vampiros
{
    public class HabilidadDisciplinaServices : IHabilidadDisciplinaServices
    {
        private readonly IHabilidadDisciplinaRepository _repository;

        public HabilidadDisciplinaServices(IHabilidadDisciplinaRepository repository)
        {
            _repository = repository;
        }



        public async Task<ActionResponse<HabilidadDisciplina>> Editar(HabilidadDisciplina entidad)
        {
            try
            {
                return new ActionResponse<HabilidadDisciplina>
                {
                    Exitoso = true,
                    Resultado = await _repository.Editar(entidad)
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<HabilidadDisciplina>
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

        public async Task<ActionResponse<HabilidadDisciplina>> Guardar(HabilidadDisciplina entidad)
        {
            try
            {
                return new ActionResponse<HabilidadDisciplina>
                {
                    Exitoso = true,
                    Resultado = await _repository.Guardar(entidad)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<HabilidadDisciplina>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<List<HabilidadDisciplina>>> GuardarTodos(List<HabilidadDisciplina> entidades)
        {
            var lista = new List<HabilidadDisciplina>();

            try
            {
                foreach (var habilidad in entidades)
                {
                    lista.Add(await _repository.Guardar(habilidad));
                }

                return new ActionResponse<List<HabilidadDisciplina>>
                {
                    Exitoso = true,
                    Resultado = lista
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<List<HabilidadDisciplina>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<HabilidadDisciplina>> ObtenerPorId(int id)
        {
            try
            {
                return new ActionResponse<HabilidadDisciplina>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerPorId(id)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<HabilidadDisciplina>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<List<HabilidadDisciplina>>> ObtenerTodos()
        {
            try
            {
                return new ActionResponse<List<HabilidadDisciplina>>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerTodos()
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<List<HabilidadDisciplina>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<List<HabilidadDisciplina>>> ObtenerTodosPorId(int id)
        {
            try
            {
                return new ActionResponse<List<HabilidadDisciplina>>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerTodosPorId(id)
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<List<HabilidadDisciplina>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }
    }
}
