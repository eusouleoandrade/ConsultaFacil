using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace INTELECTAH.ConsultaFacil.SQLServerRepository
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<Consulta> Consultas { get; set; }

        public DbSet<TipoExame> TipoExames { get; set; }

        public DbSet<Exame> Exames { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
    }
}