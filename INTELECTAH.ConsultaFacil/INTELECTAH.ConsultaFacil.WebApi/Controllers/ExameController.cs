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
    public class ExameController : ControllerBase
    {
        private readonly IExameService _exameService;
        private readonly ILogger<ExameController> _logger;
        private readonly IOptions<ApiSettings> _apiSettings;
        private readonly string _unavailable;

        public ExameController(IExameService exameService,
            ILogger<ExameController> logger,
            IOptions<ApiSettings> apiSettings)
        {
            _exameService = exameService;
            _logger = logger;
            _apiSettings = apiSettings;
            _unavailable = _apiSettings.Value.UnavailableMessage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_exameService.GetAll().Select(s => s.ToExameViewModel()).ToArray());
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
                return Ok(_exameService.Get(id).ToExameViewModel());
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
        public IActionResult Post([FromBody] ExameViewModel viewModel)
        {
            try
            {
                _exameService.Create(viewModel.ToEntity());
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
        public IActionResult Put([FromBody] ExameViewModel viewModel)
        {
            try
            {
                _exameService.Update(viewModel.ToEntity());
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
                _exameService.Delete(_exameService.Get(id));
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
