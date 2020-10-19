using INTELECTAH.ConsultaFacil.Domain.Entities;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.CommonRespository
{
    public interface IConsultaRepository
    {
        Consulta Find(int id);

        IEnumerable<Consulta> GetList();

        void Add(Consulta entity);

        void Update(Consulta entity);

        void Remove(Consulta entity);

        Consulta FindByDataHora(DateTime dataHora);

        void ValidateDependency(Consulta entity);
    }
}
