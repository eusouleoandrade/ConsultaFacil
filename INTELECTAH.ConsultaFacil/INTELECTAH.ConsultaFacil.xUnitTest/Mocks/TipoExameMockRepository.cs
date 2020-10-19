using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INTELECTAH.ConsultaFacil.xUnitTest.Mocks
{
    public class TipoExameMockRepository : MockRepository, ITipoExameRepository
    {
        private readonly List<TipoExame> _data;

        public TipoExameMockRepository()
        {
            _data = _tipoExameData.ToList();
        }

        public void Add(TipoExame entity)
        {
            try
            {
                entity.TipoExameId = entity.TipoExameId <= Decimal.Zero ? _data.Select(s => s.TipoExameId).Max() + 1 : entity.TipoExameId;
                _data.Add(entity);
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public TipoExame Find(int id)
        {
            try
            {
                return _data.FirstOrDefault(f => f.TipoExameId == id);
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public TipoExame FindByNome(string nome)
        {
            try
            {
                return _data.FirstOrDefault(f => f.Nome == nome);
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public IEnumerable<TipoExame> GetList()
        {
            try
            {
                return _data;
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void Remove(TipoExame entity)
        {
            try
            {
                _data.Remove(entity);
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void Update(TipoExame entity)
        {
            try
            {
                Remove(Find(entity.TipoExameId));
                Add(entity);
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }
    }
}
