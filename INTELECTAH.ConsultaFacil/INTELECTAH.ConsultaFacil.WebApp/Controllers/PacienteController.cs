using INTELECTAH.ConsultaFacil.Service.Common.Types;
using INTELECTAH.ConsultaFacil.ViewModel;
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
    public class PacienteController : Controller
    {
        private readonly ILogger<PacienteController> _logger;
        private readonly HttpClient _client;

        public PacienteController(ILogger<PacienteController> logger, IHttpClientFactory clientFactory)
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
            ViewBag.SexoEnum = SelectListSexo;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PacienteViewModel viewModel)
        {
            if (ModelState.IsValid)
                return SetViewModelByActionName(nameof(Create), viewModel);

            ViewBag.SexoEnum = SelectListSexo;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.SexoEnum = SelectListSexo;
            return GetViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PacienteViewModel viewModel)
        {
            if (ModelState.IsValid)
                return SetViewModelByActionName(nameof(Edit), viewModel);

            ViewBag.SexoEnum = SelectListSexo;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return GetViewModelById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PacienteViewModel viewModel)
        {
            return SetViewModelByActionName(nameof(Delete), viewModel);
        }

        private IActionResult GetListViewModel()
        {
            HttpResponseMessage response = null;

            try
            {
                response = _client.GetAsync($"paciente").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<IEnumerable<PacienteViewModel>>(response.Content.ReadAsStringAsync().Result);
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

        private IEnumerable<SelectListItem> SelectListSexo
        {
            get
            {
                var sexoEnumValues = Enum.GetValues(typeof(SexoEnum));
                IList<SelectListItem> sexoEnumList = new List<SelectListItem>();
                foreach (var item in sexoEnumValues)
                    sexoEnumList.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });

                return sexoEnumList;
            }
        }

        private IActionResult GetViewModelById(int id)
        {
            HttpResponseMessage response = null;

            try
            {
                response = _client.GetAsync($"paciente/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var viewModel = JsonConvert.DeserializeObject<PacienteViewModel>(response.Content.ReadAsStringAsync().Result);
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

        private IActionResult SetViewModelByActionName(string actionName, PacienteViewModel viewModel)
        {
            HttpResponseMessage response = null;
            string messageFeedback = "";

            try
            {
                switch (actionName)
                {
                    case nameof(Delete):
                        response = _client.DeleteAsync($"paciente/{viewModel.PacienteId}").Result;
                        messageFeedback = $"Paciente {viewModel.Nome?.ToUpper()} deletado com sucesso.";
                        break;

                    case nameof(Create):
                        var contentCreate = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PostAsync("paciente", contentCreate).Result;
                        messageFeedback = $"Paciente { viewModel.Nome?.ToUpper()} criado com sucesso.";
                        break;

                    case nameof(Edit):
                        var contentEdit = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                        response = _client.PutAsync($"paciente/{viewModel.PacienteId}", contentEdit).Result;
                        messageFeedback = $"Paciente {viewModel.Nome?.ToUpper()} editado com sucesso.";
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
            ViewBag.SexoEnum = SelectListSexo;
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
