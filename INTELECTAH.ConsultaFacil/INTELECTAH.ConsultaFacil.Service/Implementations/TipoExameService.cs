using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Exception.Implementations;
using INTELECTAH.ConsultaFacil.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Service.Implementations
{
    public class TipoExameService : ITipoExameService
    {
        private readonly ITipoExameRepository _repository;

        public TipoExameService(ITipoExameRepository repository)
        {
            _repository = repository;
        }

        public void Create(TipoExame entity)
        {
            CheckIsNull(entity);
            entity.Validate();
            CheckContainsByNome(entity);
            _repository.Add(entity);
        }

        public void Delete(TipoExame entity)
        {
            CheckIsNull(entity);
            CheckNotContains(entity);
            _repository.Remove(entity);
        }

        public TipoExame Get(int id)
        {
            CheckIdIsValid(id);
            var entity = _repository.Find(id);
            CheckIsNull(entity);
            return entity;
        }

        public IEnumerable<TipoExame> GetAll()
        {
            return _repository.GetList();
        }

        public void Update(TipoExame entity)
        {
            CheckIsNull(entity);
            entity.Validate();
            CheckContainsByNome(entity);
            _repository.Update(entity);
        }

        private void CheckIsNull(TipoExame entity)
        {
            if (entity is null)
                throw new ServiceException("Tipo Exame não encontrado");
        }

        private void CheckContainsByNome(TipoExame entity)
        {
            var entityByNome = _repository.FindByNome(entity.Nome);

            if (!(entityByNome is null) && entity.TipoExameId != entityByNome.TipoExameId)
                throw new ServiceException("Tipo Exame já cadastrado");
        }

        private void CheckNotContains(TipoExame entity)
        {
            if (_repository.Find(entity.TipoExameId) is null)
                throw new ServiceException("Tipo Exame não cadastrado");
        }

        private void CheckIdIsValid(int id)
        {
            if (id <= Decimal.Zero)
                throw new ServiceException("Identificador inválido");
        }
    }
}
