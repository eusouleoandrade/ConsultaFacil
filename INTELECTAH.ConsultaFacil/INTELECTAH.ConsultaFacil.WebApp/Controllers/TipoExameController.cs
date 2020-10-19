using INTELECTAH.ConsultaFacil.ViewModel.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace INTELECTAH.ConsultaFacil.WebApp.Controllers
{
    public class TipoExameController : Controller
    {
        private readonly ILogger<TipoExameController> _logger;
        private readonly HttpClient _client;

        public TipoExameController(ILogger<TipoExameController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _client = clientFactory.CreateClient("ClientApi");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return GetListViewModel();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return GetViewModelById(id);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TipoExameViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return SetViewModelByActionName(nameof(Create), viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return GetViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TipoExameViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return SetViewModelByActionName(nameof(Edit), viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return GetViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TipoExameViewModel viewModel)
        {
            return SetViewModelByActionName(nameof(Delete), viewModel);
        }

        private IActionResult GetListViewModel()
        {
            HttpResponseMessage response = null;

            try
            {
                response = _client.GetAsync($"tipoexame").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<IEnumerable<TipoExameViewModel>>(response.Content.ReadAsStringAsync().Result);
                    return View(viewModel.OrderBy(o => o.Nome));
                }
                else
                {
                    return SendFeedback(response);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(response);
            }
        }

        private IActionResult GetViewModelById(int id)
        {
            HttpResponseMessage response = null;

            try
            {
                response = _client.GetAsync($"tipoexame/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<TipoExameViewModel>(response.Content.ReadAsStringAsync().Result);
                    return View(viewModel);
                }
                else
                {
                    return SendFeedback(response);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(response);
            }
        }

        private IActionResult SetViewModelByActionName(string actionName, TipoExameViewModel viewModel)
        {
            HttpResponseMessage response = null;
            string messageFeedback = "";

            try
            {
                switch (actionName)
                {
                    case nameof(Delete):
                        response = _client.DeleteAsync($"tipoexame/{viewModel.TipoExameId}").Result;
                        messageFeedback = $"Tipo Exame {viewModel.Nome?.ToUpper()} deletado com sucesso.";
                        break;

                    case nameof(Create):
                        var contentCreate = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PostAsync("tipoexame", contentCreate).Result;
                        messageFeedback = $"Tipo Exame { viewModel.Nome?.ToUpper()} criado com sucesso.";
                        break;

                    case nameof(Edit):
                        var contentEdit = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PutAsync($"tipoexame/{viewModel.TipoExameId}", contentEdit).Result;
                        messageFeedback = $"Tipo Exame {viewModel.Nome?.ToUpper()} editado com sucesso.";
                        break;
                }

                if (response.IsSuccessStatusCode)
                {
                    SendFeedback(false, messageFeedback);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return SendFeedback(response);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                return SendFeedback(response);
            }
        }

        private IActionResult SendFeedback(HttpResponseMessage response)
        {
            var feedbackResponse = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = "" });
            SendFeedback(true, feedbackResponse.Message);
            return View();
        }

        private void SendFeedback(bool isError, string message)
        {
            if (isError)
                TempData["MessageError"] = message;
            else
                TempData["Message"] = message;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _client?.Dispose();
            base.Dispose(disposing);
        }
    }
}