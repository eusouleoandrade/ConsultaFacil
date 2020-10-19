using INTELECTAH.ConsultaFacil.ViewModel;
using INTELECTAH.ConsultaFacil.ViewModel.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace INTELECTAH.ConsultaFacil.WebApp.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ILogger<ConsultaController> _logger;
        private readonly HttpClient _client;

        public ConsultaController(ILogger<ConsultaController> logger, IHttpClientFactory clientFactory)
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
        public IActionResult Create(int? idPaciente)
        {
            if (idPaciente is null)
            {
                SetSelectList();
                return View();
            }

            var viewModel = new ConsultaViewModel { PacienteId = (int)idPaciente };
            SetSelectList(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ConsultaViewModel viewModel)
        {
            if (ModelState.IsValid)
                return SetViewModelByActionName(nameof(Create), viewModel);

            SetSelectList(viewModel);
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return GetViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ConsultaViewModel viewModel)
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
        public IActionResult Delete(ConsultaViewModel viewModel)
        {
            return SetViewModelByActionName(nameof(Delete), viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public SelectList FilterExame(int idTipoExame)
        {
            return new SelectList(ListExame(idTipoExame), "ExameId", "Nome");
        }

        private IActionResult GetListViewModel()
        {
            HttpResponseMessage response = null;

            try
            {
                response = _client.GetAsync($"consulta").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<IEnumerable<ConsultaViewModel>>(response.Content.ReadAsStringAsync().Result);
                    return View(viewModel.OrderBy(o => o.Paciente.Nome));
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
                response = _client.GetAsync($"consulta/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<ConsultaViewModel>(response.Content.ReadAsStringAsync().Result);
                    SetSelectList(viewModel);
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

        private IEnumerable<PacienteViewModel> ListPaciente
        {
            get
            {
                HttpResponseMessage response = null;
                IEnumerable<PacienteViewModel> viewModelList = new List<PacienteViewModel>();

                try
                {
                    response = _client.GetAsync($"paciente").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        viewModelList = JsonConvert.DeserializeObject<IEnumerable<PacienteViewModel>>(response.Content.ReadAsStringAsync().Result);
                        return viewModelList;
                    }
                    else
                    {
                        SendFeedback(response);
                        return viewModelList;
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message, ex, ex.InnerException);
                    SendFeedback(response);
                    return viewModelList;
                }
            }
        }

        private IEnumerable<ExameViewModel> ListExame(int? idTipoExame = null)
        {
            HttpResponseMessage response = null;
            IEnumerable<ExameViewModel> viewModelList = new List<ExameViewModel>();

            try
            {
                response = _client.GetAsync($"exame").Result;

                if (response.IsSuccessStatusCode)
                {
                    viewModelList = JsonConvert.DeserializeObject<IEnumerable<ExameViewModel>>(response.Content.ReadAsStringAsync().Result).OrderBy(o => o.Nome);
                    return idTipoExame is null ? viewModelList : viewModelList.Where(w => w.TipoExameId == idTipoExame);
                }
                else
                {
                    SendFeedback(response);
                    return viewModelList;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex, ex.InnerException);
                SendFeedback(response);
                return viewModelList;
            }
        }

        private IEnumerable<TipoExameViewModel> ListTipoExame
        {
            get
            {
                HttpResponseMessage response = null;
                IEnumerable<TipoExameViewModel> tipoExameViewModel = new List<TipoExameViewModel>();

                try
                {
                    response = _client.GetAsync($"tipoexame").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        tipoExameViewModel = JsonConvert.DeserializeObject<IEnumerable<TipoExameViewModel>>(response.Content.ReadAsStringAsync().Result);
                        return tipoExameViewModel.OrderBy(o => o.Nome);
                    }
                    else
                    {
                        SendFeedback(response);
                        return tipoExameViewModel;
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message, ex, ex.InnerException);
                    SendFeedback(response);
                    return tipoExameViewModel;
                }
            }
        }

        private IActionResult SetViewModelByActionName(string actionName, ConsultaViewModel viewModel)
        {
            HttpResponseMessage response = null;
            string messageFeedback = "";

            try
            {
                switch (actionName)
                {
                    case nameof(Delete):
                        response = _client.DeleteAsync($"consulta/{viewModel.ConsultaId}").Result;
                        messageFeedback = $"Consulta deletada com sucesso.";
                        break;

                    case nameof(Create):
                        var contentCreate = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PostAsync("consulta", contentCreate).Result;
                        messageFeedback = $"Consulta criada com sucesso.";
                        break;

                    case nameof(Edit):
                        var contentEdit = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PutAsync($"consulta/{viewModel.ConsultaId}", contentEdit).Result;
                        messageFeedback = $"Consulta editada com sucesso.";
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

        private void SetSelectList(ConsultaViewModel viewModel = null)
        {
            if (viewModel is null)
            {
                ViewBag.PacienteId = new SelectList(ListPaciente, "PacienteId", "Nome");
                ViewBag.TipoExameId = new SelectList(ListTipoExame, "TipoExameId", "Nome");
                ViewBag.ExameId = new SelectList(new List<ExameViewModel>(), "ExameId", "Nome");
            }
            else
            {
                if (!(viewModel.Exame is null))
                    ViewBag.TipoExameId = new SelectList(ListTipoExame, "TipoExameId", "Nome", viewModel.Exame.TipoExameId);
                else
                    ViewBag.TipoExameId = new SelectList(ListTipoExame, "TipoExameId", "Nome");

                if (viewModel.PacienteId > Decimal.Zero)
                    ViewBag.PacienteId = new SelectList(ListPaciente, "PacienteId", "Nome", viewModel.PacienteId);
                else
                    ViewBag.PacienteId = new SelectList(ListPaciente, "PacienteId", "Nome");

                if (!(viewModel.Exame is null) && viewModel.ExameId > Decimal.Zero)
                    ViewBag.ExameId = new SelectList(ListExame(viewModel.Exame.TipoExameId), "ExameId", "Nome", viewModel.ExameId);
                else
                    ViewBag.ExameId = new SelectList(new List<ExameViewModel>(), "ExameId", "Nome");
            }
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
