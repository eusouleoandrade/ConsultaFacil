using INTELECTAH.ConsultaFacil.xUnitTest.Abstrations;
using Xunit;

namespace INTELECTAH.ConsultaFacil.xUnitTest.Implementations
{
    public class TipoExameServiceGet : TipoExameServiceTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ReturnValue(int id)
        {
            // Arranje / Act
            var tipoExame = _tipoExameService.Get(id);

            // Assert
            Assert.NotNull(tipoExame);
        }

        [Fact]
        public void ReturnHemograma()
        {
            // Arranje
            int id = 1;

            // Act
            var tipoExame = _tipoExameService.Get(id);

            // Assert
            string expectTitle = "Hemograma";
            Assert.Equal(expectTitle, tipoExame.Nome);
        }

        [Fact]
        public void ReturnRaioX()
        {
            // Arranje
            int id = 2;

            // Act
            var tipoExame = _tipoExameService.Get(id);

            // Assert
            string expectedTitle = "Raio X";
            Assert.Equal(expectedTitle, tipoExame.Nome);
        }
    }
}
