using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;

namespace WebClient.ViewModels
{
    public class DiseaseCreateEditViewModel
    {
        public Disease Disease { get; set; }
        public List<int> SymptomIds { get; set; } = new List<int>();
        public MultiSelectList SymtomsSelectList { get; set; }
    }
}