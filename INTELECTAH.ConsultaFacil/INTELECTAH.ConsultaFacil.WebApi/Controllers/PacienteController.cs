using INTELECTAH.ConsultaFacil.Exception;
using INTELECTAH.ConsultaFacil.Mapper;
using INTELECTAH.ConsultaFacil.Service;
using INTELECTAH.ConsultaFacil.ViewModel;
using INTELECTAH.ConsultaFacil.WebApi.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;

namespace INTELECTAH.ConsultaFacil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        private readonly ILogger<PacienteController> _logger;
        private readonly IOptions<ApiSettings> _apiSettings;
        private readonly string _unavailable;

        public PacienteController(IPacienteService pacienteService, 
            ILogger<PacienteController> logger, 
            IOptions<ApiSettings> apiSettings)
        {
            _pacienteService = pacienteService;
            _logger = logger;
            _apiSettings = apiSettings;
            _unavailable = _apiSettings.Value.UnavailableMessage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_pacienteService.GetAll().Select(s => s.ToPacienteViewModel()).ToArray());
            }
            catch (AppException ex)
            {
                return SendFeedback(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(_unavailable);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_pacienteService.Get(id).ToPacienteViewModel());
            }
            catch (AppException ex)
            {
                return SendFeedback(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(_unavailable);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PacienteViewModel viewModel)
        {
            try
            {
                _pacienteService.Create(viewModel.ToEntity());
                return Ok();
            }
            catch (AppException ex)
            {
                return SendFeedback(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(_unavailable);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PacienteViewModel viewModel)
        {
            try
            {
                _pacienteService.Update(viewModel.ToEntity());
                return Ok();
            }
            catch (AppException ex)
            {
                return SendFeedback(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(_unavailable);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _pacienteService.Delete(_pacienteService.Get(id));
                return Ok();
            }
            catch (AppException ex)
            {
                return SendFeedback(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(_unavailable);
            }
        }

        private IActionResult SendFeedback(string message)
        {
            return BadRequest(new { Message = message });
        }
    }
}
