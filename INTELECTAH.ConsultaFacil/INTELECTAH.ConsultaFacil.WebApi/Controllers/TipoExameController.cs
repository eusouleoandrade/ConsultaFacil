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
    public class TipoExameController : ControllerBase
    {
        private readonly ITipoExameService _tipoExameService;
        private readonly ILogger<TipoExameController> _logger;
        private readonly IOptions<ApiSettings> _apiSettings;
        private readonly string _unavailable;

        public TipoExameController(ITipoExameService tipoExameService, 
            ILogger<TipoExameController> logger, 
            IOptions<ApiSettings> apiSettings)
        {
            _tipoExameService = tipoExameService;
            _logger = logger;
            _apiSettings = apiSettings;
            _unavailable = _apiSettings.Value.UnavailableMessage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_tipoExameService.GetAll().Select(s => s.ToTipoExameViewModel()).ToArray());
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
                return Ok(_tipoExameService.Get(id).ToTipoExameViewModel());
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
        public IActionResult Post([FromBody] TipoExameViewModel viewModel)
        {
            try
            {
                _tipoExameService.Create(viewModel.ToEntity());
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
        public IActionResult Put([FromBody] TipoExameViewModel viewModel)
        {
            try
            {
                _tipoExameService.Update(viewModel.ToEntity());
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
                _tipoExameService.Delete(_tipoExameService.Get(id));
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