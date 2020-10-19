using INTELECTAH.ConsultaFacil.Exception;
using INTELECTAH.ConsultaFacil.Mapper.Implementations;
using INTELECTAH.ConsultaFacil.Service.Interfaces;
using INTELECTAH.ConsultaFacil.ViewModel.Implementations;
using INTELECTAH.ConsultaFacil.WebApi.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;

namespace INTELECTAH.ConsultaFacil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;
        private readonly ILogger<ConsultaController> _logger;
        private readonly IOptions<ApiSettings> _apiSettings;
        private readonly string _unavailable;

        public ConsultaController(IConsultaService consultaService,
            ILogger<ConsultaController> logger,
            IOptions<ApiSettings> apiSettings)
        {
            _consultaService = consultaService;
            _logger = logger;
            _apiSettings = apiSettings;
            _unavailable = _apiSettings.Value.UnavailableMessage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_consultaService.GetAll().Select(s => s.ToConsultaViewModel()).ToArray());
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
                return Ok(_consultaService.Get(id).ToConsultaViewModel());
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
        public IActionResult Post([FromBody] ConsultaViewModel viewModel)
        {
            try
            {
                _consultaService.Create(viewModel.ToEntity());
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
        public IActionResult Put([FromBody] ConsultaViewModel viewModel)
        {
            try
            {
                _consultaService.Update(viewModel.ToEntity());
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
                _consultaService.Delete(_consultaService.Get(id));
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
