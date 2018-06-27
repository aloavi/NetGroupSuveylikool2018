using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
