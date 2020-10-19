using INTELECTAH.ConsultaFacil.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace INTELECTAH.ConsultaFacil.xUnitTest.Mocks
{
    public abstract class MockRepository
    {
        protected readonly IEnumerable<TipoExame> _tipoExameData;

        public MockRepository()
        {
            _tipoExameData = GetTipoExameData();
        }

        private IEnumerable<TipoExame> GetTipoExameData()
        {
            return new List<TipoExame>
            {
                new TipoExame
                    {
                        TipoExameId = 1,
                        Nome = "Hemograma",
                        Descricao = "Exame de sangue.",
                    },
                    new TipoExame
                    {
                        TipoExameId = 2,
                        Nome = "Raio X",
                        Descricao = "Exame de imagem."
                    } 
            };
        }
    }
}
