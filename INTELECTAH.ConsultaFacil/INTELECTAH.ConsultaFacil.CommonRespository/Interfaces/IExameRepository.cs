using INTELECTAH.ConsultaFacil.Domain;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.CommonRespository
{
    public interface IExameRepository
    {
        Exame Find(int id);

        IEnumerable<Exame> GetList();

        void Add(Exame entity);

        void Update(Exame entity);

        void Remove(Exame entity);

        Exame FindByNome(string nome);

        void ValidateDependency(Exame entity);
    }
}
