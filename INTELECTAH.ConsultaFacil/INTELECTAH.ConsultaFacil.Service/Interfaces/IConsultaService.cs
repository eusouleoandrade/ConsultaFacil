using INTELECTAH.ConsultaFacil.Domain.Entities;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Service.Interfaces
{
    public interface IConsultaService
    {
        Consulta Get(int id);

        IEnumerable<Consulta> GetAll();

        void Create(Consulta entity);

        void Update(Consulta entity);

        void Delete(Consulta entity);
    }
}
