using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Exception.Implementations;
using INTELECTAH.ConsultaFacil.xUnitTest.Abstrations;
using Xunit;

namespace INTELECTAH.ConsultaFacil.xUnitTest.Implementations
{
    public class TipoExameServiceEdit : TipoExameServiceTest
    {
        [Fact]
        public void CheckEdit()
        {
            // Arranje
            TipoExame updateTipoExame = new TipoExame
            {
                TipoExameId = 2,
                Nome = "Exame Raio X",
                Descricao = "Exame de imagem."
            };

            // Act
            _tipoExameService.Update(updateTipoExame);

            // Assert
            var expectedTipoExame = _tipoExameService.Get(updateTipoExame.TipoExameId);
            Assert.NotNull(expectedTipoExame);
            Assert.Equal(expectedTipoExame.Nome, updateTipoExame.Nome);
        }

        [Fact]
        public void CheckEditWithExceptionReturn()
        {
            // Arranje
            TipoExame updateTipoExame = new TipoExame
            {
                TipoExameId = 1,
                Nome = "Raio X",
                Descricao = "Exame de imagem."
            };

            // Act
            void act() => _tipoExameService.Update(updateTipoExame);

            // Assert
            string expectedMessage = "Tipo Exame já cadastrado";
            ServiceException exception = Assert.Throws<ServiceException>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
