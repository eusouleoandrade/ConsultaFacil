using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Exception;
using INTELECTAH.ConsultaFacil.SQLServerRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INTELECTAH.ConsultaFacil.Repository.SqlServer.Implementations
{
    public class TipoExameSQLServerRepository : BaseSqlServerRepository, ITipoExameRepository
    {
        public TipoExameSQLServerRepository(DataBaseContext context) : base(context) { }

        public void Add(TipoExame entity)
        {
            try
            {
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
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
                return _dbContext.TipoExames.Find(id);
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
                return _dbContext.TipoExames.Where(w => w.Nome == nome).FirstOrDefault();
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
                return _dbContext.TipoExames.ToList();
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
                _dbContext.TipoExames.Remove(entity);
                _dbContext.SaveChanges();
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
                DetachLocal(d => d.TipoExameId == entity.TipoExameId);
                _dbContext.TipoExames.Update(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        private void DetachLocal(Func<TipoExame, bool> predicate)
        {
            var local = _dbContext.Set<TipoExame>().Local.Where(predicate).FirstOrDefault();

            if (!(local is null))
                _dbContext.Entry(local).State = EntityState.Detached;
        }
    }
}
