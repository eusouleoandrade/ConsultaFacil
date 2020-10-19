using INTELECTAH.ConsultaFacil.ViewModel.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace INTELECTAH.ConsultaFacil.WebApp.Controllers
{
    public class ExameController : Controller
    {
        private readonly ILogger<ExameController> _logger;
        private readonly HttpClient _client;

        public ExameController(ILogger<ExameController> logger, IHttpClientFactory clientFactory)
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
        public IActionResult Create()
        {
            SetSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExameViewModel viewModel)
        {
            if (ModelState.IsValid)
                return SetViewModelByActionName(nameof(Create), viewModel);

            SetSelectList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SetSelectList();
            return GetViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExameViewModel viewModel)
        {
            if (ModelState.IsValid)
                return SetViewModelByActionName(nameof(Edit), viewModel);

            SetSelectList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return GetViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ExameViewModel viewModel)
        {
            return SetViewModelByActionName(nameof(Delete), viewModel);
        }

        private IActionResult GetListViewModel()
        {
            HttpResponseMessage response = null;

            try
            {
                response = _client.GetAsync($"exame").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<IEnumerable<ExameViewModel>>(response.Content.ReadAsStringAsync().Result);
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
                response = _client.GetAsync($"exame/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<ExameViewModel>(response.Content.ReadAsStringAsync().Result);
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

        private IActionResult SetViewModelByActionName(string actionName, ExameViewModel viewModel)
        {
            HttpResponseMessage response = null;
            string messageFeedback = "";

            try
            {
                switch (actionName)
                {
                    case nameof(Delete):
                        response = _client.DeleteAsync($"exame/{viewModel.ExameId}").Result;
                        messageFeedback = $"Exame {viewModel.Nome?.ToUpper()} deletado com sucesso.";
                        break;

                    case nameof(Create):
                        var contentCreate = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PostAsync("exame", contentCreate).Result;
                        messageFeedback = $"Exame { viewModel.Nome?.ToUpper()} criado com sucesso.";
                        break;

                    case nameof(Edit):
                        var contentEdit = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PutAsync($"exame/{viewModel.ExameId}", contentEdit).Result;
                        messageFeedback = $"Exame {viewModel.Nome?.ToUpper()} editado com sucesso.";
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

        private IEnumerable<SelectListItem> SelectListTipoExame
        {
            get
            {
                HttpResponseMessage response = null;
                IEnumerable<SelectListItem> selectList = new List<SelectListItem>();

                try
                {
                    response = _client.GetAsync($"tipoexame").Result;


                    if (response.IsSuccessStatusCode)
                    {
                        var tipoExameViewModel = JsonConvert.DeserializeObject<IEnumerable<TipoExameViewModel>>(response.Content.ReadAsStringAsync().Result);
                        selectList = tipoExameViewModel.OrderBy(o => o.Nome)
                            .Select(s => new SelectListItem { Text = s.Nome, Value = s.TipoExameId.ToString() });

                        return selectList;
                    }
                    else
                    {
                        SendFeedback(response);
                        return selectList;
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message, ex, ex.InnerException);
                    SendFeedback(response);
                    return selectList;
                }
            }
        }

        private void SetSelectList()
        {
            ViewBag.TipoExameList = SelectListTipoExame;
        }

        private IActionResult SendFeedback(HttpResponseMessage response)
        {
            var feedbackResponse = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { Message = "" });
            SendFeedback(true, feedbackResponse.Message);
            SetSelectList();
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
