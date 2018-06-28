using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
using WebClient.Services.Interfaces;

namespace WebClient.Controllers
{
    public class SymptomsController : Controller
    {
        private readonly ISymptomService _symptomService;

        public SymptomsController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        // GET: Symptoms
        public async Task<ActionResult> Index()
        {
            return View(await _symptomService.GetAllAsync());
        }

        // GET: Symptoms/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var symptom = await _symptomService.GetByIdAsync(id);
            if (symptom == null) return NotFound();
            return View(symptom);
        }

        // GET: Symptoms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Symptoms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Symptom symptom)
        {
            if (ModelState.IsValid)
            {
                await _symptomService.AddAsync(symptom);
                return RedirectToAction(nameof(Index));
            }
            return View(symptom);
        }

        // GET: Symptoms/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var symptom = await _symptomService.GetByIdAsync(id);
            if (symptom == null) return NotFound();
            return View(symptom);
        }

        // POST: Symptoms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Symptom symptom)
        {
            if (ModelState.IsValid)
            {
                await _symptomService.UpdateAsync(symptom);
                return RedirectToAction(nameof(Index));
            }
            return View(symptom);
        }

        // GET: Symptoms/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var symptom = await _symptomService.GetByIdAsync(id);
            if (symptom == null) return NotFound();
            return View(symptom);
        }

        // POST: Symptoms/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Symptom symptom)
        {
            await _symptomService.DeleteAsync(symptom.SymptomId);
            return RedirectToAction(nameof(Index));
        }
    }
}