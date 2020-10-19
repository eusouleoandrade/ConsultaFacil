using INTELECTAH.ConsultaFacil.xUnitTest.Abstrations;
using System;
using System.Linq;
using Xunit;

namespace INTELECTAH.ConsultaFacil.xUnitTest.Implementations
{
    public class TipoExameServiceGetAll : TipoExameServiceTest
    {
        [Fact]
        public void ReturnAll()
        {
            // Arranje / Act
            var tipoExameList = _tipoExameService.GetAll();

            // Assert
            Assert.NotNull(tipoExameList);
            Assert.True(tipoExameList.Count() > Decimal.Zero);
        }
    }
}
