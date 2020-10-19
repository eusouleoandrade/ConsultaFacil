using INTELECTAH.ConsultaFacil.Domain;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Service.Interfaces
{
    public interface IExameService
    {
        Exame Get(int id);

        IEnumerable<Exame> GetAll();

        void Create(Exame entity);

        void Update(Exame entity);

        void Delete(Exame entity);
    }
}
