using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Exception;
using INTELECTAH.ConsultaFacil.Repository.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INTELECTAH.ConsultaFacil.SQLServerRepository
{
    public class PacienteSQLServerRespository : BaseSqlServerRepository, IPacienteRepository
    {
        public PacienteSQLServerRespository(DataBaseContext context) : base(context) { }

        public void Add(Paciente entity)
        {
            try
            {
                _dbContext.Pacientes.Add(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public Paciente Find(int id)
        {
            try
            {
                return _dbContext.Pacientes.Find(id);
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public IEnumerable<Paciente> GetList()
        {
            try
            {
                return _dbContext.Pacientes.ToList();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void Remove(Paciente entity)
        {
            try
            {
                _dbContext.Pacientes.Remove(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public void Update(Paciente entity)
        {
            try
            {
                DetachLocal(d => d.PacienteId == entity.PacienteId);
                _dbContext.Pacientes.Update(entity);
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        public Paciente FindByCpf(string cpf)
        {
            try
            {
                return _dbContext.Pacientes.Where(w => w.CPF == cpf).FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                throw new RepositoryException(ex);
            }
        }

        private void DetachLocal(Func<Paciente, bool> predicate)
        {
            var local = _dbContext.Set<Paciente>().Local.Where(predicate).FirstOrDefault();

            if (!(local is null))
                _dbContext.Entry(local).State = EntityState.Detached;
        }
    }
}
