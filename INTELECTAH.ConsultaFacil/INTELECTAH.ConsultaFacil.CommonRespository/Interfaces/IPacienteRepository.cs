using INTELECTAH.ConsultaFacil.Domain;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.CommonRespository
{
    public interface IPacienteRepository
    {
        Paciente Find(int id);

        Paciente FindByCpf(string cpf);

        IEnumerable<Paciente> GetList();

        void Add(Paciente entity);

        void Update(Paciente entity);

        void Remove(Paciente entity);
    }
}
