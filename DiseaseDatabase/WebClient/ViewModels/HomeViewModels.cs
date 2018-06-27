using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Disease> TopDiseases { get; set; }
        public List<Symptom> TopSymptoms { get; set; }
        public int SymptomCount { get; set; }
    }
}
