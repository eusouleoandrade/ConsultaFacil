using AutoMapper;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.ViewModel.Implementations;

namespace INTELECTAH.ConsultaFacil.Mapper.Implementations
{
    public static class TipoExameMapper
    {
        public static TipoExameViewModel ToTipoExameViewModel(this TipoExame model)
        {
            IMapper mapper = TipoExameToTipoExameViewModelConfig().CreateMapper();
            return mapper.Map<TipoExameViewModel>(model);
        }

        public static TipoExame ToEntity(this TipoExameViewModel viewModel)
        {
            IMapper mapper = TipoExameToTipoExameViewModelConfig().CreateMapper();
            return mapper.Map<TipoExame>(viewModel);
        }

        private static MapperConfiguration TipoExameToTipoExameViewModelConfig()
        {
            return new MapperConfiguration(m =>
            {
                m.CreateMap<TipoExame, TipoExameViewModel>();
                m.CreateMap<TipoExameViewModel, TipoExame>();
            });
        }
    }
}