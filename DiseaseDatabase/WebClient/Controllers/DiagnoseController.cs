using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;
using WebClient.Services.Interfaces;
using WebClient.ViewModels;

namespace WebClient.Controllers
{
    public class DiagnoseController : Controller
    {
        private readonly ISymptomService _symptomService;
        private readonly IDiagnoseService _diagnoseService;

        public DiagnoseController(ISymptomService symptomService, IDiagnoseService diagnoseService)
        {
            _symptomService = symptomService;
            _diagnoseService = diagnoseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Symptoms()
        {
            var vm = new DiagnoseSymtomsViewModel
            {
                Symptoms = await _symptomService.GetAllAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Symptoms(DiagnoseSymtomsViewModel vm)
        {
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Results(DiagnoseSymtomsViewModel vm)
        {
            //TODO Check if none were selected
            var selectedSymptoms = vm.Symptoms.Where(s => s.IsSelected).ToList();
            if (selectedSymptoms.Count > 0)
            {
                vm.Diseases = await _diagnoseService.DiagnoseBySymptomsAsync(selectedSymptoms);

                if (vm.Diseases != null)
                    return View(vm);
            }

            // something went wrong
            return RedirectToAction(nameof(Symptoms), vm);
        }

        public IActionResult Questionnaire()
        {
            // TODO complete afther api side gets done
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Questionnaire(DiagnoseQuestionnaireViewModel vm)
        {
            return View();
        }

    }
}