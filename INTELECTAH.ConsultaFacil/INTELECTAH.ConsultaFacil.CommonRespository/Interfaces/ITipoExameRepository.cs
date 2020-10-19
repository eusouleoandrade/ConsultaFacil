using INTELECTAH.ConsultaFacil.Domain;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.CommonRespository
{
    public interface ITipoExameRepository
    {
        TipoExame Find(int id);

        IEnumerable<TipoExame> GetList();

        void Add(TipoExame entity);

        void Update(TipoExame entity);

        void Remove(TipoExame entity);

        TipoExame FindByNome(string nome);
    }
}
