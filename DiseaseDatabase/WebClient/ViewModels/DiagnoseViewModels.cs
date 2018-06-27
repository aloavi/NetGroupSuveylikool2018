using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;

namespace WebClient.ViewModels
{
    public class DiagnoseSymtomsViewModel
    {
        public List<Symptom> Symptoms { get; set; }
        public List<Disease> Diseases { get; set; }
    }
    public class DiagnoseQuestionnaireViewModel
    {
        // TODO Replace with actual fields

    }
}