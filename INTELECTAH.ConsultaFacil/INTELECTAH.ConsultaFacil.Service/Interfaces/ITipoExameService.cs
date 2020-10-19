using INTELECTAH.ConsultaFacil.Domain;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Service.Interfaces
{
    public interface ITipoExameService
    {
        TipoExame Get(int id);

        IEnumerable<TipoExame> GetAll();

        void Create(TipoExame entity);

        void Update(TipoExame entity);

        void Delete(TipoExame entity);
    }
}
