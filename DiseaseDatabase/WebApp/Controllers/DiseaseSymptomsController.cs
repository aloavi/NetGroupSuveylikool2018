using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class DiseaseSymptomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiseaseSymptomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiseaseSymptoms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DiseaseSymptoms.Include(d => d.Disease).Include(d => d.Symptom);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DiseaseSymptoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseSymptom = await _context.DiseaseSymptoms
                .Include(d => d.Disease)
                .Include(d => d.Symptom)
                .FirstOrDefaultAsync(m => m.DiseaseSymptomId == id);
            if (diseaseSymptom == null)
            {
                return NotFound();
            }

            return View(diseaseSymptom);
        }

        // GET: DiseaseSymptoms/Create
        public IActionResult Create()
        {
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "DiseaseId", "DiseaseName");
            ViewData["SymptomId"] = new SelectList(_context.Symptoms, "SymptomId", "SymptomName");
            return View();
        }

        // POST: DiseaseSymptoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiseaseSymptomId,DiseaseId,SymptomId")] DiseaseSymptom diseaseSymptom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diseaseSymptom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "DiseaseId", "DiseaseName", diseaseSymptom.DiseaseId);
            ViewData["SymptomId"] = new SelectList(_context.Symptoms, "SymptomId", "SymptomName", diseaseSymptom.SymptomId);
            return View(diseaseSymptom);
        }

        // GET: DiseaseSymptoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseSymptom = await _context.DiseaseSymptoms.FindAsync(id);
            if (diseaseSymptom == null)
            {
                return NotFound();
            }
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "DiseaseId", "DiseaseName", diseaseSymptom.DiseaseId);
            ViewData["SymptomId"] = new SelectList(_context.Symptoms, "SymptomId", "SymptomName", diseaseSymptom.SymptomId);
            return View(diseaseSymptom);
        }

        // POST: DiseaseSymptoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiseaseSymptomId,DiseaseId,SymptomId")] DiseaseSymptom diseaseSymptom)
        {
            if (id != diseaseSymptom.DiseaseSymptomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diseaseSymptom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseSymptomExists(diseaseSymptom.DiseaseSymptomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "DiseaseId", "DiseaseName", diseaseSymptom.DiseaseId);
            ViewData["SymptomId"] = new SelectList(_context.Symptoms, "SymptomId", "SymptomName", diseaseSymptom.SymptomId);
            return View(diseaseSymptom);
        }

        // GET: DiseaseSymptoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseSymptom = await _context.DiseaseSymptoms
                .Include(d => d.Disease)
                .Include(d => d.Symptom)
                .FirstOrDefaultAsync(m => m.DiseaseSymptomId == id);
            if (diseaseSymptom == null)
            {
                return NotFound();
            }

            return View(diseaseSymptom);
        }

        // POST: DiseaseSymptoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diseaseSymptom = await _context.DiseaseSymptoms.FindAsync(id);
            _context.DiseaseSymptoms.Remove(diseaseSymptom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseaseSymptomExists(int id)
        {
            return _context.DiseaseSymptoms.Any(e => e.DiseaseSymptomId == id);
        }
    }
}
