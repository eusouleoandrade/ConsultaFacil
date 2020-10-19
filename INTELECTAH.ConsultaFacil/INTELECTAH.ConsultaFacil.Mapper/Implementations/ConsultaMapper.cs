using AutoMapper;
using INTELECTAH.ConsultaFacil.Domain.Entities;
using INTELECTAH.ConsultaFacil.ViewModel.Implementations;

namespace INTELECTAH.ConsultaFacil.Mapper.Implementations
{
    public static class ConsultaMapper
    {
        public static ConsultaViewModel ToConsultaViewModel(this Consulta model)
        {
            IMapper mapper = ConsultaToConsultaViewModelConfig().CreateMapper();
            return mapper.Map<ConsultaViewModel>(model);
        }

        public static Consulta ToEntity(this ConsultaViewModel viewModel)
        {
            IMapper mapper = ConsultaToConsultaViewModelConfig().CreateMapper();
            return mapper.Map<Consulta>(viewModel);
        }

        private static MapperConfiguration ConsultaToConsultaViewModelConfig()
        {
            return new MapperConfiguration(m =>
            {
                m.CreateMap<Consulta, ConsultaViewModel>();
                m.CreateMap<ConsultaViewModel, Consulta>();
            });
        }
    }
}
