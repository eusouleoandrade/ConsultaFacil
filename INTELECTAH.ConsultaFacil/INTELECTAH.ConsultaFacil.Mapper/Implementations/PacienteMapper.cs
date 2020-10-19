using AutoMapper;
using INTELECTAH.ConsultaFacil.Domain;
using INTELECTAH.ConsultaFacil.ViewModel;

namespace INTELECTAH.ConsultaFacil.Mapper
{
    public static class PacienteMapper
    {
        public static PacienteViewModel ToPacienteViewModel(this Paciente model)
        {
            IMapper mapper = PacienteToPacienteViewModelConfig().CreateMapper();
            return mapper.Map<PacienteViewModel>(model);
        }

        public static Paciente ToEntity(this PacienteViewModel viewModel)
        {
            IMapper mapper = PacienteToPacienteViewModelConfig().CreateMapper();
            return mapper.Map<Paciente>(viewModel);
        }

        private static MapperConfiguration PacienteToPacienteViewModelConfig()
        {
            return new MapperConfiguration(m =>
            {
                m.CreateMap<Paciente, PacienteViewModel>();
                m.CreateMap<PacienteViewModel, Paciente>();
            });
        }
    }
}
