using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Helpers;
using HojasPersonaje.Backend.Repositorios.Interfaces.Vampiros;
using HojasPersonaje.Backend.Services.Interfaces.Vampiros;
using System.Linq.Expressions;

namespace HojasPersonaje.Backend.Services.Implementaciones.Vampiros
{
    public class DisciplinasServices : IDisciplinasServices
    {
        private readonly IDisciplinasRepository _repository;
        private readonly IHabilidadDisciplinaServices _habilidadServices;
        private readonly IHabilidadDisciplinaRepository _habilidadRepository;

        public DisciplinasServices(IDisciplinasRepository repository, IHabilidadDisciplinaServices habilidadServices, IHabilidadDisciplinaRepository habilidadRepository)
        {
            _repository = repository;
            _habilidadServices = habilidadServices;
            _habilidadRepository = habilidadRepository;
        }

        public async Task<ActionResponse<Disciplina>> Editar(Disciplina entidad)
        {
            try
            {
                return new ActionResponse<Disciplina>
                {
                    Exitoso = true,
                    Resultado = await _repository.Editar(entidad)
                };
            }
            catch (Exception e)
            {
                return new ActionResponse<Disciplina>
                {
                    Exitoso = false,
                    Mensaje = e.Message
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
            catch (Exception e)
            {
                return new ActionResponse<bool>
                {
                    Exitoso = false,
                    Mensaje = e.Message
                };
            }
        }

        public async Task<ActionResponse<Disciplina>> Guardar(DisciplinaDTO entidad)
        {
            try
            {
                var resultado = await _repository.Guardar(ConvertirADisciplina(entidad));
                return new ActionResponse<Disciplina>
                {
                    Exitoso = true,
                    Resultado = resultado
                };
            }
            catch (Exception e)
            {
                return new ActionResponse<Disciplina>
                {
                    Exitoso = false,
                    Mensaje = e.Message
                }; 
            }
        }

        public async Task<ActionResponse<Disciplina>> ObtenerPorId(int id)
        {
            try
            {
                return new ActionResponse<Disciplina>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerPorId(id)
                };
            }
            catch (Exception e)
            {
                return new ActionResponse<Disciplina>
                {
                    Exitoso = false,
                    Mensaje = e.Message
                };
            }
        }

        public async Task<ActionResponse<List<Disciplina>>> ObtenerTodos()
        {
            try
            {
                return new ActionResponse<List<Disciplina>>
                {
                    Exitoso = true,
                    Resultado = await _repository.ObtenerTodos()
                };
            }
            catch(Exception e)
            {
                return new ActionResponse<List<Disciplina>>
                {
                    Exitoso = false,
                    Mensaje = e.Message
                };
            }
        }

        public async Task<ActionResponse<List<Disciplina>>> Combo()
        {
            try
            {
                return new ActionResponse<List<Disciplina>>
                {
                    Exitoso = true,
                    Resultado = await _repository.Combo()
                };
            }
            catch (Exception e)
            {
                return new ActionResponse<List<Disciplina>>
                {
                    Exitoso = false,
                    Mensaje = e.Message
                };
            }
        }

        //Método para convertir de DTO a entidad
        private Disciplina ConvertirADisciplina(DisciplinaDTO dto)
        {
            return new Disciplina
            {
                Nombre = dto.nombre,
                habilidadDisciplinas = dto.habilidades
            };
        }
    }
}
