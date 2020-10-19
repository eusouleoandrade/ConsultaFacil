using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Exception.Implementations;
using INTELECTAH.ConsultaFacil.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Service.Implementations
{
    public class ExameService : IExameService
    {
        private readonly IExameRepository _repository;

        public ExameService(IExameRepository repository)
        {
            _repository = repository;
        }

        public void Create(Exame entity)
        {
            CheckIsNull(entity);
            entity.Validate();
            CheckContainsByNome(entity);
            _repository.ValidateDependency(entity);
            _repository.Add(entity);
        }

        public void Delete(Exame entity)
        {
            CheckIsNull(entity);
            CheckNotContains(entity);
            _repository.Remove(entity);
        }

        public Exame Get(int id)
        {
            CheckIdIsValid(id);
            var entity = _repository.Find(id);
            CheckIsNull(entity);
            return entity;
        }

        public IEnumerable<Exame> GetAll()
        {
            return _repository.GetList();
        }

        public void Update(Exame entity)
        {
            CheckIsNull(entity);
            entity.Validate();
            CheckContainsByNome(entity);
            _repository.ValidateDependency(entity);
            _repository.Update(entity);
        }

        private void CheckIsNull(Exame entity)
        {
            if (entity is null)
                throw new ServiceException("Exame não encontrado");
        }

        private void CheckContainsByNome(Exame entity)
        {
            var entityByNome = _repository.FindByNome(entity.Nome);

            if (!(entityByNome is null) && entity.ExameId != entityByNome.ExameId)
                throw new ServiceException("Exame já cadastrado");
        }

        private void CheckNotContains(Exame entity)
        {
            if (_repository.Find(entity.ExameId) is null)
                throw new ServiceException("Exame não cadastrado");
        }

        private void CheckIdIsValid(int id)
        {
            if (id <= Decimal.Zero)
                throw new ServiceException("Identificador inválido");
        }
    }
}
