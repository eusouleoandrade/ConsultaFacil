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
    public class ExameSQLServerRepository : BaseSqlServerRepository, IExameRepository
    {
        public ExameSQLServerRepository(DataBaseContext context) : base(context)
        {
        }

        public void Add(Exame entity)
        {
            try
            {
                _dbContext.Exames.Add(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public Exame Find(int id)
        {
            try
            {
                var entity = _dbContext.Exames.Find(id);
                GetDependeny(entity);
                return entity;
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public Exame FindByNome(string nome)
        {
            try
            {
                return _dbContext.Exames.Where(w => w.Nome == nome).FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public IEnumerable<Exame> GetList()
        {
            try
            {
                return _dbContext.Exames
                    .Include(i => i.TipoExame)
                    .ToList();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void Remove(Exame entity)
        {
            try
            {
                _dbContext.Exames.Remove(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void Update(Exame entity)
        {
            try
            {
                DetachLocal(d => d.ExameId == entity.ExameId);
                _dbContext.Exames.Update(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        private void DetachLocal(Func<Exame, bool> predicate)
        {
            var local = _dbContext.Set<Exame>().Local.Where(predicate).FirstOrDefault();

            if (!(local is null))
                _dbContext.Entry(local).State = EntityState.Detached;
        }

        public void ValidateDependency(Exame entity)
        {
            if (_dbContext.TipoExames.Find(entity.TipoExameId) is null)
                throw new RepositoryException("Tipo Exame não cadastrado");
        }

        private void GetDependeny(Exame entity)
        {
            if(!(entity is null))
                _dbContext.Entry(entity).Reference(b => b.TipoExame).Load();
        }
    }
}