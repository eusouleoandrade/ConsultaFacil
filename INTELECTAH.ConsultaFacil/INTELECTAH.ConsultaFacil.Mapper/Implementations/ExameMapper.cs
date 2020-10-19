using AutoMapper;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.ViewModel.Implementations;

namespace INTELECTAH.ConsultaFacil.Mapper.Implementations
{
    public static class ExameMapper
    {
        public static ExameViewModel ToExameViewModel(this Exame model)
        {
            IMapper mapper = ExameToExameViewModelConfig().CreateMapper();
            return mapper.Map<ExameViewModel>(model);
        }

        public static Exame ToEntity(this ExameViewModel viewModel)
        {
            IMapper mapper = ExameToExameViewModelConfig().CreateMapper();
            return mapper.Map<Exame>(viewModel);
        }

        private static MapperConfiguration ExameToExameViewModelConfig()
        {
            return new MapperConfiguration(m =>
            {
                m.CreateMap<Exame, ExameViewModel>();
                m.CreateMap<ExameViewModel, Exame>();
            });
        }
    }
}
