using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Domain.Entities;
using INTELECTAH.ConsultaFacil.Exception;
using INTELECTAH.ConsultaFacil.SQLServerRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INTELECTAH.ConsultaFacil.Repository.SqlServer.Implementations
{
    public class ConsultaSQLServerRespository : BaseSqlServerRepository, IConsultaRepository
    {
        public ConsultaSQLServerRespository(DataBaseContext context) : base(context) { }

        public void Add(Consulta entity)
        {
            try
            {
                _dbContext.Consultas.Add(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public Consulta Find(int id)
        {
            try
            {
                var entity = _dbContext.Consultas.Find(id);
                GetDependency(entity);
                return entity;
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public Consulta FindByDataHora(DateTime dataHora)
        {
            try
            {
                return _dbContext.Consultas.Where(w => w.DataHora == dataHora).FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public IEnumerable<Consulta> GetList()
        {
            try
            {
                return _dbContext.Consultas
                    .Include(e => e.Exame)
                    .Include(p => p.Paciente)
                    .ToList();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void Remove(Consulta entity)
        {
            try
            {
                _dbContext.Consultas.Remove(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void Update(Consulta entity)
        {
            try
            {
                DetachLocal(d => d.ConsultaId == entity.ConsultaId);
                _dbContext.Consultas.Update(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        private void DetachLocal(Func<Consulta, bool> predicate)
        {
            var local = _dbContext.Set<Consulta>().Local.Where(predicate).FirstOrDefault();

            if (!(local is null))
                _dbContext.Entry(local).State = EntityState.Detached;
        }

        public void ValidateDependency(Consulta entity)
        {
            IList<string> exceptions = new List<string>();

            if (_dbContext.Pacientes.Find(entity.PacienteId) is null)
                exceptions.Add("Paciente não cadastrado");

            if (_dbContext.Exames.Find(entity.ExameId) is null)
                exceptions.Add("Exame não cadastrado");

            if (exceptions.Count > Decimal.Zero)
                throw new RepositoryException(exceptions);
        }

        private void GetDependency(Consulta entity)
        {
            if (!(entity is null))
            {
                _dbContext.Entry(entity).Reference(b => b.Exame).Load();
                _dbContext.Entry(entity).Reference(b => b.Paciente).Load();
            }
        }
    }
}
