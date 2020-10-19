using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.Exception.Implementations;
using INTELECTAH.ConsultaFacil.xUnitTest.Abstrations;
using Xunit;

namespace INTELECTAH.ConsultaFacil.xUnitTest.Implementations
{
    public class TipoExameServiceCreate : TipoExameServiceTest
    {
        [Fact]
        public void CheckCreate()
        {
            // Arranje
            TipoExame newTipoExame = new TipoExame
            {
                Nome = "Tipo exame teste",
                Descricao = "Descrição tipo exame teste.",
            };

            // Act
            _tipoExameService.Create(newTipoExame);

            // Assert
            var expectedTipoExame = _mockRepository.FindByNome(newTipoExame.Nome);
            Assert.NotNull(expectedTipoExame);
            Assert.Equal(expectedTipoExame.Nome, newTipoExame.Nome);
            Assert.Equal(expectedTipoExame.Descricao, newTipoExame.Descricao);
        }

        [Fact]
        public void CheckCreateWithExceptionReturn()
        {
            // Arranje
            TipoExame newTipoExame = new TipoExame
            {
                Nome = "Hemograma",
                Descricao = "Exame de sangue.",
            };

            // Act
            void act() => _tipoExameService.Create(newTipoExame);

            // Assert
            string expectedMessage = "Tipo Exame já cadastrado";
            ServiceException exception = Assert.Throws<ServiceException>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
