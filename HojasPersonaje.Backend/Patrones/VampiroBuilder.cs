using HojasPersonaje.Backend.Entidades.Vampiros;

namespace HojasPersonaje.Backend.Patrones
{
    public class VampiroBuilder
    {
        public readonly Vampiro _vampiro;

        public VampiroBuilder()
        {
            _vampiro = new Vampiro()
            {
                Nombre = "",
                DebilidadesClanes = new List<ClanBane>(),
                disciplinaVampiros = new List<DisciplinaVampiro>()
            };
        }

        public VampiroBuilder ConNombre(string nombre)
        {
            _vampiro.Nombre = nombre;
            return this;
        }

        public VampiroBuilder ConClanBane(ClanBane clanBane)
        {
            _vampiro.DebilidadesClanes!.Add(clanBane);
            return this;
        }

        public VampiroBuilder ConDisciplinas(List<Disciplina> disciplinas)
        {
            foreach (var disciplina in disciplinas)
            {
                _vampiro.disciplinaVampiros!.Add(new DisciplinaVampiro
                {
                    disciplinaId = disciplina.Id
                });
            }

            return this;
        }

        public Vampiro Build()
        {
            return _vampiro;
        }
    }
}
