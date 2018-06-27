using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using WebClient.Models;
using WebClient.Services.Interfaces;
using WebClient.ViewModels;

namespace WebClient.Controllers
{
    public class DiseasesController : Controller
    {
        private readonly IDiseaseService _diseaseService;
        private readonly ISymptomService _symptomService;

        public DiseasesController(IDiseaseService diseaseService, ISymptomService symptomService)
        {
            _diseaseService = diseaseService;
            _symptomService = symptomService;
        }

        // GET: Diseases
        public async Task<ActionResult> Index()
        {
            return View(await _diseaseService.GetAllAsync());
        }

        // GET: Diseases/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _diseaseService.GetByIdAsync(id));
        }

        // GET: Diseases/Create
        public async Task<ActionResult> Create()
        {
            var vm = new DiseaseCreateEditViewModel
            {
                SymtomsSelectList = new MultiSelectList(await _symptomService.GetAllAsync(), nameof(Symptom.SymptomId), nameof(Symptom.SymptomName))
            };
            return View(vm);
        }

        // POST: Diseases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DiseaseCreateEditViewModel vm)
        {
            try
            {
                await _diseaseService.AddAsync(vm.Disease);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                vm.SymtomsSelectList = new MultiSelectList(await _symptomService.GetAllAsync(),
                    nameof(Symptom.SymptomId), nameof(Symptom.SymptomName), vm.Disease.Symptoms);
                return View(vm);
            }
        }

        // GET: Diseases/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var disease = await _diseaseService.GetByIdAsync(id);
            var vm = new DiseaseCreateEditViewModel
            {
                Disease = disease,
                SymtomsSelectList = new MultiSelectList(await _symptomService.GetAllAsync(),
                    nameof(Symptom.SymptomId), nameof(Symptom.SymptomName), disease.Symptoms)
            };
            return View(vm);
        }

        // POST: Diseases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DiseaseCreateEditViewModel vm)
        {
            try
            {
                await _diseaseService.UpdateAsync(vm.Disease);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                vm.SymtomsSelectList = new MultiSelectList(await _symptomService.GetAllAsync(),
                    nameof(Symptom.SymptomId), nameof(Symptom.SymptomName), vm.Disease.Symptoms);
                return View(vm);
            }
        }

        // GET: Diseases/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _diseaseService.GetByIdAsync(id));
        }

        // POST: Diseases/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Disease disease)
        {
            try
            {
                _diseaseService.DeleteAsync(disease.DiseaseId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(disease);
            }
        }
    }
}