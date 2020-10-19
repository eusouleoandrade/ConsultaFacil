using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Exception.Implementations;
using INTELECTAH.ConsultaFacil.Service.Common;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Service
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public void Create(Paciente entity)
        {
            CheckIsNull(entity);
            entity.Validate();
            CheckServiceIsValid(entity);
            CheckContainsByCpf(entity);
            _repository.Add(entity);
        }

        public void Delete(Paciente entity)
        {
            CheckIsNull(entity);
            CheckNotContains(entity);
            _repository.Remove(entity);
        }

        public Paciente Get(int id)
        {
            CheckIdIsValid(id);
            var entity = _repository.Find(id);
            CheckIsNull(entity);
            return entity;
        }

        public IEnumerable<Paciente> GetAll()
        {
            return _repository.GetList();
        }

        public void Update(Paciente entity)
        {
            CheckIsNull(entity);
            entity.Validate();
            CheckServiceIsValid(entity);
            CheckContainsByCpf(entity);
            _repository.Update(entity);
        }

        private void CheckIsNull(Paciente entity)
        {
            if (entity is null)
                throw new ServiceException("Paciente não encontrado");
        }

        private void CheckContainsByCpf(Paciente entity)
        {
            var entityByCpf = _repository.FindByCpf(entity.CPF);

            if (!(entityByCpf is null) && entity.PacienteId != entityByCpf.PacienteId)
                throw new ServiceException("Paciente já cadastrado");
        }

        private void CheckNotContains(Paciente entity)
        {
            if (_repository.Find(entity.PacienteId) is null)
                throw new ServiceException("Paciente não cadastrado");
        }

        private void CheckServiceIsValid(Paciente entity)
        {
            IList<string> exceptions = new List<string>();

            // Validate CPF
            if (!CpfCommonService.IsValid(entity.CPF))
                exceptions.Add("CPF inválido");

            // Validade Telefone
            if (!TelefoneCommonService.IsValid(entity.Telefone))
                exceptions.Add("Telefone inválido");

            // Validade E-mail
            if (!EmailCommonService.IsValid(entity.Email))
                exceptions.Add("Email inválido");

            if (exceptions.Count > Decimal.Zero)
                throw new ServiceException(exceptions);
        }

        private void CheckIdIsValid(int id)
        {
            if (id <= Decimal.Zero)
                throw new ServiceException("Identificador inválido");
        }
    }
}
