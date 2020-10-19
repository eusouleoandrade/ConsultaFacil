using INTELECTAH.ConsultaFacil.SQLServerRepository;
using System;

namespace INTELECTAH.ConsultaFacil.Repository.SqlServer
{
    public abstract class BaseSqlServerRepository : IDisposable
    {
        protected readonly DataBaseContext _dbContext;

        public BaseSqlServerRepository(DataBaseContext context)
        {
            _dbContext = context;
        }

        public virtual void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}