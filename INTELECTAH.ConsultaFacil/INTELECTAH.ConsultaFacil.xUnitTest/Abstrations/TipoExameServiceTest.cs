using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Service.Implementations;
using INTELECTAH.ConsultaFacil.Service.Interfaces;
using INTELECTAH.ConsultaFacil.xUnitTest.Mocks;

namespace INTELECTAH.ConsultaFacil.xUnitTest.Abstrations
{
    public abstract class TipoExameServiceTest
    {
        protected readonly ITipoExameService _tipoExameService;
        protected readonly ITipoExameRepository _mockRepository;

        public TipoExameServiceTest()
        {
            _mockRepository = new TipoExameMockRepository();
            _tipoExameService = new TipoExameService(_mockRepository);
        }
    }
}
