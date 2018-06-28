using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
using WebClient.Services.Interfaces;
using WebClient.ViewModels;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiseaseService _diseaseService;
        private readonly ISymptomService _symptomService;

        public HomeController(IDiseaseService diseaseService, ISymptomService symptomService)
        {
            _diseaseService = diseaseService;
            _symptomService = symptomService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeIndexViewModel
            {
                TopDiseases = await _diseaseService.GetTopAsync(),
                TopSymptoms = await _symptomService.GetTopAsync(),
                SymptomCount = await _symptomService.GetCountAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                // full path to file in temp location
                var filePath = Path.GetTempFileName();

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var content = await System.IO.File.ReadAllLinesAsync(filePath);
                    await _diseaseService.PostCsvAsync(content); // TODO Go to Disease list maybe

                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
