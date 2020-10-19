using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Domain.Entities;
using INTELECTAH.ConsultaFacil.Exception.Implementations;
using INTELECTAH.ConsultaFacil.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Service.Implementations
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _repository;

        public ConsultaService(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public void Create(Consulta entity)
        {
            CheckIsNull(entity);
            entity.GenerateProtocolo();
            entity.Validate();
            CheckContainsByDataHora(entity);
            _repository.ValidateDependency(entity);
            _repository.Add(entity);
        }

        public void Delete(Consulta entity)
        {
            CheckIsNull(entity);
            CheckNotContains(entity);
            _repository.Remove(entity);
        }

        public Consulta Get(int id)
        {
            CheckIdIsValid(id);
            var entity = _repository.Find(id);
            CheckIsNull(entity);
            return entity;
        }

        public IEnumerable<Consulta> GetAll()
        {
            return _repository.GetList();
        }

        public void Update(Consulta entity)
        {
            CheckIsNull(entity);
            entity.Validate();
            CheckContainsByDataHora(entity);
            _repository.ValidateDependency(entity);
            _repository.Update(entity);
        }

        private void CheckIdIsValid(int id)
        {
            if (id <= Decimal.Zero)
                throw new ServiceException("Identificador inválido");
        }

        private void CheckNotContains(Consulta entity)
        {
            if (_repository.Find(entity.ConsultaId) is null)
                throw new ServiceException("Consulta não cadastrado");
        }

        private void CheckContainsByDataHora(Consulta entity)
        {
            var entityByDataHora = _repository.FindByDataHora(entity.DataHora);

            if (!(entityByDataHora is null) && entity.ConsultaId != entityByDataHora.ConsultaId)
                throw new ServiceException("Existe Consulta cadastrada nesta Data e Hora");
        }

        private void CheckIsNull(Consulta entity)
        {
            if (entity is null)
                throw new ServiceException("Consulta não encontrada");
        }
    }
}
