using INTELECTAH.ConsultaFacil.Exception.Implementations;
using INTELECTAH.ConsultaFacil.xUnitTest.Abstrations;
using Xunit;

namespace INTELECTAH.ConsultaFacil.xUnitTest.Implementations
{
    public class TipoExameServiceDelete : TipoExameServiceTest
    {
        [Fact]
        public void CheckDelete()
        {
            // Arranje
            var tipoExame = _tipoExameService.Get(1);

            // Act
            _tipoExameService.Delete(tipoExame);

            // Assert
            string expectedMessage = "Tipo Exame não encontrado";
            void actAssert() => _tipoExameService.Get(tipoExame.TipoExameId);
            ServiceException exception = Assert.Throws<ServiceException>(actAssert);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
