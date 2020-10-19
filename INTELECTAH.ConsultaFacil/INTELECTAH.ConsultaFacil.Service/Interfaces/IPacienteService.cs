using INTELECTAH.ConsultaFacil.Domain;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Service
{
    public interface IPacienteService
    {
        Paciente Get(int id);

        IEnumerable<Paciente> GetAll();

        void Create(Paciente entity);

        void Update(Paciente entity);

        void Delete(Paciente entity);
    }
}
